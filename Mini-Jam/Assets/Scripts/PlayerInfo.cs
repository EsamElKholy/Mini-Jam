using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int Health;
    public int Energy;
    // Use this for initialization

    void Start ()
    {
        Health = 100;
        Energy = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddHealth(int val)
    {
        Health += val;

        Health = Mathf.Clamp(Health, 0, 100);
    }

    public void AddEnergy(int val)
    {
        Energy += val;

        Energy = Mathf.Clamp(Energy, 0, 100);
    }
}
