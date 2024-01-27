// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using UnityEngine;

// public class Player : MonoBehaviour
// {
//     Rigidbody2D playerRigidbody;
    
//     [SerializeField]
//     float playerSpeed = 1,
//     stoppingPlayerDurationSeconds = 4;

    // [SerializeField] private AudioSource footstepSFX;
    // [SerializeField] private AudioSource playerHitSFX;
    // [SerializeField] private AudioSource wallHitSFX;
    
//     float stoppingPlayerLerpParameter;

//     bool isPlayerMovementKeyPressed;
//     bool hasStoppingCoroutineStarted = true;
//     bool isSoundPlayed;

//     void Awake()
//     {
//         stoppingPlayerLerpParameter = 1;
//         playerRigidbody = GetComponent<Rigidbody2D>();
//     }

//     void FixedUpdate()
//     {
//         MovePlayer();
//     }

//     void MovePlayer()
//     {
//         if(Input.GetKey(KeyCode.UpArrow))
//         {
//             StopCoroutine(GraduallyStopPlayer());
//             playerRigidbody.AddForce(playerSpeed * Vector2.up, ForceMode2D.Impulse);
//             isPlayerMovementKeyPressed = true;
//             hasStoppingCoroutineStarted = false;
//             if(!isSoundPlayed)
//             {
//                 StartCoroutine(PlayStepSound());
//             }

//         }
//         else if(Input.GetKey(KeyCode.DownArrow))
//         {
//             StopCoroutine(GraduallyStopPlayer());
//             playerRigidbody.AddForce(playerSpeed * Vector2.down, ForceMode2D.Impulse);
//             isPlayerMovementKeyPressed = true;
//             hasStoppingCoroutineStarted = false;
//             if(!isSoundPlayed)
//             {
//                 StartCoroutine(PlayStepSound());
//             }

//         }
//         else if(Input.GetKey(KeyCode.LeftArrow))
//         {
//             StopCoroutine(GraduallyStopPlayer());
//             playerRigidbody.AddForce(playerSpeed * Vector2.left, ForceMode2D.Impulse);
//             isPlayerMovementKeyPressed = true;
//             hasStoppingCoroutineStarted = false;
//             if(!isSoundPlayed)
//             {
//                 StartCoroutine(PlayStepSound());
//             }

//         }
//         else if(Input.GetKey(KeyCode.RightArrow))
//         {
//             StopCoroutine(GraduallyStopPlayer());
//             playerRigidbody.AddForce(playerSpeed * Vector2.right, ForceMode2D.Impulse);
//             isPlayerMovementKeyPressed = true;
//             hasStoppingCoroutineStarted = false;
//             if(!isSoundPlayed)
//             {
//                 StartCoroutine(PlayStepSound());
//             }

//         }
//         else
//         {
//             isPlayerMovementKeyPressed = false;
//         }

//         if(!isPlayerMovementKeyPressed && !hasStoppingCoroutineStarted)
//         {
//             StartCoroutine(GraduallyStopPlayer());
//         }
//     }

    // IEnumerator PlayStepSound()
    // {
    //     footstepSFX.Play();
    //     isSoundPlayed = true;
    //     yield return new WaitForSeconds(0.3f);
    //     isSoundPlayed = false;
    // }

    // public void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "CollisionTag")
    //     {
    //     playerHitSFX.Play();
    //     }
    //     if(collision.gameObject.tag == "WallCollision")
    //     {
    //         wallHitSFX.Play();
    //     }
    // }

//     IEnumerator GraduallyStopPlayer()
//     {
//         stoppingPlayerLerpParameter = 1;
//         hasStoppingCoroutineStarted = true;
//         Vector2 currentPlayerVelocity = playerRigidbody.velocity;
//         float fictionalFrameCount = 40;

//         while(stoppingPlayerLerpParameter > 0.05f)
//         {
//             playerRigidbody.velocity = Vector2.Lerp(currentPlayerVelocity,Vector2.zero,1 - stoppingPlayerLerpParameter);
//             stoppingPlayerLerpParameter -= 1/fictionalFrameCount;
//             yield return new WaitForSeconds(stoppingPlayerDurationSeconds/fictionalFrameCount);
//         }

//         playerRigidbody.velocity = Vector2.zero;
//     }
// }
