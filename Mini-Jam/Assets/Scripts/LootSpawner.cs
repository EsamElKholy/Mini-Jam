using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour {

    public GameObject lootBox;
    public Transform limitLeft;
    public Transform limitRight;
    public float coolDownTime;
    float coolDownCounter;

	// Use this for initialization
	void Start ()
    {
        coolDownTime = 15;
        coolDownCounter = 0;

    }
	
	// Update is called once per frame
	void Update ()
    {
        coolDownCounter += Time.deltaTime;
        if (coolDownCounter>coolDownTime)
        {
            var randomX = Random.Range(limitLeft.position.x, limitRight.position.x);
            Vector3 newPos = transform.position;
            newPos.x = randomX;
            var Box=Instantiate(lootBox, newPos, Quaternion.identity);
            var boxVel = Box.GetComponent<Rigidbody2D>().velocity;
            boxVel.x = Random.Range(-4, 4);
            Box.GetComponent<Rigidbody2D>().velocity = boxVel;
            Box.GetComponent<Rigidbody2D>().gravityScale = 0.5f;

            Destroy(Box, 7);
            coolDownCounter = 0;
        }
	}

   

}
