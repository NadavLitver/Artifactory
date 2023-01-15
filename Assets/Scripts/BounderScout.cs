using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BounderScout : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SensorGroup frontSensor;
    [SerializeField] private SensorGroup downSensor;
    [SerializeField, Range(1,10)] private float speed = 10;
    public bool reached;

    public IEnumerator flyTowards(Vector3 direction, float timeOut)
    {
        frontSensor.AddSensor(new GroundCheckSensor() { Direcion = direction, Range = 1 });
        downSensor.AddSensor(new GroundCheckSensor() { Direcion = Vector3.down, Range = 1 });
        while (!frontSensor.IsGrounded() && downSensor.IsGrounded())
        {
            rb.velocity = direction * speed;
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = Vector3.zero;
        reached = true;
    }



}
