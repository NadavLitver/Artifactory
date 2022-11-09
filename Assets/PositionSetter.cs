using System.Collections;
using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float delayBetweenUpdates;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask layerToCheck;
    [SerializeField] Vector2 offset;
    public bool follow;
    //[SerializeField] List<Vector2> positions;
    public Vector2 smallestPoint;
    public Vector2 biggestPoint;


    public void Start()
    {
        follow = true;
        playerTransform = GameManager.Instance.assets.Player.transform;
        StartCoroutine(UpdateClosestInteractable());
    }
    IEnumerator UpdateClosestInteractable()
    {
        while (follow)
        {
           // yield return new WaitForSecondsRealtime(delayBetweenUpdates);
            Collider2D[] collidersFound = Physics2D.OverlapCircleAll(transform.position, checkRadius, layerToCheck);
            //Debug.Log("Found " + collidersFound.Length + " amount of enemies ");
            if (collidersFound.Length < 1)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition,  offset, Time.deltaTime * 2);
                yield return null;
            }
            else
            {
                smallestPoint = transform.position;
                biggestPoint = transform.position;
                foreach (var item in collidersFound)
                {
                    if (smallestPoint.x > item.transform.position.x)
                    {
                        smallestPoint.x = item.transform.position.x;
                    }
                    if (smallestPoint.y > item.transform.position.y)
                    {
                        smallestPoint.y = item.transform.position.y;
                    }
                    //
                    if (biggestPoint.x < item.transform.position.x)
                    {
                        biggestPoint.x = item.transform.position.x;
                    }
                    if (biggestPoint.y < item.transform.position.y)
                    {
                        biggestPoint.y = item.transform.position.y;
                    }
                }
                Vector2 finalPoint = new Vector2((smallestPoint.x + biggestPoint.x + playerTransform.position.x) / 3, (smallestPoint.y + biggestPoint.y + playerTransform.position.y) / 3);
                transform.position = Vector2.MoveTowards(transform.position, finalPoint, Time.deltaTime * 2);
              
                yield return null;

            }
            //positions.Clear();




        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
