using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IProximityDetection))]
public class DragToPlayer : MonoBehaviour
{
    [SerializeField] float dragSpeed;
    [SerializeField] private Vector2 tweanOffset;
    [SerializeField] private float tweanTime;
    IProximityDetection proximityDetection;

    private void Start()
    {
        proximityDetection = GetComponent<IProximityDetection>();
        TweanObject();
        dragTowardsPlayer();
        //proximityDetection.OnInProximity.AddListener(dragTowardsPlayer);
    }

    private void dragTowardsPlayer()
    {
        StartCoroutine(DragToplayerRoutine());
       // proximityDetection.OnInProximity.RemoveListener(dragTowardsPlayer);
    }

    IEnumerator DragToplayerRoutine()
    {
        yield return new WaitForSecondsRealtime(tweanTime);
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

    public virtual void TweanObject()
    {
        Vector2 offset = tweanOffset;
        if ((GameManager.Instance.assets.playerActor.transform.position.x - transform.position.x) > 0)
        {
            offset.x *= -1;
        }
        Vector3 dest = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y);
        LeanTween.move(gameObject, dest, tweanTime).setEaseOutCirc();
    }


    public virtual void OnDragEnd()
    {

    }

}