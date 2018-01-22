using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dragon"|| collision.gameObject.tag == "Bsela")
        {
            if (Random.Range(0,2)==0)
            {
                collision.gameObject.GetComponent<PlayerInfo>().AddEnergy(50);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerInfo>().AddHealth(25);
            }
            
            Destroy(gameObject);
        }
    }

}
