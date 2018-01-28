using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeZone : MonoBehaviour {

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
        if(!enabled)
        {
            return;
        }
        if(other.tag == "Paciente")
        {
            Paciente paciente = other.GetComponent<Paciente>();
            if(paciente.infectionLevel > 0)
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
		GameScoreMannager.Instance.scoreSaved ++;
		Debug.Log(GameScoreMannager.Instance.scoreSaved);
    }

    void Bad()
    {
        Debug.Log("bad");
        animator.Play("Bad");
		//GameOver
		SceneManager.LoadScene("GameOver");
    }
}
