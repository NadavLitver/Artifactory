using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPickup : DragToPlayer
{
    private ZooAnimal refAnimal;
    [SerializeField] private SpriteRenderer rend;

    public override void OnDragEnd()
    {
        GameManager.Instance.Zoo.CatchAnimal(refAnimal);
        gameObject.SetActive(false);
    }

    public void CacheAnimal(ZooAnimal animal)
    {
        refAnimal = animal;
        rend.sprite = refAnimal.RSprite;
    }
}
