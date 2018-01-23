using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Dragon"|| collision.gameObject.tag == "Bsela")
        {
            collision.gameObject.GetComponent<PlayerInfo>().AddHealth(-1000);
        }
    }
}
