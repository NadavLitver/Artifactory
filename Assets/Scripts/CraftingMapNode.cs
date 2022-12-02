using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class CraftingMapNode : MonoBehaviour
{
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] RecipeCoponent mycomponent;
    [SerializeField] Image line;
    [SerializeField] Transform connectionPoint;
    [SerializeField] GameObject cover;

    float rotation;
    bool discovered;
    public RecipeCoponent Mycomponent { get => mycomponent;}
    public Image Line { get => line; }
    public bool Discovered { get => discovered; set => discovered = value; }
    public float Rotation { get => rotation; set => rotation = value; }

    public UnityEvent OnDiscovered;
    private void Start()
    {
        OnDiscovered.AddListener(RemoveCover);
    }

    public void SetUpNode(RecipeCoponent givenComp)
    {
        mycomponent = givenComp;
        itemSprite.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(givenComp.itemType);
        textMesh.text = givenComp.amount.ToString();
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
        line.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
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
