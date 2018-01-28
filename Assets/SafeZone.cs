using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeZone : MonoBehaviour {

	private float infectionAllow = 3.0f;
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
        AudioManagerSingleton.instance.PlaySound(
            AudioManagerSingleton.AudioClipName.RIGHT_MOVE, 
            AudioManagerSingleton.AudioType.SFX, false, 2);
		GameManager.Instance.scoreSaved ++;
		GameManager.Instance.contZombie --;

    }

    void Bad()
    {
        AudioManagerSingleton.instance.PlaySound(
            AudioManagerSingleton.AudioClipName.WRONG_MOVE,
            AudioManagerSingleton.AudioType.SFX, false, 2);
        Debug.Log("bad");
        animator.Play("Bad");
		GameManager.Instance.gameOverInfected = true;
		//GameOver
		SceneManager.LoadScene("GameOver");
    }
}
