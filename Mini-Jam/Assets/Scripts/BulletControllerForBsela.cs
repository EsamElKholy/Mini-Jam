using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerForBsela : MonoBehaviour
{

	// Use this for initialization
    public float destroyTime;
    public string targetTag;
    void Start ()
    {        
        Destroy(this.gameObject, destroyTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D otherObj)
    {
        if (otherObj.gameObject.tag == targetTag)
        {
            var controller = otherObj.gameObject.GetComponent<CharacterController>();
            var info = otherObj.gameObject.GetComponent<PlayerInfo>();

            if (controller.isBlocking && info.Energy > 0)
            {
                info.AddHealth(-5);
                info.AddEnergy(-10);
            }
            else
            {
                info.AddHealth(-10);
            }

            Destroy(this.gameObject);
        }
    }

}
