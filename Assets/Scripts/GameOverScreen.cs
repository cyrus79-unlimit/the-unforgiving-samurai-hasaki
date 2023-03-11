using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public float freezeDelay = 1.0f;
    public void SetUp()
    {
        Invoke("FreezeScene", freezeDelay);
        gameObject.SetActive(true);
    }

    private void FreezeScene()
    {
        Time.timeScale = 0f; // freeze the scene
    }

    public void RestartButton()
    {
        Time.timeScale = 1.0f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
