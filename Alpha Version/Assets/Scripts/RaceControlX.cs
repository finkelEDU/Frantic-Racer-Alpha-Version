using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceControlX : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 0;

    public bool raceActive = true;

    void Update()
    {
        if(raceActive)
        {
            //timerText.SetText("Time: " + Mathf.Round(timer));
            timerText.SetText("Time: " + timer.ToString("F3"));
            timer += Time.deltaTime;
        }
        else
        {
            timerText.SetText("Finished! \nFinal time: " 
                            + timer.ToString("F3")
                            + "\n Press R to try again");
        }
    }
}
