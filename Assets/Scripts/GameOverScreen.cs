using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{   
    public void SetUp()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        Debug.Log("Working");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
