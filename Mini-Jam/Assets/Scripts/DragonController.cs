using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    private float xMin, xMax, yMin;
    private Collider2D collider;
    public int numberOfRays;
    private Vector2[] rayCastDown;

    public bool isGrounded;
    private bool canFloat;
    public bool isFloating;

    public float fallGravity = 5;
    public float jumpGravity = 2;
    public float floatGravity = 0.3f;

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        isGrounded = true;
        canFloat = false;
        isFloating = false;

        rayCastDown = new Vector2[numberOfRays];

        collider = GetComponent<Collider2D>();

        xMin = collider.bounds.min.x - this.transform.position.x;
        xMax = collider.bounds.max.x - this.transform.position.x;
        yMin = collider.bounds.min.y - this.transform.position.y;
        rayCastDown = new Vector2[numberOfRays];
        var dy = xMax - xMin;
        for (int i = 0; i < numberOfRays; i++)
        {
            var x = i * dy / (numberOfRays - 1) + xMin;
            rayCastDown[i] = new Vector2(x, yMin);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
#region MOVEMENT
        if (Input.GetKey(KeyCode.A))
        {
            MoveH(-speed);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            MoveH(0.0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveH(speed);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            MoveH(0.0f);
        }
        #endregion

#region JUMP AND FLOAT
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (isGrounded == true && canFloat == false)
            {
                body.gravityScale = jumpGravity;
                var velocity = body.velocity;
                velocity.y = 10;
                body.velocity = velocity;
                canFloat = true;
                isGrounded = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canFloat)
        {
            body.gravityScale = jumpGravity;
            var velocity = body.velocity;
            velocity.y = 7f;
            body.velocity = velocity;
            canFloat = false;
            isFloating = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isFloating)
        {
            isFloating = false;
        }

        if (body.velocity.y < 0)
        {
            if (isFloating == true)
            {
                body.gravityScale = floatGravity;
            }
            else
            {
                body.gravityScale = fallGravity;
            }

            for (int i = 0; i < numberOfRays; i++)
            {
                Debug.DrawRay(this.transform.position + (Vector3)rayCastDown[i], Vector3.down);
                var hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastDown[i], Vector3.down, .3f);
                if (hit)
                {
                    isGrounded = true;
                    isFloating = false;
                    canFloat = false;
                }
            }
        }
        else if (body.velocity.y == 0.0)
        {
            isGrounded = true;
            isFloating = false;
            canFloat = false;
        }
#endregion
    }

    void MoveH(float h)
    {
        body.velocity = new Vector3(h, body.velocity.y);
    }
}
