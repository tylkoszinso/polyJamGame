using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;

    [SerializeField]
    float maxDisplacement = 0.06f,
    playerPersonalSpaceRadius = 2f,
    clostEnoughtToDestinationRadius = 0.5f,
    enemyReactionTime = 0.2f,
    dashStrenght = 10;

    bool isEnemyDashing;    

    UnityEngine.Vector2 playerToGoalDirection;
    UnityEngine.Vector2 playerPosition;
    UnityEngine.Vector2 enemyPosition;
    UnityEngine.Vector2 currentBallDestination;
    UnityEngine.Vector2 closestGoalPosition;
    UnityEngine.Vector2 goalPosition;

    void OnEnable()
    {
        player = GameObject.Find("Player");
        closestGoalPosition = GameObject.FindObjectOfType<Goal>().closestGoal;
        playerPersonalSpaceRadius += clostEnoughtToDestinationRadius;
    }

    void FixedUpdate()
    {
        closestGoalPosition = GameObject.FindObjectOfType<Goal>().closestGoal;   
        CalculateObjectsPositions();
        
        if(!EnemyReachedPlayerPersonalSpace() || !EnemyReachedDashPosition())
        {
            StartCoroutine(SetPositionForDash());
        }
        else if(!isEnemyDashing && EnemyReachedDashPosition())
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isEnemyDashing = true;
        GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        GetComponent<Rigidbody2D>().AddForce(playerToGoalDirection * dashStrenght, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.2f);
        GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero;
        isEnemyDashing = false;
    }

    IEnumerator SetPositionForDash()
    {
        yield return new WaitForSeconds(enemyReactionTime);
        transform.position = UnityEngine.Vector2.MoveTowards(transform.position,currentBallDestination,maxDisplacement);
    }

    bool EnemyReachedDashPosition()
    {
        return (UnityEngine.Vector2.Distance(enemyPosition,currentBallDestination) < clostEnoughtToDestinationRadius);
    }

    bool EnemyReachedPlayerPersonalSpace()
    {
        return UnityEngine.Vector2.Distance(enemyPosition,playerPosition) < playerPersonalSpaceRadius;
    }

    void CalculateObjectsPositions()
    {
        playerPosition = new UnityEngine.Vector2(player.transform.position.x,player.transform.position.y);
        enemyPosition = new UnityEngine.Vector2(transform.position.x,transform.position.y);

        goalPosition = closestGoalPosition;

        // Calculate player To goal vector.
        playerToGoalDirection = (goalPosition - playerPosition).normalized * playerPersonalSpaceRadius;

        currentBallDestination = playerPosition - playerToGoalDirection;
        Debug.DrawLine(currentBallDestination,currentBallDestination + playerToGoalDirection);
    }
}
