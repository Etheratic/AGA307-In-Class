using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float health = 100;
   
    //need to know if we are touching the ground
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        //checks if we are touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        //check input for player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //move the player
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        

    }

    public void Jump(int _jumpHeight)
    {
        velocity.y = Mathf.Sqrt(_jumpHeight * -2f * gravity);

    }

    public void Hit(int _damage)
    {
        health -= _damage;
        if (health < 0)
            _GM.gameState = GameState.GameOver;
        print("player health: " + health);

    }
}
