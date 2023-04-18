using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CraftingMapNode : MonoBehaviour
{
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] RecipeCoponent mycomponent;
    [SerializeField] Image line;
    [SerializeField] Transform connectionPoint;
    [SerializeField] GameObject cover;
    [SerializeField] Image finalNodeBackground;
    [SerializeField] Image circle;

    [SerializeField] Sprite selectedLineSprite;
    Sprite lineSprite;
    [SerializeField] Sprite selectedNodeSprite;
    Sprite nodeSprite;
    [SerializeField] Sprite finalNodeSelectedSprite;
    Sprite finalNodeSprite;


    [SerializeField] List<CraftingNodeConnection> nodeConnections = new List<CraftingNodeConnection>();
    private CustomPos myPos;

    float rotation;
    bool discovered;
    public RecipeCoponent Mycomponent { get => mycomponent; }
    public Image Line { get => line; }
    public bool Discovered { get => discovered; set => discovered = value; }
    public float Rotation { get => rotation; set => rotation = value; }
    public CustomPos MyPos { get => myPos; }
    public List<CraftingNodeConnection> NodeConnections { get => nodeConnections; }
    public GameObject Cover { get => cover; }
    public Image ItemSprite { get => itemSprite; set => itemSprite = value; }

    public UnityEvent OnDiscovered;
    private void Start()
    {
        OnDiscovered.AddListener(RemoveCover);
        ShufflePoints();
        finalNodeSprite = finalNodeBackground.sprite;
        lineSprite = line.sprite;
        nodeSprite = circle.sprite;
    }

    public void SelectedColor()
    {
        finalNodeBackground.sprite = finalNodeSelectedSprite;
        line.sprite = selectedLineSprite;
        circle.sprite = selectedNodeSprite;
    }
    public void UnSelectedColor()
    {
        finalNodeBackground.sprite = finalNodeSprite;
        line.sprite = lineSprite;
        circle.sprite = nodeSprite;
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

    public List<ConnectionPoints> GetAdjacentConnections()
    {
        foreach (var item in nodeConnections)
        {
            if (item.Occupied)
            {
                return item.GetAdjacentConnectionPoints(item.ConnectionPoint);
            }
        }
        return null;
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
        float finalRot = Random.Range(rotation - 9, rotation + 11);
        line.transform.rotation = Quaternion.Euler(0f, 0f, finalRot);
    }

    public void RotateAndSetFinalLine(float givenBaseRotation)
    {
        ((RectTransform)line.transform).sizeDelta = new Vector2(((RectTransform)line.transform).sizeDelta.x, ((RectTransform)line.transform).sizeDelta.y * 1.2f);
        rotation = givenBaseRotation;
        line.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        finalNodeBackground.gameObject.SetActive(true);
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
                if (item.ConnectionPoint == ConnectionPoints.Right || item.ConnectionPoint == ConnectionPoints.LowerRight || item.ConnectionPoint == ConnectionPoints.UpperRight)
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

