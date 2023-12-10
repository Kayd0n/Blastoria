using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public Animator animator;
    public bool isPlayerOne = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isPlayerOne && Input.GetKeyDown(KeyCode.E))
        {
            Vector3 playerPos = transform.position;

            animator.Play("DropBomb");
            BombBehaviour.InstantiateBomb(playerPos, 4);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("DropBomb") && !animator.IsInTransition(0))
            {
                animator.Play("Idle");
            }

        }
        else if (!isPlayerOne && Input.GetKeyDown(KeyCode.RightControl))
        {
            Vector3 playerPos = transform.position;

            animator.Play("DropBomb");
            BombBehaviour.InstantiateBomb(playerPos, 4);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("DropBomb") && !animator.IsInTransition(0))
            {
                animator.Play("Idle");
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerOne)
        {
            MovePlayerOne();
        }
        else
        {
            MovePlayerTwo();
        }
    }

    private void MovePlayerOne()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        //set the animation on walk if the player is walking
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S))
        {
            animator.Play("PlayerWalk");
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk") && !animator.IsInTransition(0))
            {
                animator.Play("Idle");
            }
        }
    }

    private void MovePlayerTwo()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        //set the animation on walk if the player is walking
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            animator.Play("PlayerWalk");
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalk") && !animator.IsInTransition(0))
            {
                animator.Play("Idle");
            }
        }
    }
    public void Die(GameObject player)
    {
        if (isPlayerOne)
        {
            Debug.Log("Player 1 died");
            Destroy(player);
            SceneManager.LoadSceneAsync("PlayerTwoWin");
        }
        else
        {
            Debug.Log("Player 2 died");
            Destroy(player);
            SceneManager.LoadSceneAsync("PlayerOneWin");
        }
    }
}