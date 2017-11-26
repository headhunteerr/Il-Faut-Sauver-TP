using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetChooser : MonoBehaviour {

    public GameObject planetTemplate;

    private void Awake()
    {
        for (int i = 0; i < Random.Range(1, 5);  i++)
        {
            GameObject planet = Object.Instantiate(planetTemplate,planetTemplate.transform.parent);
            planet.SetActive(true);
        }
    }


}
