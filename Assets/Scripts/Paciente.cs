using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour {

    public float walkSpeed;
    public bool direction;
    public float maxDistance;
    public float infectionLevel;
    public bool isSick = false;

    SpriteRenderer spriteRenderer;

    private float infectionTime = 0f;
    private float infectionCooldown = 2f;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Awake ()
    {
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
        // Se estou dentro do raio de um infectado, me infecte mais rápido. NÃO FUNCIONA
        if (other.gameObject.GetComponent<Paciente>() != null)
        {
            Paciente otherPacient = other.gameObject.GetComponent<Paciente>();
            bool otherDirection = otherPacient.direction;
            if ((direction != otherDirection) && other.tag != "Trigger" && (Time.time > infectionTime + infectionCooldown))
            {
                Debug.Log("Estou em um trigger!? " + gameObject.tag + "->" + other.gameObject.tag);
                infectionLevel += 1f;
                infectionTime = Time.time;
            }
        }
    }

    void Update () {

        // Mais infectado = mais verde
        if (infectionLevel > 10) infectionLevel = 10;
        spriteRenderer.color = new Color(infectionLevel * 0.1f, 0f, 0f);

        infectionLevel += 0.1f * Time.deltaTime;

        if (!direction) // Significa que spawnei na direita. Mover para a esquerda.
        {
            transform.Translate(Vector3.left * Time.deltaTime * (walkSpeed + Random.Range(0f, 2f)));
        }
        else // Significa que spawnei na esquerda. Mover para a direita.
        {
            transform.Translate(Vector3.right * Time.deltaTime * (walkSpeed + Random.Range(0f, 2f)));
        }

        // Pequena aleatorizada no movimento vertical
        transform.Translate(Vector3.up * Time.deltaTime * (Random.Range(-1f, 1f)));


        // É destruído se sai da tela
        if (transform.position.x < -maxDistance || transform.position.x > maxDistance)
        {
            Debug.Log("Estou fora da tela. Ativando suicídio.");
            Destroy(gameObject);
        }
    }
}
