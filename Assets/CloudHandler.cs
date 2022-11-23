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
            Clouds[i].position = new Vector2(Mathf.MoveTowards(Clouds[i].position.x, CloudEndingPos.position.x, Time.deltaTime * Random.Range(0.2f, 4f)), Clouds[i].position.y);
            if (GameManager.Instance.generalFunctions.IsInRange(Clouds[i].position, CloudEndingPos.position, 1f))
            {
                Clouds[i].position = CloudStartingPos.position;
            }
        }
    }
}
