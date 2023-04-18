using System.Collections.Generic;
using UnityEngine;

public class RelicBarDesc : MonoBehaviour
{
    [SerializeField] private RelicBarDescImage imagePrefab;
    private List<RelicBarDescImage> createdImages = new List<RelicBarDescImage>();

    public void AddRelic(Relic givenRelic)
    {
        RelicBarDescImage image = Instantiate(imagePrefab, transform);
        image.SetUp(givenRelic);
        createdImages.Add(image);
        
    }

    public void RemoveRelic(Relic givenRelic)
    {
        foreach (var item in createdImages)
        {
            if (ReferenceEquals(item.RefRelic, givenRelic))
            {
                createdImages.Remove(item);
                Destroy(item.gameObject);
                return;
            }
        }
    }

    public void SwapRight()
    {
        if (createdImages.Count < 4)
        {
            return;
        }
        List<Relic> newOrderedList = new List<Relic>();
        newOrderedList.Add(createdImages[createdImages.Count - 1].RefRelic);
        
        for (int i = 0; i < createdImages.Count-1; i++)
        {
            newOrderedList.Add(createdImages[i].RefRelic);
        }
        for (int i = 0; i < createdImages.Count; i++)
        {
            createdImages[i].SetUp(newOrderedList[i]);
            Debug.Log(createdImages[i].RefRelic);

        }
    }
    public void SwapLeft()
    {
        if (createdImages.Count < 4)
        {
            return;
        }
        List<Relic> newOrderedList = new List<Relic>();
        for (int i = 1; i < createdImages.Count; i++)
        {
            newOrderedList.Add(createdImages[i].RefRelic);
        }
        newOrderedList.Add(createdImages[0].RefRelic);
        for (int i = 0; i < createdImages.Count; i++)
        {
            createdImages[i].SetUp(newOrderedList[i]);
            Debug.Log(createdImages[i].RefRelic);
        }
    }

}
