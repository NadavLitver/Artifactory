using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CraftingMapNode : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] RecipeCoponent mycomponent;
    [SerializeField] GameObject line;
    public RecipeCoponent Mycomponent { get => mycomponent;}
    public GameObject Line { get => line; }

    public void SetUpNode(RecipeCoponent givenComp)
    {
        mycomponent = givenComp;
        //get sprite from gamemanager
        textMesh.text = givenComp.amount.ToString();
    }


}
