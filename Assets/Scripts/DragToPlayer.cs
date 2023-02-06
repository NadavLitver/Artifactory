using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IProximityDetection))]
public class DragToPlayer : MonoBehaviour
{

    [SerializeField] float dragSpeed;
    IProximityDetection proximityDetection;

    private void Start()
    {
        proximityDetection = GetComponent<IProximityDetection>();
        proximityDetection.OnInProximity.AddListener(dragTowardsPlayer);
    }

    private void dragTowardsPlayer()
    {
        StartCoroutine(DragToplayerRoutine());
        proximityDetection.OnInProximity.RemoveListener(dragTowardsPlayer);
    }

    IEnumerator DragToplayerRoutine()
    {
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, GameManager.Instance.assets.Player.transform.position, counter);
            transform.position = positionLerp;
            counter += Time.deltaTime * dragSpeed;
            yield return new WaitForEndOfFrame();
        }
        OnDragEnd();
    }


    public virtual void OnDragEnd()
    {

    }

}