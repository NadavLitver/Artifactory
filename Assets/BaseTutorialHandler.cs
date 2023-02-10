using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseTutorialHandler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float timeBetweenLines;
    [SerializeField] float typeIntervals;

    [Header("Objects to use Starting")]
    [SerializeField] BaseTutorialInteractable m_interactable;
    [SerializeField] TextMeshProUGUI startingTextComponent;
    [SerializeField] Dialogue startingDialogue;
    [SerializeField] GameObject StartPanel;
    [SerializeField] Image Arrow;
    [Header("Objects to use Crafting")]
    [SerializeField] TextMeshProUGUI craftingTextComponent;
    [SerializeField] Dialogue craftingDialogue;
    [SerializeField] GameObject craftingPanel;
    [SerializeField] Image Hand;
    [SerializeField] InventoryCraftingPanel inventoryCraftingPanel;
    [SerializeField] SelectedCraftingPanel selectedCraftingPanel;
    [Header("Objects to turn off")]
    [SerializeField] CraftInteractable craftingMachine;
    [SerializeField] CloneInteractable cloneTree;
    private ItemType currentItemType;
    private Vector3 arrowStartingPos;
    bool startedBefore;
    bool typing;
    bool didPlayerContinue;
    bool didPlayerGetCloseToCrafting;
    bool didPlayerUseCrafting;


    private void OnEnable()
    {
        if (startedBefore)
        {
            this.gameObject.SetActive(false);
            return;
        }
        arrowStartingPos = Arrow.transform.position;
        didPlayerGetCloseToCrafting = false;
        craftingMachine.gameObject.SetActive(false);
        cloneTree.gameObject.SetActive(false);
        m_interactable.gameObject.SetActive(true);
        startedBefore = true;
        StartCoroutine(BaseTutorialRoutine());
        
    }



    IEnumerator BaseTutorialRoutine()
    {
        m_interactable.onInteract.AddListener(() => didPlayerContinue = true);
        craftingMachine.m_detection.OnInProximity.AddListener(() => didPlayerGetCloseToCrafting = true);
        yield return TypeStart(startingDialogue);
        yield return new WaitUntil(() => didPlayerUseCrafting);
        craftingPanel.SetActive(true);
        StartPanel.SetActive(false);
        yield return CraftingTutorial(craftingDialogue);



    }

    public void Print(Dialogue dialogue)
    {
        if (typing)
        {
            Debug.Log("Tried to type new dialogue mid typing");
            return;
        }
        else
        {
            StartCoroutine(TypeStart(dialogue));
        }
    }
    internal IEnumerator TypeStart(Dialogue dialogue)
    {
        typing = true;

        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i],startingTextComponent);
            OnLineOverStart(i);
            yield return new WaitForSeconds(1);

            if (i != dialogue.lines.Length - 1)
            {
                yield return new WaitUntil(() => didPlayerContinue);
            }
            didPlayerContinue = false;
        }
        typing = false;


    }
    void OnLineOverStart(int index)
    {
        if (index == 0)
        {
            Arrow.gameObject.SetActive(true);
        }
        if (index == 2)
        {
            StartCoroutine(KeepArrowOnCraftingTable());
        }
    }
    IEnumerator CraftingTutorial(Dialogue dialogue)
    {
        typing = true;
        Arrow.gameObject.SetActive(false);
        for (int i = 0; i < inventoryCraftingPanel.CreatedSlotsGetter.Count; i++)
        {
            inventoryCraftingPanel.createdSlotsButtons[i].enabled = false;
            inventoryCraftingPanel.CreatedSlotsGetter[i].OnSelected.AddListener(OnResourceSelected);
        }
       
        currentItemType = ItemType.Null;

        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i],craftingTextComponent);

            

            yield return new WaitForSeconds(0.5f);
            if(i == 0)
            {
                Hand.gameObject.SetActive(true);
                Hand.transform.position = inventoryCraftingPanel.CreatedSlotsGetter[0].transform.position;
                inventoryCraftingPanel.createdSlotsButtons[0].enabled = true;
                yield return new WaitUntil(() => currentItemType == ItemType.Glimmering);
            }
            else if(i == 1)
            {
                Hand.transform.position = inventoryCraftingPanel.CreatedSlotsGetter[i].transform.position;
                inventoryCraftingPanel.createdSlotsButtons[1].enabled = true;
                yield return new WaitUntil(() => currentItemType == ItemType.Branch);
            }
            else if(i == 2)//use selected panel to check for cancelling the branch choice
            {

            }else if(i == 3)
            {
                
            }else if(i == 4)
            {

            }else if(i == 5)
            {

            }
        }
        for (int i = 0; i < inventoryCraftingPanel.CreatedSlotsGetter.Count; i++)
        {
            inventoryCraftingPanel.createdSlotsButtons[i].enabled = true;
            inventoryCraftingPanel.CreatedSlotsGetter[i].OnSelected.RemoveListener(OnResourceSelected);
        }
        typing = false;
    }

    private void OnResourceSelected(ItemUiSlot item)
    {
        switch (item.MyItemType)
        {
            case ItemType.Glimmering:
                currentItemType = ItemType.Glimmering;
                break;
            case ItemType.Branch:
                currentItemType = ItemType.Branch;
                break;
            case ItemType.Rune:
                currentItemType = ItemType.Rune;
                break;
            case ItemType.TuffCoin:
                currentItemType = ItemType.TuffCoin;
                break;
            case ItemType.Null:
                currentItemType = ItemType.Null;
                break;
            default:
                break;  
        }
    }
   
   
    
    IEnumerator KeepArrowOnCraftingTable()
    {
        craftingMachine.gameObject.SetActive(true);
        m_interactable.gameObject.SetActive(false);
        craftingMachine.OnInteract.AddListener(() => didPlayerUseCrafting = true);
        while (!didPlayerGetCloseToCrafting)
        {
            Arrow.gameObject.transform.position = Camera.main.WorldToScreenPoint(craftingMachine.transform.position + Vector3.up);
            yield return new WaitForEndOfFrame();
        }
        Arrow.gameObject.transform.position = arrowStartingPos;
    }
    IEnumerator HandleLine(string Line,TextMeshProUGUI textComponent)
    {
        textComponent.text = string.Empty;
        foreach (char ot in Line)
        {
            textComponent.text += ot;
            yield return new WaitForSeconds(typeIntervals);
        }
    }
}
