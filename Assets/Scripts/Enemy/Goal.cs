using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Vector2 closestGoal;

    void FindClosestGoal()
    {
        if(transform.position.x < 0)
        {
            //najblizsza bramka to lewa
            closestGoal = new Vector2(-8.5f,0f);
        }
        else
        {
            //najblizsza bramka to prawa
            closestGoal = new Vector2(8.5f,0f); 
        }
    }

    void Update()
    {
        FindClosestGoal();
    }
}
