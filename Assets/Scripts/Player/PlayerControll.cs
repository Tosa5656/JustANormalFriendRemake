using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
	[SerializeField] private int WalkSpeed = 5;
	[SerializeField] private int RunSpeed = 10;
	[SerializeField] private int JumpPower = 200;
	[SerializeField] private bool Grounded;
	[SerializeField] private Rigidbody rb;
	
    void FixedUpdate()
    {
        Walk();
		Jump();
    }
	
	private void Walk()
    {
        Vector3 velocity = new Vector3();

        if(Input.GetKey(KeyCode.W))
        {
            velocity += transform.forward * WalkSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.S))
        {
            velocity -= transform.forward * WalkSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.A))
        {
            velocity -= transform.right * WalkSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D))
        {
            velocity += transform.right * WalkSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            velocity *= RunSpeed;
        }
		else
        {
            velocity *= WalkSpeed;
        }

        velocity.y = rb.velocity.y;
        
        rb.velocity = velocity;

    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            rb.AddForce(transform.up * JumpPower);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Grounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Grounded = false;
        }
    }
}
