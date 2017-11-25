using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPanel : MonoBehaviour{

    public Button startButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart(){
        StartCoroutine(waitToRead());
        startButton.gameObject.SetActive(true);
    }

    IEnumerator waitToRead()
    {
        yield return new WaitForSeconds(5f);
    }
}
