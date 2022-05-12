using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(2);
    }
}
