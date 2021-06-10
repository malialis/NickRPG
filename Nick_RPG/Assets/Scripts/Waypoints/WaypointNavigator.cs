using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;

public class WaypointNavigator : MonoBehaviour
{
    private CharacterNavigationController controller;
    public Waypoint currentWaypoint;

    private int direction;
    
    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();
    }

    private void Start()
    {
        if(controller.canMove)
        controller.SetTarget(currentWaypoint.GetPosition());

        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
    }

    private void Update()
    {
        if (controller.IsReached())
        {
            bool shouldBranch = false;

            if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }

            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {
                    if(currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        direction = 1;
                    }
                    
                }
                else if (direction == 1)
                {
                    if(currentWaypoint.previousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;
                    }
                    
                }
            }           
            
            controller.SetTarget(currentWaypoint.GetPosition());
        }
    }



}
