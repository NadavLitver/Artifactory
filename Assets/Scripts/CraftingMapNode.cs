using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
public class CraftingMapNode : MonoBehaviour
{
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] RecipeCoponent mycomponent;
    [SerializeField] Image line;
    [SerializeField] Transform connectionPoint;
    [SerializeField] GameObject cover;
    [SerializeField] GameObject finalNodeBackground;
    [SerializeField] Image circle;

    [SerializeField] List<CraftingNodeConnection> nodeConnections = new List<CraftingNodeConnection>();
    private CustomPos myPos;

    float rotation;
    bool discovered;
    public RecipeCoponent Mycomponent { get => mycomponent; }
    public Image Line { get => line; }
    public bool Discovered { get => discovered; set => discovered = value; }
    public float Rotation { get => rotation; set => rotation = value; }
    public CustomPos MyPos { get => myPos; }
    public List<CraftingNodeConnection> NodeConnections { get => nodeConnections; set => nodeConnections = value; }
    public GameObject Cover { get => cover; }

    public UnityEvent OnDiscovered;
    private void Start()
    {
        OnDiscovered.AddListener(RemoveCover);
        ShufflePoints();
    }

    public void ShufflePoints()
    {
        int n = nodeConnections.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            CraftingNodeConnection value = nodeConnections[k];
            nodeConnections[k] = nodeConnections[n];
            nodeConnections[n] = value;
        }
    }
    public List<ConnectionPoints> GetAvailableConnectionPointsFromDirection(Vector3 direction, CraftingMapNode node)
    {
        List<ConnectionPoints> viableConnectionPoints = new List<ConnectionPoints>();
        List<ConnectionPoints> takenPoints = new List<ConnectionPoints>();

        foreach (var item in node.NodeConnections)
        {
            if (!item.Occupied)
            {
                viableConnectionPoints.Add(item.ConnectionPoint);
            }
        }
        
        return viableConnectionPoints;
    }

    public void SetPos(CustomPos givenPos)
    {
        myPos = givenPos;
    }

    public void SetUpNode(RecipeCoponent givenComp)
    {
        mycomponent = givenComp;
        itemSprite.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(givenComp.itemType);
       // textMesh.text = givenComp.amount.ToString();
        cover.SetActive(false);
    }
    public void SetUpNode(Sprite sprite)
    {
        itemSprite.sprite = sprite;
        cover.SetActive(false);
    }

    public void RotateLine(float givenBaseRotation)
    {
        rotation = givenBaseRotation;
        float finalRot = Random.Range(rotation - 15, rotation + 16);
        line.transform.rotation = Quaternion.Euler(0f, 0f, finalRot);
    }

    public void RotateAndSetFinalLine(float givenBaseRotation)
    {
        ((RectTransform)line.transform).sizeDelta = new Vector2(((RectTransform)line.transform).sizeDelta.x, ((RectTransform)line.transform).sizeDelta.y * 1.2f);
        rotation = givenBaseRotation;
        line.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        finalNodeBackground.SetActive(true);
        circle.gameObject.SetActive(false);
    }
    
    public Transform GetConnectionPoint(ConnectionPoints givenPoint)
    {
        foreach (var item in nodeConnections)
        {
            if (givenPoint == item.ConnectionPoint)
            {
                item.Occupied = true;
                return item.transform;
            }
        }
        return null;
    }

    public List<ConnectionPoints> GetAllowedPoints()
    {
        List<ConnectionPoints> allowedPoints = new List<ConnectionPoints>();
        foreach (var item in nodeConnections)
        {
            if (item.Occupied)
            {
                continue;
            }
            if (transform.localPosition.x > 0)
            {
                if (item.ConnectionPoint == ConnectionPoints.Right|| item.ConnectionPoint == ConnectionPoints.LowerRight || item.ConnectionPoint == ConnectionPoints.UpperRight)
                {
                    allowedPoints.Add(item.ConnectionPoint);
                }
            }
            else if (transform.localPosition.x < 0)
            {
                if (item.ConnectionPoint == ConnectionPoints.Left || item.ConnectionPoint == ConnectionPoints.LowerLeft || item.ConnectionPoint == ConnectionPoints.Left)
                {
                    allowedPoints.Add(item.ConnectionPoint);
                }
            }
        }

        return allowedPoints;
      
    }


    public void Discover()
    {
        OnDiscovered?.Invoke();
    }

    public void RemoveCover()
    {
        cover.SetActive(false);
    }

    public Vector3 CalacDistanceFromSpriteToConnectionPoint()
    {
        return GameManager.Instance.generalFunctions.CalcRangeV2(transform.position, connectionPoint.transform.position);

    }
}

