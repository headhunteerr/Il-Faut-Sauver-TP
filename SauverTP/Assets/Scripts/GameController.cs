using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public static bool firstStart = true;

    public GameObject musicSlider;
    public int musicVolume;

    public GameObject fxSlider;
    public int fxVolume;

    public int maxHealth = 100;
    public int maxFood = 100;
    public int maxWater = 100;
    public int maxFuel = 200;

    public int playerHealth;
    public int playerFood;
    public int playerWater;
    public int playerFuel;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (firstStart)
        {
            playerHealth = maxHealth;
            playerFood = maxFood;
            playerWater = maxWater;
            playerFuel = maxFuel;
        }
    }

    public void changeMusicVolume()
    {
        musicVolume = musicSlider.GetComponent("Slider").Value;
    }
}
