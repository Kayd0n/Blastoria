using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody rb;
    public Animator animator;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.Play("DropBomb");
            Debug.Log("Action pressed"); // Have to add the behavior to launch a bomb
        }
    }

    void FixedUpdate()
    {
        // float horizontalMovement = Input.GetAxis("Horizontal");
        // float verticalMovement = Input.GetAxis("Vertical");
        // float characterVelocity = Mathf.Abs(rb.velocity.x);

        // // transform.Translate(Vector3.forward * Time.deltaTime * verticalMovement);
        // // transform.Translate(-Vector3.right * Time.deltaTime * horizontalMovement);

        // // MovePlayerVertically(horizontalMovement);
        // // MovePlayer(verticalMovvement);
        // Debug.Log(characterVelocity);
        // animator.SetFloat("Speed", characterVelocity);
    }

    // void MovePlayerVertically(float verticalMovvement)
    // {
    //     Vector3 targetVelocity = new Vector3(verticalMovvement, rb.velocity.x);

    //     rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    // }

    // void MovePlayerHorizontally(float horizontalMovement)
    // {
    //     Vector3 targetVelocity = new Vector3(horizontalMovement, rb.velocity.y);

    //     rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    // }
}
