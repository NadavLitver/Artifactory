using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class RelicChoiceInteractable : Interactable
{
    public UnityEvent OnSelectedRelic;

    [SerializeField, ReadOnly] private Relic myRelic;
    [SerializeField] private SpriteRenderer relicSprite;

    private void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        myRelic = GameManager.Instance.RelicManager.GetFreeRelic();
        relicSprite.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(myRelic);
    }

    public override void Interact()
    {
        Instantiate(GameManager.Instance.assets.relicDropPrefab, transform.position, Quaternion.identity, transform);
        OnSelectedRelic?.Invoke();
    }

    public void HideRelicSprite()
    {
        relicSprite.enabled = false;
    }
}
