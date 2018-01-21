using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BselaController : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D bsela;
    bool isGrounded;
    bool canDoubleJump;
    float xMin, xMax, yMin;
    Collider2D collider;
    public int numberOfRays;
    Vector2[] rayCastDown;
    //facing direction is a float that is positive when the character is facing right and negative when he is facing left
    float facingDirection;
    public GameObject smallShootedBesla;

    ShootProjectile shootProjectile;
	void Start () {
        shootProjectile = GetComponent<ShootProjectile>();
        facingDirection = 1;
        bsela = GetComponent<Rigidbody2D>();
        isGrounded = true;
        canDoubleJump = false;
        collider = GetComponent<Collider2D>();

        xMin = collider.bounds.min.x-this.transform.position.x;
        xMax = collider.bounds.max.x-this.transform.position.x;
        yMin = collider.bounds.min.y-this.transform.position.y;
        rayCastDown = new Vector2[numberOfRays];
        var dy = xMax - xMin;
        for (int i = 0; i < numberOfRays; i++)
        {
            var x = i * dy / (numberOfRays - 1) + xMin;
            rayCastDown[i] = new Vector2(x, yMin);

        }
    }
	
	// Update is called once per frame
	void Update () {
        #region Move Controlls
        float mydirection=Input.GetAxis("Horizontal1");
        if (mydirection>0)
        {
            var velocity = bsela.velocity;
            velocity.x = 10;
            bsela.velocity = velocity;
            facingDirection = 1;
        }
        else if (mydirection<0)
        {

            var velocity = bsela.velocity;
            velocity.x = -10;
            bsela.velocity = velocity;
            facingDirection = -1;
        }
        else
        {
            var velocity = bsela.velocity;
            velocity.x = 0;
            bsela.velocity = velocity;
        }

       
        #endregion
        #region Jump Controlls
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)&&(isGrounded||canDoubleJump))
        {
            if (isGrounded == true && canDoubleJump == false)
            {
                bsela.gravityScale = 2f;
                var velocity = bsela.velocity;
                velocity.y = 10;
                bsela.velocity = velocity;
                canDoubleJump = true;
                isGrounded = false;
               
            }
            else if (canDoubleJump == true)
            {
                bsela.gravityScale = 2f;
                var velocity = bsela.velocity;
                velocity.y = 10;
                bsela.velocity = velocity;
                canDoubleJump = false;              
            }
            
           
            
        }
        if (bsela.velocity.y<0)
        {
            bsela.gravityScale = 1.5f;
            for (int i = 0; i < numberOfRays; i++)
            {
                Debug.DrawRay(this.transform.position + (Vector3)rayCastDown[i], Vector3.down);
                var hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastDown[i], Vector3.down, .3f);
                if (hit)
                {
                    isGrounded = true;
                    
                }
            }
        }
        #endregion
        #region Shooting bsela 
        shootProjectile.Shoot(facingDirection);
     
        #endregion
    }



    
}

