using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class DragAndDrop : MonoBehaviour {
    Animator animator;

    //Used for drop inertia
    public List<Vector3> inertiaPositioHistory;
    public int inertiaHistoryMaxCount = 5;
    public float inertiaSensibility = 40;
    public float inertiaDuration = 0.3f;

    //animation
    string idleAnimationName = "idle";
    string draggingAnimationName = "dragging";
    string droppingAnimationName = "dropping";


    AudioManagerSingleton.AudioClipName draggingSfx = AudioManagerSingleton.AudioClipName.GRITO;
    AudioManagerSingleton.AudioClipName droppingSfx = AudioManagerSingleton.AudioClipName.SWOSH;

    public enum State {
        IDLE,
        DRAGGING,
        DROPPING
    };

    public State state = State.IDLE;
    Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (state)
        {
            case State.DRAGGING:
                updateDragging();
                return;
        }
    }

    void updateAnimation()
    {
        switch (state)
        {
            case State.IDLE:
                setAnimation(idleAnimationName);
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

    void startDragging()
    {
        AudioManagerSingleton.instance.PlaySound(draggingSfx, AudioManagerSingleton.AudioType.SFX);
    }
    void updateDragging()
    {
        recordInertiaHistory();
        moveToMousePosition();
    }
    
    void startDropping()
    {
        AudioManagerSingleton.instance.PlaySound(droppingSfx, AudioManagerSingleton.AudioType.SFX);
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
        stopDropping();
        yield return null;
    }

    void stopDropping()
    {
        _rigidBody.velocity = Vector2.zero;
        setState(State.IDLE);
    }

    void moveToMousePosition()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
    
    void OnMouseDown()
    {
        if(state == State.IDLE)
        {
            setState(State.DRAGGING);
        }
    }

    void OnMouseUp()
    {
        if(state == State.DRAGGING)
        {
            setState(State.DROPPING);
        }
    }
}
