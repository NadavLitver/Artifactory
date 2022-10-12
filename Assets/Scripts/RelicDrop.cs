using System.Collections;
using UnityEngine;


public class RelicDrop : MonoBehaviour
{
    [SerializeField] Relic myRelic;
    [SerializeField] float dragTime;
    public Relic MyRelic { get => myRelic; }
    IProximityDetection proximityDetection;

    bool reached;
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
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        OnDragEnd();
    }


    private void OnDragEnd()
    {
        GameManager.Instance.assets.playerActor.PlayerRelicInventory.AddRelic(MyRelic);
        gameObject.SetActive(false);
    }

}
