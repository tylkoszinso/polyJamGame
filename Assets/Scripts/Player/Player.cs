using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Color walkingColor;
    public Color rollingColor;

    Rigidbody2D playerRigidbody;
    
    [SerializeField]
    float playerSpeed = 1,
    stoppingPlayerDurationSeconds = 4,
    entryRollingVelocity = 20;

    [SerializeField]
    float playerWalkEntrySpeed = 1,
    playerWalkSpeedAddition = 0.03f;

    float playerWalkSpeedUp = 1;
    float playerWalkSpeedDown = 1;
    float playerWalkSpeedLeft = 1;
    float playerWalkSpeedRight = 1;
    
    float stoppingPlayerLerpParameter;

    bool isPlayerMovementKeyPressed;
    bool hasStoppingCoroutineStarted = true;
    bool shouldIStartRolling;

    bool isSoundPlayed;

    [SerializeField] private AudioSource footstepSFX;
    [SerializeField] private AudioSource playerHitSFX;
    [SerializeField] private AudioSource wallHitSFX;

    void Awake()
    {
        walkingColor = Color.green;
        rollingColor = Color.red;

        stoppingPlayerLerpParameter = 1;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if((playerRigidbody.velocity.magnitude < entryRollingVelocity))
        {
            WalkPlayer();
            GetComponent<Renderer>().material.color = walkingColor;
        }
        else
        {
            shouldIStartRolling = true;
        }

        if(shouldIStartRolling)
        {
            GetComponent<Renderer>().material.color = rollingColor;
            RollPlayer();
        }
    }

    void WalkPlayer()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            playerWalkSpeedDown = playerWalkEntrySpeed;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x,playerWalkSpeedUp);
            playerWalkSpeedUp += playerWalkSpeedAddition;
            isPlayerMovementKeyPressed = true;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            playerWalkSpeedUp = playerWalkEntrySpeed;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x,-playerWalkSpeedDown);
            playerWalkSpeedDown += playerWalkSpeedAddition;
            isPlayerMovementKeyPressed = true;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            playerWalkSpeedRight = playerWalkEntrySpeed;
            playerRigidbody.velocity = new Vector2(-playerWalkSpeedLeft,playerRigidbody.velocity.y);
            playerWalkSpeedLeft += playerWalkSpeedAddition;
            isPlayerMovementKeyPressed = true;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            playerWalkSpeedLeft = playerWalkEntrySpeed;
            playerRigidbody.velocity = new Vector2(playerWalkSpeedRight,playerRigidbody.velocity.y);
            playerWalkSpeedRight += playerWalkSpeedAddition;
            isPlayerMovementKeyPressed = true;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }
        }
        if(!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            isPlayerMovementKeyPressed = false;
        }

        // if(Input.GetKeyUp(KeyCode.UpArrow))
        // {
        //     playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x,0);
        // }
        // if(Input.GetKeyUp(KeyCode.DownArrow))
        // {
        //     playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x,0);
        // }
        // if(Input.GetKeyUp(KeyCode.LeftArrow))
        // {
        //     playerRigidbody.velocity = new Vector2(0,playerRigidbody.velocity.y);
        // }
        // if(Input.GetKeyUp(KeyCode.RightArrow))
        // {
        //     playerRigidbody.velocity = new Vector2(0,playerRigidbody.velocity.y);
        // }

        if(!isPlayerMovementKeyPressed)
        {
            playerWalkSpeedRight = playerWalkSpeedLeft = playerWalkSpeedUp = playerWalkSpeedDown = 1;
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    void RollPlayer()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.up, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }

        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.down, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }

        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.left, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }

        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            playerRigidbody.AddForce(playerSpeed * Vector2.right, ForceMode2D.Impulse);
            isPlayerMovementKeyPressed = true;
            hasStoppingCoroutineStarted = false;
            if(!isSoundPlayed)
            {
                StartCoroutine(PlayStepSound());
            }
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

    IEnumerator PlayStepSound()
    {
        footstepSFX.Play();
        isSoundPlayed = true;
        print("walking sound started");
        yield return new WaitForSeconds(0.3f);
        isSoundPlayed = false;
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
        shouldIStartRolling = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CollisionTag")
        {
            playerHitSFX.Play();
        }
        if(collision.gameObject.tag == "WallCollision")
        {
            wallHitSFX.Play();
        }
    }
}
