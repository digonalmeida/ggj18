using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class DragAndDrop : MonoBehaviour {

    public enum State {
        IDLE,
        DRAGGING,
        DROPPING
    };

    public Animator animator;

    string idleAnimationName = "idle";
    string draggingAnimationName = "dragging";
    string droppingAnimationName = "dropping";

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

    public List<Vector3> lastPositions;
    public int numberOfPositions = 5;
    public float throwSensibility = 40;
    public UnityEvent OnStartDraggin;
    public UnityEvent OnStartDropping;
    public UnityEvent OnEndDropping;
    public float droppingTime = 0.3f;

    public void recordPosition()
    {
        lastPositions.Add(transform.position);
        if(lastPositions.Count > numberOfPositions)
        {
            lastPositions.RemoveAt(0);
        }
    }
    public void clearPositions()
    {
        lastPositions.Clear();
    }

    public Vector2 calculateDraggingVelocity()
    {
        if(lastPositions.Count == 0)
        {
            return Vector2.zero;
        }

        return (transform.position - lastPositions[0]) * throwSensibility;
    }

    public State state = State.IDLE;
    Rigidbody2D _rigidBody;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void setState(State newState)
    {
        state = newState;
        switch(newState)
        {
            case State.DRAGGING:
                startDragging();
                break;
            case State.DROPPING:
                startDropping();
                break;
            case State.IDLE:
                startIdle();
                break;
        }
        updateAnimation();
    }


    void Update()
    {
        switch(state)
        {
            case State.DRAGGING:
                updateDragging();
                return;
            case State.DROPPING:
                return;
            case State.IDLE:
                updateIdle();
                return;
        }
    }


    void startIdle()
    {
        Debug.Log("start idle");
    }

    void updateIdle()
    {
        if(checkIfClicked())
        {
          //  setState(State.DRAGGING);
        }
    }

    bool checkIfClicked()
    {
        return false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection;
        
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void startDragging()
    {
        OnStartDraggin.Invoke();
    }

    void stopDropping()
    {
        OnEndDropping.Invoke();
        setState(State.IDLE);
        _rigidBody.velocity = Vector2.zero;
    }

    void updateDragging()
    {
        recordPosition();

        Debug.Log("stop dragging");
        MoveToMousePosition();

        
    }
    
    void startDropping()
    {
        OnStartDropping.Invoke();
        //throw
        Vector2 throwVelocity = calculateDraggingVelocity();
        _rigidBody.velocity = throwVelocity;
        clearPositions();
        Debug.Log("startDropping");
        StartCoroutine(droppingCoroutine());
    }
    
    IEnumerator droppingCoroutine()
    {
        float droppingTimeout = droppingTime;

        while (droppingTimeout > 0)
        {
            droppingTimeout -= Time.deltaTime;
            yield return null;
        }
        stopDropping();


        yield return null;
    }

    void MoveToMousePosition()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        transform.position = newPosition;
    }

    bool checkDropped()
    {
        if(Input.GetMouseButtonUp(0))
        {
            return true;
        }
        else
        {
            return false;
        }
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
