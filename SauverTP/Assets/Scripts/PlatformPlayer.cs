using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    public class PlatformPlayer : Player
    {

        [SerializeField] private float m_MaxSpeed = 0f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character


        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = 1f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private bool noOxygen;
        private bool gameOver;

        public Slider healthSlider;
        public Slider foodSlider;
        public Slider oxygenSlider;
        public Slider fuelSlider;
        public Slider waterSlider;

        public SpriteRenderer pressSprite;
        public Animator animator;

        private Chronometer decChrono;
        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            decChrono = new Chronometer();
            decChrono.Start();
            GameController controller = new GameController();
            controller.playerHealth = 100;
            controller.playerFood = 100;
            Planet planet = GameController.Instance.currentPlanet;

            noOxygen = !planet.withOxigen();
            playerFood = controller.playerFood;
            playerHealth = controller.playerHealth;
            playerFuel = controller.playerFuel;
            playerWater = controller.playerWater;
            playerOxygen = controller.playerWater;

            foodSlider.maxValue = controller.maxFood;
            fuelSlider.maxValue = controller.maxFuel;
            waterSlider.maxValue = controller.maxWater;
            oxygenSlider.maxValue = controller.maxOxygen;
            healthSlider.maxValue = controller.maxHealth;
        }

        private void FixedUpdate()
        {
            if (gameOver) return;

            pressSprite.enabled = false;

            decChrono.Update();
            if (decChrono.getTime() >= 1)
            {
                playerWater = max(0, playerWater - 2);
                if (noOxygen) playerOxygen = max(0, playerOxygen - 3);
                playerFood = max(0, playerFood - 1);
                decChrono.Reset();
                decChrono.Start();

                if (playerOxygen <= 0 || playerFood <= 0 || playerWater <= 0)
                {
                    float delta = playerFood <= 0 ? 0.01f : 0;
                    if (playerOxygen <= 0)
                    {
                        delta += 0.1f;
                    }
                    if (playerWater <= 0)
                    {
                        delta += 0.05f;
                    }
                    playerHealth -= delta;
                    if (playerHealth < 0)
                    {
                        playerHealth = 0;
                    }
                }
                updateUI();
            }

            if (playerHealth <= 0)
            {
                GameOver();
            }
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject == gameObject) continue;

                if (isGround(colliders[i].gameObject))
                {
                    m_Grounded = true;
                }
                else if (isObject(colliders[i].gameObject))
                {
                    pressSprite.enabled = true;
                    if (Input.GetKey("e"))
                    {
                        handleObject(colliders[i].gameObject.name);
                        Destroy(colliders[i].gameObject);
                    }

                }
            }

            
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, m_FacingRight ? Vector2.right : Vector2.left, 25);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider != null)
                {
                    if (isObject(hit.collider.gameObject))
                    {
                        pressSprite.enabled = true;
                        pressSprite.flipX = !m_FacingRight;

                        if (Input.GetKey("e"))
                        {
                            handleObject(hit.collider.gameObject.name);
                            Destroy(hit.collider.gameObject);
                        }

                    }

                }
            }

            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

           
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        private bool isGround(GameObject gameObject)
        {
            return !isObject(gameObject);
        }

        private bool isObject(GameObject gameObject)
        {
            return gameObject.name.Contains("Objet");
        }

        private void handleObject(String name)
        {
            int value = Int32.Parse(name.Substring(name.IndexOf('=') + 1));
            if (name.Contains("oxygen"))
            {
                playerOxygen += value;
                
            } else if (name.Contains("food"))
            {
                playerFood += value;
            } else if (name.Contains("water"))
            {
                playerWater += value;
            } else if (name.Contains("fuel"))
            {
                playerFuel += value;
            }

        }

        private int max(int a, int b)
        {
            return a >= b ? a : b;
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        void GameOver()
        {
        //    gameOver = true;
            //TODO
        }


        void updateUI()
        {
            fuelSlider.value = playerFuel;
            oxygenSlider.value = playerOxygen;
            waterSlider.value = playerWater;
            foodSlider.value = playerFood;
            healthSlider.value = playerHealth;
        }
    }
}
