﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{


    public int playerHealth;
    public int playerFood;
    public int playerWater;
    public int playerFuel;
    public int playerOxygen;


    // Use this for initialization
    void Start () {
        playerHealth = GameController.Instance.playerHealth;
        playerFood = GameController.Instance.playerFood;
        playerWater = GameController.Instance.playerWater;
        playerFuel = GameController.Instance.playerFuel;
        playerOxygen = GameController.Instance.playerOxygen;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
