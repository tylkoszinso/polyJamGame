using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    
    [SerializeField]
    float playerSpeed = 1,
    stoppingPlayerDurationSeconds = 4;
    
    float stoppingPlayerLerpParameter;

    bool isPlayerMovementKeyPressed;
    bool hasStoppingCoroutineStarted = true;

    void Awake()
    {
        stoppingPlayerLerpParameter = 1;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.up, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;

        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.down, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;

        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.left, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;

        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.right, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;

        }
        else
        {
            isPlayerMovementKeyPressed = false;
        }

        if(!isPlayerMovementKeyPressed && !hasStoppingCoroutineStarted)
        {
            StartCoroutine(GraduallyStopPlayer());
        }
    }

    IEnumerator GraduallyStopPlayer()
    {
        stoppingPlayerLerpParameter = 1;
        hasStoppingCoroutineStarted = true;
        Vector2 currentPlayerVelocity = playerRigidbody.velocity;
        float fictionalFrameCount = 40;

        while(stoppingPlayerLerpParameter > 0.05f)
        {
            playerRigidbody.velocity = Vector2.Lerp(currentPlayerVelocity,Vector2.zero,1 - stoppingPlayerLerpParameter);
            stoppingPlayerLerpParameter -= 1/fictionalFrameCount;
            yield return new WaitForSeconds(stoppingPlayerDurationSeconds/fictionalFrameCount);
        }

        playerRigidbody.velocity = Vector2.zero;
    }
}
