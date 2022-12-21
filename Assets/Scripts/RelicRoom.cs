using System.Collections.Generic;
using UnityEngine;

public class RelicRoom : Room
{

    [SerializeField] private List<RelicChoiceInteractable> relicChoices = new List<RelicChoiceInteractable>();
    
    private void Start()
    {
        foreach (var item in relicChoices)
        {
            item.OnSelectedRelic.AddListener(OnSelectedRelic);
        }   
    }

    private void OnSelectedRelic()
    {
        foreach (var item in relicChoices)
        {
            item.HideRelicSprite();
        }
    }    

}
