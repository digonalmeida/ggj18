using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreMannager : MonoBehaviour {

	public int scoreSaved = 0;

	public static GameScoreMannager mInstance;

	public static GameScoreMannager Instance
	{
		get
		{
			if (mInstance == null){
				GameObject go = new GameObject();
				mInstance = go.AddComponent<GameScoreMannager>();
				mInstance.scoreSaved = 0;
			}
			return mInstance;
		}
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
