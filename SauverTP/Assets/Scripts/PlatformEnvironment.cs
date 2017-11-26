using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformEnvironment : MonoBehaviour {

    public GameObject food;
    public GameObject fuel;
    public GameObject water;
    private const int MAX_X = 700;

    // Use this for initialization
    void Start () {
        Planet planet = new Planet(true,true,true,true,1);

        if (planet.withFuel())
        {
            for (int i = Random.Range(2, 10); i > 0; i--)
            {
                instantiateObject(fuel);
            }
        }

        if (planet.withFood())
        {
            for (int i = Random.Range(2, 10); i > 0; i--)
            {
                instantiateObject(food);
            }
        }


        if (planet.withWater())
        {
            for (int i = Random.Range(2, 10); i > 0; i--)
            {
                instantiateObject(water);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void instantiateObject(GameObject which)
    {
        float x;
        do
        {
            x = Random.Range(-MAX_X, MAX_X);
        } while (Mathf.Abs(x) < 10);

        GameObject obj = Instantiate(which, new Vector3(x, which.transform.position.y), Quaternion.identity);
        obj.name = which.name;

    }
}
