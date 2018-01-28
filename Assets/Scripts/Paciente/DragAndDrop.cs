using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class DragAndDrop : MonoBehaviour {
    Animator animator;
    GameController gameController;
    //Used for drop inertia
    public List<Vector3> inertiaPositioHistory;
    public int inertiaHistoryMaxCount = 5;
    public float inertiaSensibility = 40;
    public float inertiaDuration = 0.3f;

    //animation
    string idleAnimationName = "idle";
    string draggingAnimationName = "dragging";
    string droppingAnimationName = "dropping";

    public float colliderRadius = 10f;


    AudioManagerSingleton.AudioClipName draggingSfx = AudioManagerSingleton.AudioClipName.GRITO1;
    AudioManagerSingleton.AudioClipName droppingSfx = AudioManagerSingleton.AudioClipName.SWOSH;

	AudioManagerSingleton.AudioClipName falling = AudioManagerSingleton.AudioClipName.FALLING;

    Paciente _paciente;

    public enum State {
        IDLE,
        DRAGGING,
        DROPPING
    };

    public State state = State.IDLE;
    Rigidbody2D _rigidBody;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        _rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _paciente = GetComponent<Paciente>();
    }

    void Update()
    {
        switch (state)
        {
            case State.DRAGGING:
                updateDragging();
                return;
            case State.IDLE:
                UpdateIdle();
                return;
        }
        Vector3 pos = transform.position;
        pos.z = pos.y * 10;
        transform.position = pos;
    }

    void updateAnimation()
    {
        switch (state)
        {
            case State.IDLE:
               // setAnimation(idleAnimationName);
                break;
            case State.DRAGGING:
                setAnimation(draggingAnimationName);
                break;
            case State.DROPPING:
                setAnimation(droppingAnimationName);
                break;
        }
    }

    void setAnimation(string animationName)
    {
        if(animator == null)
        {
            Debug.Log("no animator set");
            return;
        }

        animator.Play(animationName);
    }

    public void recordInertiaHistory()
    {
        inertiaPositioHistory.Add(transform.position);
        if(inertiaPositioHistory.Count > inertiaHistoryMaxCount)
        {
            inertiaPositioHistory.RemoveAt(0);
        }
    }

    public void clearInertiaHistory()
    {
        inertiaPositioHistory.Clear();
    }
    
    void setState(State newState)
    {
        state = newState;
        switch(newState)
        {
            case State.DROPPING:
                startDropping();
                break;
            case State.DRAGGING:
                startDragging();
                break;
        }
        updateAnimation();
    }

    void UpdateIdle()
    {
        if(checkMouseCollision())
        {
            setState(State.DRAGGING);
        }
    }
    public float distance;
    bool checkMouseCollision()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width ||
            Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return false;
        }

        if(gameController.holding >= gameController.maxHolding)
        {
            return false;
        }
        if(!Input.GetMouseButton(0))
        {
            return false;
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition);

        distance = ((Vector2)transform.position - mousePosition).magnitude;
        
        if (distance < colliderRadius)
        {
            return true;
        }
        return false;
    }
    void startDragging()
    {
       // _rigidBody.isKinematic = true;
        gameController.holding++;
        AudioManagerSingleton.instance.PlaySound(Random.Range(4, 9), AudioManagerSingleton.AudioType.SFX, false, 0.5f);
        _paciente.OnDragged();
    }
    void updateDragging()
    {
        recordInertiaHistory();
        moveToMousePosition();

        if (Input.GetMouseButtonUp(0))
        {
            setState(State.DROPPING);
        }
    }
    
    void startDropping()
    {
        _rigidBody.isKinematic = false;
        gameController.holding = 0;
		AudioManagerSingleton.instance.PlaySound(droppingSfx, AudioManagerSingleton.AudioType.SFX, false, 1f);
        applyInertia();
        StartCoroutine(droppingCoroutine());
    }

    void applyInertia()
    {
		
        Vector2 throwVelocity;

        if (inertiaPositioHistory.Count == 0)
        {
            throwVelocity = Vector2.zero;
        }
        else
        {
            
            throwVelocity = (transform.position - inertiaPositioHistory[0]) * inertiaSensibility;
            throwVelocity.x += Random.Range(-7f, 7f);
        }

        _rigidBody.velocity = throwVelocity;
        inertiaPositioHistory.Clear();
    }

    IEnumerator droppingCoroutine()
    {
        float droppingTimeout = inertiaDuration;

        while (droppingTimeout > 0)
        {
            droppingTimeout -= Time.deltaTime;
            yield return null;
        }
        _rigidBody.velocity = Vector2.zero;
		AudioManagerSingleton.instance.PlaySound (falling,AudioManagerSingleton.AudioType.SFX, false, 5f);
        yield return new WaitForSeconds(0.3f);
        stopDropping();
        yield return null;
    }

    void stopDropping()
    {
		
        animator.StopPlayback();
        _rigidBody.velocity = Vector2.zero;
        setState(State.IDLE);
        _paciente.OnDropped();

    }

    void moveToMousePosition()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
