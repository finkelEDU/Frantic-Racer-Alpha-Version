using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonControlX : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    public void Start()
    {
        startButton.onClick.AddListener(StartClicked);
        quitButton.onClick.AddListener(QuitClicked);
    }
    
    void StartClicked()
    {
        SceneManager.LoadScene("Level 1");
    }

    void QuitClicked()
    {
        Application.Quit();
    }
}
