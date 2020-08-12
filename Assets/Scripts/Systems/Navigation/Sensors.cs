using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    public bool debug = false;
    int sensorAmount = 6;
    float sensorRange = 18f;
    Vector3 rangeDir;
    int segments;
    List<Vector3> bubble = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        rangeDir = new Vector3(sensorRange, 0, sensorRange);

        //segments = Mathf.RoundToInt(180 / sensorAmount);
        segments = sensorAmount;
        for (int s = 0; s < segments+1; s++)
        {
            var rad = Mathf.Deg2Rad * (s * 180f / segments);
            Vector3 sensor = new Vector3(Mathf.Sin(rad) * sensorRange , 0, (Mathf.Cos(rad) * sensorRange));
            //sensor = sensor.normalized;
            bubble.Add(sensor);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Vector3 s in bubble)
        {
            if (debug)
            {
                Debug.DrawLine(transform.position, s, Color.yellow);
            }
        }
    }
}
