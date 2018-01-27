using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void OnButton_PlayGame(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnButton_QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayUISound()
    {

        AudioManagerSingleton.AudioClipName buttonSfx = AudioManagerSingleton.AudioClipName.CLICK;
        AudioManagerSingleton.instance.PlaySound(buttonSfx, AudioManagerSingleton.AudioType.SFX, false, 0.5f);
    }
}
