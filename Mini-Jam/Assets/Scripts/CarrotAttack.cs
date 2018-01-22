using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotAttack : MonoBehaviour
{
    float attackWaitTime;
    float attackTimeCounter;
    Collider2D myCollider;

	// Use this for initialization
	void Start ()
    {
        myCollider = GetComponent<Collider2D>();
        attackTimeCounter = 0;
        attackWaitTime = 0.2f;
	}
	
	// Update is called once per frame
	void Update ()
    {        
        attackTimeCounter += Time.deltaTime;

        if (attackTimeCounter >= attackWaitTime)
        {
            attackTimeCounter = 0;
            myCollider.enabled = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dragon")
        {
            var body = collision.GetComponent<Rigidbody2D>();
            var info = collision.gameObject.GetComponent<PlayerInfo>();
            if (collision.gameObject.GetComponent<CharacterController>().isBlocking)
            {
                info.AddHealth(-10);
                info.AddEnergy(-15);
            }
            else
            {
                info.AddHealth(-15);
            }
            myCollider.enabled = false;
            body.AddForce(new Vector2(50,25)*1.0f,ForceMode2D.Impulse);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Dragon")
    //    {
    //        var info = collision.gameObject.GetComponent<PlayerInfo>();
    //        if (collision.gameObject.GetComponent<CharacterController>().isBlocking)
    //        {
    //            info.AddHealth(-10);
    //            info.AddEnergy(-15);
    //        }
    //        else
    //        {
    //            info.AddHealth(-15);
    //        }

    //        myCollider.enabled = false;
    //    }
    //}
}
