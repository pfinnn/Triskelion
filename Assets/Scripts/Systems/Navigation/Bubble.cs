using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bubble : MonoBehaviour
{
    Collider bubble;
    int segments;
    Vector3 sensorFront;
    Vector3 sensorRight;

    //List<Collider> obstacles = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        sensorFront = transform.forward;
        sensorRight = transform.right + new Vector3(6, 0, 0);
        bubble = GetComponent<Collider>();
        Debug.Log(bubble.name + " " + bubble.bounds);
    }

    // Update is called once per frame
    void Update()
    {
        sensorFront = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
        sensorRight = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);

        //Debug.DrawLine(transform.position, sensorFront, Color.blue);
        //Debug.DrawLine(transform.position, sensorRight, Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "has entered bubble");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Dot(sensorFront, other.transform.position) > 0 && Vector3.Dot(sensorRight, other.transform.position) > 0)
        {
            Debug.DrawLine(transform.position, other.transform.position, Color.red);
        }

        // check if obstacle is behind agent
        //bool isBehind = Vector3.Dot(transform.position, sensorFront) < 0;
        //if (isBehind)
        //{
        //    Debug.DrawLine(transform.position, other.transform.position, Color.blue);
        //}
        // obstacle is left of agent
        //if (Vector3.Dot(other.transform.position, sensorLeft) > 0 && !isBehind)
        //{
        //    Debug.DrawLine(transform.position, other.transform.position, Color.blue);
        //}
        //// obstacle is right of agent
        //else if (Vector3.Dot(other.transform.position, sensorRight) > 0 )
        //{
        //    Debug.DrawLine(transform.position, other.transform.position, Color.red);
        //}



    }
}
