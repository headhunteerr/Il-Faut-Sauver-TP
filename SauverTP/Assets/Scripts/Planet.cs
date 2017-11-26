using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    private static string[] planetTypes = {"earth", "desert", "ice", "lava"};

    private bool oxygen;
    private bool food;
    private bool water;
    private bool fuel;
    private string planetType;
    private int distanceDelta;
    private int fuelConsumed;

    public Image planetImage;
    public Sprite earthSprite;
    public Sprite desertSprite;
    public Sprite iceSprite;
    public Sprite lavaSprite;

    private void Awake()
    {
        oxygen = Random.value > .5;
        food = Random.value > .5;
        water = Random.value > .5;
        fuel = Random.value > .5;
        planetType = planetTypes[Random.Range(0, 3)];
        if (planetType == "earth")
        {
            planetImage.sprite = earthSprite;
        }
        if (planetType == "desert")
        {
            planetImage.sprite = desertSprite;
        }
        if (planetType == "ice")
        {
            planetImage.sprite = iceSprite;
        }
        if (planetType == "lava")
        {
            planetImage.sprite = lavaSprite;
        }
        distanceDelta = Random.Range(-10, 10);
        fuelConsumed = Random.Range(50,75);

        this.gameObject.GetComponent<Button>().onClick.AddListener(chosenPlanet);
    }

    public bool withOxigen()
    {
        return oxygen;
    }

    public bool withFood()
    {
        return food;
    }

    public bool withWater()
    {
        return water;
    }

    public bool withFuel()
    {
        return fuel;
    }

    public string getPlanetType()
    {
        return this.planetType;
    }

    public void chosenPlanet()
    {
        GameController.Instance.currentPlanet = this;
        GameController.Instance.goOnPlanet();
    }
}
