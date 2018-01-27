using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paciente : MonoBehaviour {

    public float walkSpeed;
    public bool direction;
    public float maxDistance;

    void Start () {
        
	}

    void Awake ()
    {
        if (transform.position.x > 0)
        {
            direction = false;
        }
        else
        {
            direction = true;
        }
    }
	
	void Update () {
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

        if (transform.position.x < -maxDistance || transform.position.x > 9.5f)
        {
            Debug.Log("Estou fora da tela. Ativando suicídio.");
            Destroy(gameObject);
        }
    }
}
