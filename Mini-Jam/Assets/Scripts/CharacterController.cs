using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    private float xMin, xMax, yMin, yMax;
    private Collider2D myCollider;
    public int numberOfRays;
    private Vector2[] rayCastDown;
    private Vector2[] rayCastRight;
    private Vector2[] rayCastLeft;
    private Animator myAnimator;

    public bool isGrounded;
    private bool canFloat;
    private bool canDoubleJump;
    public bool isFloating;

    public float fallGravity = 5;
    public float jumpGravity = 2;
    public float floatGravity = 0.3f;
    float facingDirection;
    public GameObject projectile;
    public GameObject spawnProjectiles;

    bool controllerConnected;

    private KeyCode right;
    private KeyCode left;
    private KeyCode jump;
    private KeyCode fire;
    private KeyCode lightMelee;
    private KeyCode heavyMelee;
    private KeyCode block;
    private KeyCode ultimate1;
    private KeyCode ultimate2;
    private KeyCode dash;

    public int heavyMeleeCost;
    public int ultimateAttackCost;

    public int playerNumber;

    private float myDirection;

    public bool isBlocking;
    private SimpleScreenShake shake;
    private PlayerInfo info;
    public Collider2D carrotCollider;

    public float ultimateWaitTime;
    private float ultimateWaitCounter;

    public float dashForce;
    public bool isDashing;

    private Vector3 dashVel;
    public float dashWaitTime;
    private float dashTimeCounter;

    // Use this for initialization
    void Start ()
    {
        myDirection = 0;
        isBlocking = false;
        isDashing = false;

        if (Input.GetJoystickNames().Length <= 1)
        {
            controllerConnected = false;

            if (playerNumber == 1)
            {
                right = KeyCode.D;
                left = KeyCode.A;
                jump = KeyCode.Space;
                fire = KeyCode.F;
                lightMelee = KeyCode.Z;
                heavyMelee = KeyCode.C;
                block = KeyCode.G;
                ultimate1 = KeyCode.V;
                ultimate2 = KeyCode.B;
                dash = KeyCode.R;
            }
            else if (playerNumber == 2)
            {
                right = KeyCode.Keypad6;
                left = KeyCode.Keypad4;
                jump = KeyCode.Keypad0;
                fire = KeyCode.Keypad9;
                lightMelee = KeyCode.Keypad1;
                heavyMelee = KeyCode.Keypad7;
                block = KeyCode.Keypad3;
                ultimate1 = KeyCode.O;
                ultimate2 = KeyCode.P;
                dash = KeyCode.I;
            }            
        }
        else
        {
            controllerConnected = true;

            if (playerNumber == 1)
            {
                jump = KeyCode.Joystick1Button0;
                fire = KeyCode.Joystick1Button1;
                lightMelee = KeyCode.Joystick1Button2;
                heavyMelee = KeyCode.Joystick1Button3;
                block = KeyCode.Joystick1Button5;
                ultimate1 = KeyCode.Joystick1Button6;
                ultimate2 = KeyCode.Joystick1Button7;
                dash = KeyCode.Joystick1Button4;
            }
            else if (playerNumber == 2)
            {
                jump = KeyCode.Joystick2Button0;
                fire = KeyCode.Joystick2Button1;
                lightMelee = KeyCode.Joystick2Button2;
                heavyMelee = KeyCode.Joystick2Button3;
                block = KeyCode.Joystick2Button5;
                ultimate1 = KeyCode.Joystick2Button6;
                ultimate2 = KeyCode.Joystick2Button7;
                dash = KeyCode.Joystick2Button4;
            }           
        }

        heavyMeleeCost = 25;
        ultimateAttackCost = 70;
        ultimateWaitTime = 30.0f / 60.0f;
        ultimateWaitCounter = 0;
        dashForce = 35;
        dashWaitTime = 0.17f;
        dashTimeCounter = 0;

        facingDirection = 1;
        body = GetComponent<Rigidbody2D>();
        isGrounded = true;
        canFloat = false;
        isFloating = false;
        canDoubleJump = false;

        rayCastDown = new Vector2[numberOfRays];
        rayCastRight = new Vector2[numberOfRays];
        rayCastLeft = new Vector2[numberOfRays];

        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        info = GetComponent<PlayerInfo>();
        shake = Camera.main.GetComponent<SimpleScreenShake>();

        xMin = myCollider.bounds.min.x - this.transform.position.x;
        xMax = myCollider.bounds.max.x - this.transform.position.x;

        yMin = myCollider.bounds.min.y - this.transform.position.y;
        yMax = myCollider.bounds.max.y - this.transform.position.y;

        var dx = xMax - xMin;
        var dy = yMax - yMin;
        for (int i = 0; i < numberOfRays; i++)
        {
            var x = i * dx / (numberOfRays - 1) + xMin;
            rayCastDown[i] = new Vector2(x, yMin);

            var y = i * dy / (numberOfRays - 1) + yMin;
            rayCastRight[i] = new Vector2(xMax, y);
            rayCastLeft[i] = new Vector2(xMin, y);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!info.isDead)
        {
            Move();
            Dash();

            if (playerNumber == 1)
            {
                JumpWithDouble();
            }
            else if (playerNumber == 2)
            {
                JumpWithFloat();
            }

            Shoot();
            LightMelee();

            if (info.Energy >= heavyMeleeCost)
            {
                if (playerNumber == 1)
                {
                    HeavyMeleeBsela();
                }
                else if (playerNumber == 2)
                {
                    HeavyMeleeDragon();
                }
            }

            if (info.Energy >= ultimateAttackCost)
            {
                UltimateAttack();
            }

            Block();
        }
        
        Die();
    }

    void Move()
    {
        myDirection = 0;

        if (controllerConnected)
        {
            myDirection = Input.GetAxis("Horizontal" + (playerNumber + ""));
        }
        else
        {
            if (Input.GetKey(right))
            {
                myDirection = 1;
            }
            else if (Input.GetKey(left))
            {
                myDirection = -1;
            }
        }

        var scale = transform.localScale;

        if (myDirection > 0)
        {
            var velocity = body.velocity;
            velocity.x = 10;
            body.velocity = velocity;
            facingDirection = 1;
            scale.x = Mathf.Abs(scale.x);
            myAnimator.SetBool("isWalking", true);
        }
        else if (myDirection < 0)
        {

            var velocity = body.velocity;
            velocity.x = -10;
            body.velocity = velocity;
            facingDirection = -1;
            scale.x = Mathf.Abs(scale.x) * facingDirection;
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            var velocity = body.velocity;
            velocity.x = 0;
            body.velocity = velocity;
            myAnimator.SetBool("isWalking", false);
        }

        transform.localScale = scale;
    }

    void JumpWithDouble()
    {
        if (Input.GetKeyDown(jump) && (isGrounded || canDoubleJump))
        {
            myAnimator.SetBool("jump", true);
            if (isGrounded == true && canDoubleJump == false)
            {
                
                body.gravityScale = jumpGravity;
                var velocity = body.velocity;
                velocity.y = 10;
                body.velocity = velocity;
                canDoubleJump = true;
                isGrounded = false;
                


            }
            else if (canDoubleJump == true)
            {
                body.gravityScale = jumpGravity;
                var velocity = body.velocity;
                velocity.y = 10;
                body.velocity = velocity;
                canDoubleJump = false;
            }
        }

        if (body.velocity.y <= 0)
        {
            print(body.velocity.y);
            myAnimator.SetBool("jump", false);
            myAnimator.SetBool("fall", true);
            body.gravityScale = fallGravity / 2.0f;
            for (int i = 0; i < numberOfRays; i++)
            {
                Debug.DrawRay(this.transform.position + (Vector3)rayCastDown[i], Vector3.down);
                var hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastDown[i], Vector3.down, .3f);
                if (hit)
                {
                    isGrounded = true;
                    myAnimator.SetBool("jump", false);
                    myAnimator.SetBool("fall", false);

                }
            }
        }
    }

    void JumpWithFloat()
    {
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            myAnimator.SetBool("jump", true);
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
        else if (Input.GetKeyDown(jump) && canFloat)
        {
            body.gravityScale = jumpGravity;
            var velocity = body.velocity;
            velocity.y = 7f;
            body.velocity = velocity;
            canFloat = false;
            isFloating = true;
            myAnimator.SetBool("canFloat", true);

        }
        else if (Input.GetKeyUp(jump) && isFloating)
        {
            isFloating = false;
            myAnimator.SetBool("canFloat", false);
        }

        if (body.velocity.y < 0)
        {
            myAnimator.SetBool("jump", false);
            if (isFloating == true)
            {
                body.gravityScale = floatGravity;
            }
            else
            {
                body.gravityScale = fallGravity;
                myAnimator.SetBool("fall", true);
            }

            for (int i = 0; i < numberOfRays; i++)
            {
                Debug.DrawRay(this.transform.position + (Vector3)rayCastDown[i], Vector3.down);
                //Debug.DrawRay(this.transform.position + (Vector3)rayCastRight[i], Vector3.right);
                var hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastDown[i], Vector3.down, .3f);
                if (hit)
                {
                    isGrounded = true;
                    isFloating = false;
                    canFloat = false;
                    myAnimator.SetBool("canFloat", false);
                    myAnimator.SetBool("isWalking", false);
                    myAnimator.SetBool("fall", false);
                }
            }
        }
        else if (body.velocity.y == 0.0)
        {
            isGrounded = true;
            isFloating = false;
            canFloat = false;
            myAnimator.SetBool("jump", false);
            myAnimator.SetBool("fall", false);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(fire))
        {
            if (myAnimator.GetBool("isAttacking") == false)
            {
                myAnimator.SetBool("isAttacking", true);
            }           

        }
        else if (Input.GetKeyUp(fire))
        {                        
            myAnimator.SetBool("isAttacking", false);

            Vector3 position = new Vector3();
            position = spawnProjectiles.transform.position;
            var newlyCreatedBsela = Instantiate(projectile, position, transform.rotation);
            Vector2 newlyCreatedObjectVelocity = newlyCreatedBsela.GetComponent<Rigidbody2D>().velocity;

            if (facingDirection > 0)
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

    void LightMelee()
    {
        if (Input.GetKeyDown(lightMelee))
        {            
            RaycastHit2D hit = new RaycastHit2D();
            for (int i = 0; i < numberOfRays; i++)
            {
                if (facingDirection == 1)
                {
                    Debug.DrawLine(this.transform.position + (Vector3)rayCastRight[i], this.transform.position + (Vector3)rayCastRight[i] + (Vector3.right * 0.7f));
                    hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastRight[i], Vector3.right, .7f);
                }
                else
                {
                    Debug.DrawLine(this.transform.position + (Vector3)rayCastLeft[i], this.transform.position + (Vector3)rayCastLeft[i] + (-Vector3.right * 0.7f));
                    hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastLeft[i], -Vector3.right, .7f);
                } 
            }

            if (hit)
            {
                if (hit.collider.tag == "Bsela" || hit.collider.tag == "Dragon")
                {
                    var controller = hit.collider.GetComponent<CharacterController>();
                    var info = GetComponent<PlayerInfo>();
                    var otherInfo = hit.collider.GetComponent<PlayerInfo>();

                    if (controller.isBlocking && otherInfo.Energy > 0)
                    {
                        otherInfo.AddHealth(-1);
                        otherInfo.AddEnergy(-5);
                        info.AddEnergy(5);
                    }
                    else
                    {
                        otherInfo.AddHealth(-5);
                        info.AddEnergy(5);
                    }
                }
            }
        }
    }

    void Block()
    {
        if (info.Energy > 0)
        {
            if (Input.GetKeyDown(block))
            {
                isBlocking = true;
            }
            else if (Input.GetKeyUp(block))
            {
                isBlocking = false;
            }
        }       
    }

    void HeavyMeleeBsela()
    {
        if (Input.GetKeyDown(heavyMelee))
        {
            info.AddEnergy(-heavyMeleeCost);
            carrotCollider.enabled = true;
        }
    }

    void HeavyMeleeDragon()
    {
        if (Input.GetKeyDown(heavyMelee))
        {
            info.AddEnergy(-heavyMeleeCost);

            shake.enabled = false;
            shake.shakeDuration = 0.5f;
            shake.enabled = true;

            RaycastHit2D hit = new RaycastHit2D();
            for (int i = 0; i < numberOfRays; i++)
            {
                if (facingDirection == 1)
                {
                    Debug.DrawLine(this.transform.position + (Vector3)rayCastRight[i], this.transform.position + (Vector3)rayCastRight[i] + (Vector3.right * 10));
                    hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastRight[i], Vector3.right, 10f);
                }
                else
                {
                    Debug.DrawLine(this.transform.position + (Vector3)rayCastLeft[i], this.transform.position + (Vector3)rayCastLeft[i] + (-Vector3.right * 10));
                    hit = Physics2D.Raycast(this.transform.position + (Vector3)rayCastLeft[i], -Vector3.right, 10);
                }
            }

            if (hit)
            {
                if (hit.collider.tag == "Bsela")
                {
                    var controller = hit.collider.GetComponent<CharacterController>();
                    var otherInfo = hit.collider.GetComponent<PlayerInfo>();

                    if (controller.isBlocking && otherInfo.Energy > 0)
                    {
                        otherInfo.AddHealth(-10);
                        otherInfo.AddEnergy(-15);
                    }
                    else
                    {
                        otherInfo.AddHealth(-15);
                    }
                }
            }
        }
    }

    void UltimateAttack()
    {
        if (Input.GetKey(ultimate1))
        {
            ultimateWaitCounter += Time.deltaTime;

            if (ultimateWaitCounter <= ultimateWaitTime)
            {
                if (Input.GetKeyDown(ultimate2))
                {
                    info.AddEnergy(-ultimateAttackCost);
                }                
            }
        }

        if (ultimateWaitCounter > ultimateWaitTime)
        {
            ultimateWaitCounter = 0;
        }
    }

    void Die()
    {
        if (info.Health == 0)
        {
            info.isDead = true;
        }
    }

   void Dash()
    {
        if (myAnimator == null)
        {
            return;
        }

        if (Input.GetKeyUp(dash))
        {
            isDashing = true;
            myAnimator.SetBool("dash", true);
            dashVel = body.velocity + (Vector2.right * facingDirection * dashForce);
            info.AddEnergy(-10);
        }

        if (isDashing)
        {
            var vel = Vector3.Lerp(body.velocity, dashVel, 1);
            dashTimeCounter += Time.deltaTime;
            body.velocity = vel;
        }

        //print(Vector3.Distance(transform.position, dashVel + (Vector3.right * facingDirection * dashDistance)));

        //print(body.velocity.x);
        if (isDashing && dashTimeCounter >= dashWaitTime)// Vector3.Distance(transform.position, dashVel + (Vector3.right * facingDirection * dashDistance)) <= 0.01f)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            isDashing = false;
            myAnimator.SetBool("dash", false);
            dashTimeCounter = 0;
        }
    }
}
