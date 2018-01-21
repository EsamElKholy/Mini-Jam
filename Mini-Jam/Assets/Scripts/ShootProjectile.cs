using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            Vector3 position = new Vector3();
            position = transform.position;
            var newlyCreatedBsela = Instantiate(projectile, position, new Quaternion());
            Vector2 newlyCreatedObjectVelocity = newlyCreatedBsela.GetComponent<Rigidbody2D>().velocity;
            newlyCreatedObjectVelocity.x = 10;
            newlyCreatedBsela.GetComponent<Rigidbody2D>().velocity = newlyCreatedObjectVelocity;
        }
    }
}
