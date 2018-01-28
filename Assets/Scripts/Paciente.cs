using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour {
    public GameObject healAnimation = null;
    Animator animator;
    public float walkSpeed;
    bool _direction = false;
    public bool direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
            updateDirection();
        }
    }
    public float maxDistance;
    public float infectionLevel;
    public bool isSick = false;
    
    public SpriteRenderer spriteRenderer;

    private float infectionTime = 0f;
    private float infectionCooldown = 2f;
    public float infectionPerSecond = 1;
    public float healPerSecond = 2;

    private bool _lastDirection = true;

    public float hospitalLinePos;
    public bool bateVolta = false;

    AudioManagerSingleton.AudioClipName healSfx = AudioManagerSingleton.AudioClipName.HEAL;
    Rigidbody2D _rigidbody;

    // Máquina de estados do paciente
    /*
    walking -> stunned (OnDragged -- chamado por DragAndDrop.cs)
    stunned -> hospital/walking (OnDragged -- chamado por DragAndDrop.cs)
    */

    public enum State {
        WALKING,
        STUNNED,
        HOSPITAL,
        DUMMY
    }
    public State state;
    void Start()
    {
        animator = GetComponent<Animator>();
        setState(State.WALKING);
        
    }


    void Update()
    {
        switch(state)
        {
            case State.WALKING:
                UpdateWalking();
                break;
            case State.HOSPITAL:
                UpdateHospital();
                break;
        }
    }

    void UpdateHospital()
    {
        
        if(infectionLevel > 0 && infectionLevel - (healPerSecond * Time.deltaTime) < 0)
        {
            healAnimation.SetActive(true);
            AudioManagerSingleton.instance.sfxVolume = 1.4f;
            AudioManagerSingleton.instance.PlaySound(healSfx, AudioManagerSingleton.AudioType.SFX);
            AudioManagerSingleton.instance.sfxVolume = 0.4f;
            healAnimation.GetComponent<Animator>().Play("heal");
        }
        infectionLevel -= healPerSecond * Time.deltaTime;
        if (infectionLevel <= -10) infectionLevel = 10;
        UpdateInfectionIndicator();
        if(infectionLevel <= 0)
        {

        }
       
    }
    public void setState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.WALKING:
                animator.Play("walking");
                break;
            case State.HOSPITAL:
                animator.Play("idle");
                break;
        }
    }
    public void OnDragged()
    {
        setState(State.STUNNED);
    }
    public void OnDropped()
    {
        if(transform.position.y > 1)
        {
            setState(State.WALKING);
        }
        else
        {
            setState(State.HOSPITAL);
        }
    }
    void Awake ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        infectionLevel = Random.Range(0, 10f);

        if (infectionLevel >= 6)
        {
            isSick = true;
        }

        // Define, ao iniciar, o lado que ele se movimentará
        if (transform.position.x > 0)
        {
            direction = false;
        }
        else
        {
            direction = true;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(state != State.WALKING)
        {
            return;
        }
        // Se estou dentro do raio de um infectado, me infecte mais rápido. NÃO FUNCIONA
        if (other.gameObject.GetComponent<Paciente>() != null)
        {
            Paciente otherPacient = other.gameObject.GetComponent<Paciente>();
            bool otherDirection = otherPacient.direction;
            if ((direction != otherDirection) && other.tag != "Trigger" && (Time.time > infectionTime + infectionCooldown))
            {
                infectionLevel += 5f;
                infectionTime = Time.time;
            }
        }
    }

    
    void updateDirection()
    {
        if (!direction) // Significa que spawnei na direita. Mover para a esquerda.
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x);
            transform.localScale = newScale;

        }
        else // Significa que spawnei na esquerda. Mover para a direita.
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
    }

    void UpdateInfectionIndicator()
    {
        float l = infectionLevel;
        if(l < 0)
        {
            l = 0;
        }

        Color c = spriteRenderer.color;
        c.a = l * 0.1f;
        spriteRenderer.color = c;
    }
    void UpdateWalking(){
        
        if(direction != _lastDirection)
        {
            updateDirection();
            _lastDirection = direction;
        }
        // Mais infectado = mais verde
        if (infectionLevel > 10) infectionLevel = 10;
        if (infectionLevel < 0) infectionLevel = 0;

        UpdateInfectionIndicator();

        infectionLevel += infectionPerSecond * Time.deltaTime;

        if (!direction) // Significa que spawnei na direita. Mover para a esquerda.
        {
            if(walkSpeed != 0)
            {
                _rigidbody.velocity = Vector3.left * (walkSpeed + Random.Range(0f, 2.0f));
                //transform.Translate(Vector3.left * Time.deltaTime * (walkSpeed + Random.Range(0f, 2f)));
            }


        }
        else // Significa que spawnei na esquerda. Mover para a direita.
        {
            if(walkSpeed != 0)
            {
                _rigidbody.velocity = Vector3.right * (walkSpeed + Random.Range(0f, 2.0f));
            }
            //   transform.Translate(Vector3.right * Time.deltaTime * (walkSpeed + Random.Range(0f, 2f)));
        }

        // Pequena aleatorizada no movimento vertical
       // transform.Translate(Vector3.up * Time.deltaTime * (Random.Range(-1f, 1f)));


        // É destruído se sai da tela
        if (transform.position.x < -maxDistance || transform.position.x > maxDistance)
        {
            Debug.Log("Estou fora da tela. Ativando suicídio.");
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.collider.tag == "Wall")
        {
            direction = !direction;
        }
        updateDirection();
    }

    
}
