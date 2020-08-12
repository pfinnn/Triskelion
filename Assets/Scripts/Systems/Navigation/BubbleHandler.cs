using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleHandler : MonoBehaviour
{
    List<Vector3> obstaclesLeft = new List<Vector3>();
    List<Vector3> obstaclesRight = new List<Vector3>();

    Vector3 sensorFront;
    Vector3 sensorRight;
    Vector3 sensorLeft;
    Vector3 sensorBack;

    internal bool HasObstacles()
    {
        if (obstaclesLeft.Count != 0)
        {
            return true;
        }
        else if (obstaclesRight.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    internal Vector3 GetBestDirection(Vector3 agentPosition)
    {
        

        


        if ((obstaclesLeft.Count > 0 && obstaclesRight.Count == 0) || obstaclesLeft.Count < obstaclesRight.Count && obstaclesLeft.Count != 0)
        {
            // send left
            Vector3 closestObstacle = obstaclesLeft[0];
            float distanceAgentObstacle = Vector3.Distance(agentPosition, closestObstacle);
            // get closest obstacle

            for (int i = 1; i < obstaclesLeft.Count; i++)
            {
                Vector3 _obstacle = obstaclesLeft[i];
                float _distance = Vector3.Distance(agentPosition, _obstacle);

                if (distanceAgentObstacle > _distance)
                {
                    closestObstacle = _obstacle;
                    distanceAgentObstacle = _distance;
                }
            }

            Debug.DrawLine(agentPosition, closestObstacle);

            return closestObstacle;
            // determine angle left of it
            
        }
        else
        {
            // refractor into functions
            // send right
            Vector3 closestObstacle = obstaclesRight[0];
            float distanceAgentObstacle = Vector3.Distance(agentPosition, closestObstacle);
            // get closest obstacle
            for (int i = 1; i < obstaclesRight.Count; i++)
            {
                Vector3 _obstacle = obstaclesRight[i];
                float _distance = Vector3.Distance(agentPosition, _obstacle);

                if (distanceAgentObstacle > _distance)
                {
                    closestObstacle = _obstacle;
                    distanceAgentObstacle = _distance;
                }
            }
            Debug.DrawLine(agentPosition, closestObstacle);
            return closestObstacle;
        }
    }

    //List<Collider> obstacles = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        //bubble = GetComponent<Collider>();
        //Debug.Log(bubble.name + " " + bubble.bounds);
    }

    // Update is called once per frame
    void Update()
    {
        sensorFront = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        sensorRight = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        sensorLeft = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
        sensorBack = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);

        Debug.DrawLine(transform.position, sensorFront, Color.yellow);
        Debug.DrawLine(transform.position, sensorRight, Color.yellow);
        Debug.DrawLine(transform.position, sensorLeft, Color.yellow);
        Debug.DrawLine(transform.position, sensorBack, Color.yellow);
    }


    internal void HandleCollisionFromBubble(Collider other, Bubble bubble)
    {
        Debug.Log("Handling collision for " + bubble.name);

        Vector3 colliderPos = other.transform.position;

        switch (bubble.side)
        {
            case "right":
                // check if is empty to save perfo
            if (!obstaclesRight.Contains(colliderPos))
            {
                obstaclesRight.Add(colliderPos);
            }
            break;
            case "left":
            if (!obstaclesLeft.Contains(colliderPos))
            {
                obstaclesLeft.Add(colliderPos);
            }
            break; 
        }

    }



}
