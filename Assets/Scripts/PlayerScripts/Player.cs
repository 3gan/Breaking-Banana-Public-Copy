using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;

    public GameObject gameOverUI;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (PauseController.IsGamePaused)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("isWalking", false);
            return;
        }
        rb.velocity = moveInput * moveSpeed;
        anim.SetBool("isWalking", rb.velocity.magnitude > 0);
    }

    public void Move(InputAction.CallbackContext cont)
    {
        if (cont.canceled)
        {
            anim.SetBool("isWalking", false);
            anim.SetFloat("LastX", moveInput.x);
            anim.SetFloat("LastY", moveInput.y);
        }
        moveInput = cont.ReadValue<Vector2>();
        anim.SetFloat("InputX", moveInput.x);   
        anim.SetFloat("InputY", moveInput.y);
    }
}
