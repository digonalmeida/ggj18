using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeZone : MonoBehaviour {

	private float infectionAllow = 0.3f;
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Paciente")
        {
            Paciente paciente = other.GetComponent<Paciente>();
			if(paciente.infectionLevel > infectionAllow)
            {
                Bad();
            }
            else
            {
                Good();
            }
            Destroy(other.gameObject);
        }
    }

    void Good()
    {
        Debug.Log("good");
        animator.Play("Good");
		GameManager.Instance.scoreSaved ++;
		GameManager.Instance.contZombie --;
    }

    void Bad()
    {
        Debug.Log("bad");
        animator.Play("Bad");
		//GameOver
		SceneManager.LoadScene("GameOver");
    }
}
