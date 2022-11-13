using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionToTargetPosition : MonoBehaviour
{
    [SerializeField] Transform Target;
    public void Update()
    {
        transform.position = Target.position;
    }
}
