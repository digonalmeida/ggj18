using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour {

    public float walkSpeed;
    public bool direction;
    public float maxDistance;
    public float infectionLevel;
    public bool isSick = false;    

    void Start () {
        
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
        if(direction && other.tag != "Trigger")
        {
            Debug.Log("Estou em um trigger!? " + gameObject.tag + "->" + other.gameObject.tag);
            infectionLevel += 0.5f;
        }
    }

    void Update () {
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
