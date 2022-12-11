using UnityEngine;
public abstract class CraftedItem : ScriptableObject
{
    [SerializeField] protected Sprite sprite;

    public Sprite Sprite { get => sprite; }

    public abstract void SetUp();

    public abstract void Obtain();
}
