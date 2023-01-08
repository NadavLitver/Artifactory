using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BounderScout : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundCheckNew frontSensor;
    [SerializeField] private GroundCheckNew downSensor;
    [SerializeField] private float speed;
    public bool reached;

    public IEnumerator flyTowards(Vector3 direction, float timeOut)
    {
        frontSensor.AddSensor(new GroundCheckSensor() { Direcion = direction, Range = 1 });
        downSensor.AddSensor(new GroundCheckSensor() { Direcion = Vector3.down, Range = 1 });
        while (!frontSensor.IsGrounded() && downSensor.IsGrounded())
        {
            rb.velocity = direction * speed;
            yield return new WaitForEndOfFrame();
            if (direction == Vector3.right)
            {
                Debug.Log(transform.position);
            }
        }
        rb.velocity = Vector3.zero;
        reached = true;
    }



}
