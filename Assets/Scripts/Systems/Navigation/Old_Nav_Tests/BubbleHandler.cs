using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BubbleHandler : MonoBehaviour
{
    List<Vector3> obstaclesLeft = new List<Vector3>();
    List<Vector3> obstaclesRight = new List<Vector3>();

    Vector3 sensorFront;
    Vector3 sensorRight;
    Vector3 sensorLeft;
    Vector3 sensorBack;

    Transform target;

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

    internal void SetTarget(Transform _target)
    {
        target = _target;
    }

    internal Vector3 GetBestDirection(Vector3 agentPosition, Vector3 targetPostion, Vector3 currentDirection)
    {
        

        // check front higher weighted
        var yes = currentDirection;
        // shoot two rays of left and right side based width of the agents collider

        if ((obstaclesLeft.Count > 0 && obstaclesRight.Count == 0) || obstaclesLeft.Count < obstaclesRight.Count && obstaclesLeft.Count != 0)
        {
            // send left
            Vector3 closestObstacle = obstaclesLeft[0];
            float distanceAgentObstacle = Vector3.Distance(agentPosition, closestObstacle);
            // get closest obstacle

            for (int i = 1; i < obstaclesLeft.Count; i++)
            { 
                // determine closest to player and target
                Vector3 _obstacle = obstaclesLeft[i];
                float _distance = Vector3.Distance(agentPosition, _obstacle) + Vector3.Distance(targetPostion, _obstacle); //calcu weight var for dist to target
                if (distanceAgentObstacle < _distance)
                {
                    closestObstacle = _obstacle;
                    distanceAgentObstacle = _distance;
                }
            }

            Debug.DrawLine(agentPosition, closestObstacle);

            float angle = Vector3.Angle(closestObstacle, targetPostion);
            Vector3 dir = ApplyRotationToVector(targetPostion - closestObstacle, angle);
            return dir;
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
                // determine closest to player and target

                Vector3 _obstacle = obstaclesRight[i];
                float _distance = Vector3.Distance(agentPosition, _obstacle) + Vector3.Distance(targetPostion, _obstacle); // MIN ?calc weight var for dist to target

                if (distanceAgentObstacle < _distance)
                {
                    closestObstacle = _obstacle;
                    distanceAgentObstacle = _distance;
                }
            }
            Debug.DrawLine(agentPosition, closestObstacle);

            // calculate smart guess based on angles
            float angle = Vector3.Angle(closestObstacle, targetPostion);
            Vector3 dir = ApplyRotationToVector(targetPostion - closestObstacle, angle);
            return dir;
            
        }
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

    internal void HandleCollisionStay(Collider other, Bubble bubble)
    {
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

    internal void HandleCollisionExit(Collider other, Bubble bubble)
    {
        Vector3 colliderPos = other.transform.position;

        switch (bubble.side)
        {
            case "right":
                    obstaclesRight.Remove(colliderPos);
                break;
            case "left":
                    obstaclesLeft.Remove(colliderPos);
                break;
        }
    }

}
