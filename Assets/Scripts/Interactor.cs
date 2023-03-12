using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] float delayBetweenUpdates;

    [SerializeField] float checkRadius;

    [SerializeField] Collider2D closestInteractable;

    [SerializeField] LayerMask layerToCheck;

    public Collider2D ClosestInteractable { get => closestInteractable; set => closestInteractable = value; }
    private MobileControlsHandler mobileButtonsRef;

    void Start()
    {
        StartCoroutine(UpdateClosestInteractable());
        GameManager.Instance.inputManager.OnInteract.AddListener(Interact);
        mobileButtonsRef = GameManager.Instance.assets.mobileButtonHandler;
    }

    public void Interact()
    {
        if (!closestInteractable.gameObject.activeInHierarchy)
            closestInteractable = null;

        if (closestInteractable != null )
        {
            
            closestInteractable.GetComponent<Interactable>().Interact();
        }
    }

    IEnumerator UpdateClosestInteractable()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSecondsRealtime(delayBetweenUpdates);
            Collider2D[] collidersFound = Physics2D.OverlapCircleAll(transform.position, checkRadius, layerToCheck);
            foreach (var item in collidersFound)
            {
                IProximityDetection proximity = item.GetComponent<IProximityDetection>();
                if (!ReferenceEquals(proximity, null))
                {
                    proximity.OnInProximity?.Invoke();
                    
                }
            }
            if (collidersFound.Length <= 0)
            {
                closestInteractable = null;
                mobileButtonsRef.SetAttack();

            }
            else
            {
                mobileButtonsRef.SetInteractable();
            }
            foreach (var item in collidersFound)
            {
                if (!item.gameObject.CompareTag("Interactable"))
                {
                    continue;
                }
                if (closestInteractable == null)
                {
                    closestInteractable = item;
                }
                else if (GameManager.Instance.generalFunctions.CalcRange(transform.position, item.transform.position) < GameManager.Instance.generalFunctions.CalcRange(transform.position, closestInteractable.transform.position))
                {
                    closestInteractable = item;
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
