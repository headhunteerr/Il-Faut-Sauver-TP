using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEnvironment : MonoBehaviour {

    public GameObject food;
    public GameObject fuel;
    public GameObject water;
    // Use this for initialization
    void Start () {
        Planet planet = GameController.Instance.currentPlanet;

        if (true || planet.withFuel())
        {
            for (int i = Random.Range(2, 10); i > 0; i--)
            {
                instantiateObject(Random.Range(-1400, 1400), fuel);
            }
        }

        if (planet.withFood())
        {
            for (int i = Random.Range(2, 10); i > 0; i--)
            {
                instantiateObject(Random.Range(-1400, 1400), food);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void instantiateObject(float x, GameObject which)
    {
        GameObject obj = Instantiate(which, new Vector3(x, which.transform.position.y), Quaternion.identity);
        obj.name = which.name;

    }
}
