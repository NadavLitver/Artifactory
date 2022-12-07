using UnityEngine;
[CreateAssetMenu(fileName = "New Crafted Item", menuName = "Crafted Item")]
public class CraftedItem : ScriptableObject
{
    [SerializeField] Sprite sprite;

    public Sprite Sprite { get => sprite; }
}
