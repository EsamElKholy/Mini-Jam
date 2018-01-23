using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotAttack : MonoBehaviour
{
    float attackWaitTime;
    float attackTimeCounter;
    Collider2D myCollider;

    bool beginCarrotAttack;
    float carrotWaitTime;
    float carrotTimeCounter;

    Rigidbody2D myBody;
    CharacterController myController;
    PlayerInfo myInfo;

    // Use this for initialization
    void Start ()
    {
        myCollider = GetComponent<Collider2D>();
        attackTimeCounter = 0;
        attackWaitTime = 0.2f;

         carrotWaitTime=0.005f;
         carrotTimeCounter=0;

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

        if (beginCarrotAttack)
        {
            carrotTimeCounter += Time.deltaTime;
        }
        if (carrotTimeCounter>=carrotWaitTime)
        {
            if (myController.isBlocking)
            {
                myInfo.AddHealth(-10);
                myInfo.AddEnergy(-15);
            }
            else
            {
                myInfo.AddHealth(-15);
            }
            myCollider.enabled = false;
            myBody.AddForce(new Vector2(50, 25) * 1.0f, ForceMode2D.Impulse);
            carrotTimeCounter = 0;
            beginCarrotAttack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dragon")
        {
            myBody = collision.GetComponent<Rigidbody2D>();
            myInfo= collision.gameObject.GetComponent<PlayerInfo>();
            myController = collision.gameObject.GetComponent<CharacterController>();
            beginCarrotAttack = true;
            
            
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
