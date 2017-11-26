using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPanel : MonoBehaviour
{

    public Button startButton;

    private Chronometer chronometer = null;
    private bool activateButton;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (chronometer != null)
        {
            chronometer.Update();
            if (chronometer.getTime() >= 5)
            {
                startButton.gameObject.SetActive(true);
                chronometer = null;
            }
        }

    }

    public void GameStart()
    {
        chronometer = new Chronometer();
        chronometer.Start();

    }

}
