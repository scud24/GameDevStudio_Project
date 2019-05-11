using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void FourPlayerButtonClick()
    {
        SceneManager.LoadScene("SplitscreenTestScene");
    }
    public void TwoVTwoTeamsButtonClick()
    {
        Debug.Log("Not Implemented Yet");
    }
    public void OneVOneDuelButtonClick()
    {
        Debug.Log("Not Implemented Yet");
    }
    public void QuitButtonClick()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
