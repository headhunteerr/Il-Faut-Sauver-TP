using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet {

    private bool oxygen, food, water, fuel;
    private float gravityScale;

    public Planet(bool oxygen, bool food, bool water, bool fuel, float gravityScale)
    {
        this.oxygen = oxygen;
        this.food = food;
        this.water = water;
        this.fuel = fuel;
        this.gravityScale = gravityScale;
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

    public float getGravityScale()
    {
        return gravityScale;
    }
}
