using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{


    public float playerHealth;
    public int playerFood;
    public int playerWater;
    public int playerFuel;
    public int playerOxygen;
    public int distanceTravelled;


    // Use this for initialization
    void Start () {
        playerHealth = GameController.Instance.playerHealth;
        playerFood = GameController.Instance.playerFood;
        playerWater = GameController.Instance.playerWater;
        playerFuel = GameController.Instance.playerFuel;
        playerOxygen = GameController.Instance.playerOxygen;
        distanceTravelled = GameController.Instance.distanceTravelled;
    }
}
