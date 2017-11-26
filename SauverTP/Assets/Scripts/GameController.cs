using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance;
    public static int distanceToWin = 100;

    public static bool firstStart = true;

    public AudioSource musicSource;
    public Slider musicSlider;
    public int musicVolume;

    public Slider fxSlider;
    public int fxVolume;

    public int maxHealth = 100;
    public int maxFood = 100;
    public int maxWater = 100;
    public int maxFuel = 200;
    public int maxOxygen = 500;

    public float playerHealth;
    public int playerFood;
    public int playerWater;
    public int playerFuel;
    public int playerOxygen;
    public int distanceTravelled;
    public Planet currentPlanet;

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
            playerOxygen = maxOxygen;

            musicSlider.value = musicVolume;
            fxSlider.value = fxVolume;

            firstStart = false;
        }
    }

    public void changeMusicVolume()
    {
        musicVolume = (int)musicSlider.value;
        musicSource.volume = (float)musicVolume / 100;
    }

    public void gameStart()
    {
        SceneManager.LoadScene("ChoosePlanet");
    }

    public void goOnPlanet()
    {
        SceneManager.LoadScene("PlanetScene");
    }
}
