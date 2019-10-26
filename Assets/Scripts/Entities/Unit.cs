using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public LayerMask groundLayerMask;
    Rigidbody2D rb;
    public float moveSpeed = 5;

    //Jump Vars
    public float jumpForce = 5;
    float halfHeight;
    readonly float jumpCooldown = .25f;
    float timeOfLastJump;

    protected bool isGrounded { get { return Physics2D.Raycast(transform.position - new Vector3(0, halfHeight, 0), -Vector2.up, .05f, groundLayerMask); } }
    public virtual void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        halfHeight = GetComponent<Collider2D>().bounds.extents.y;
    }

    public virtual void Jump()
    {
        if(Time.time - timeOfLastJump > jumpCooldown)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            timeOfLastJump = Time.time;
        }
    }
    public virtual void Move(Vector2 inputDir)
    {
        //Only side to side movement
        rb.AddForce(Vector2.left * inputDir.x * moveSpeed);
    }

    public virtual void PostInitialize()
    {

    }

    public virtual void Refresh()
    {

    }

    public virtual void PhysicsRefresh()
    {
       
    }

}
