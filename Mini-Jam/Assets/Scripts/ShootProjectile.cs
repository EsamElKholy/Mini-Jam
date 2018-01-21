using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;

    // Update is called once per frame

    public void Shoot(float dir)
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            Vector3 position = new Vector3();
            position = this.transform.position;
            var newlyCreatedBsela = Instantiate(projectile, position, transform.rotation);
            Vector2 newlyCreatedObjectVelocity = newlyCreatedBsela.GetComponent<Rigidbody2D>().velocity;
            if (dir > 0)
            {
                newlyCreatedObjectVelocity.x = 10;
            }
            else
            {
                newlyCreatedObjectVelocity.x = -10;
            }
            newlyCreatedBsela.GetComponent<Rigidbody2D>().velocity = newlyCreatedObjectVelocity;
        }
    }
}
