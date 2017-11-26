
using UnityEngine;

public class Chronometer {
    private float time; // en secondes
    private bool started = false;

	// Use this for initialization
	public void Start () {
        time = 0;
        started = true;
	}
	
	// a appeler a chaque frame
	public void Update () {
        if (started)
        {
            time += Time.deltaTime;
        }
	}

    public float getTime()
    {
        return time;
    }

   public void Reset()
    {
        time = 0;
        started = false;
    }

    public bool hasStarted()
    {
        return started;
    }
}
