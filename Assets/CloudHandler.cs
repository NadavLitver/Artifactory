using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHandler : MonoBehaviour
{
    [SerializeField] Transform CloudStartingPos;
    [SerializeField] Transform CloudEndingPos;

    [SerializeField] Transform[] Clouds;

    [SerializeField] float cloudSpeed;
    private void Update()
    {
        for (int i = 0; i < Clouds.Length; i++)
        {
            Clouds[i].position = Vector2.MoveTowards(Clouds[i].position, CloudEndingPos.position, Time.deltaTime * cloudSpeed * Random.Range(0.5f, 3));
            if (GameManager.Instance.generalFunctions.IsInRange(Clouds[i].position, CloudEndingPos.position, 1f))
            {
                Clouds[i].position = CloudStartingPos.position;
            }
        }
    }
}
