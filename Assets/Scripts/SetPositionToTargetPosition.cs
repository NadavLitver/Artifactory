using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionToTargetPosition : MonoBehaviour
{
    [SerializeField] Transform Target;
    private void Start()
    {
        transform.parent = null;
    }
    public void LateUpdate()
    {
        transform.position = Target.position;
    }
}
