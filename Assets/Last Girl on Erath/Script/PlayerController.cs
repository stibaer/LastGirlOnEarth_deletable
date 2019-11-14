using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



                [SerializeField] float m_speed = 1.0f;
                [SerializeField] float m_jumpForce = 2.0f;

                private Animator m_animator;
                private Rigidbody2D m_body2d;
                private Character_Sensor m_groundSensor;
                private bool m_grounded = false;
                
                




                                        // Start is called before the first frame update
                                        void Start()
                                        {

                                            m_animator = GetComponent<Animator>();
                                            m_body2d = GetComponent<Rigidbody2D>();
                                            m_groundSensor = transform.Find("GroundSensor").GetComponent<Character_Sensor>();
                                        }


    // Update is called once per frame
    void FixedUpdate()
    {
                                        //Ground
                                        if (!m_grounded && m_groundSensor.State())
                                        {
                                            m_grounded = true;
                                            m_animator.SetBool("Grounded", m_grounded);
                                        }

                                        //Falling
                                        if (m_grounded && !m_groundSensor.State())
                                        {
                                            m_grounded = false;
                                            m_animator.SetBool("Grounded", m_grounded);
                                        }

                                        //Movement
                                        float inputX = Input.GetAxis("Horizontal");


                                        // Move
                                        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

                                        //Airspeed

                                        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);



                                        //Direction
                                        if (inputX > 0)
                                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                                        else if (inputX < 0)
                                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        

                                 
            

                                        //Jump

                                        else if (Input.GetKeyDown("space") && m_grounded) 
                                        {

                                            m_animator.SetTrigger("Jump");
                                            m_grounded = false;
                                            m_animator.SetBool("Grounded", m_grounded);
                                            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                                            m_groundSensor.Disable(0.2f);
                                        }

                                        //Run
                                        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                                            m_animator.SetInteger("AnimState", 2);





                                        

    }

}  
    

      

      

    

