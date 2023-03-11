using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string sceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(sceneName);
    }
}
