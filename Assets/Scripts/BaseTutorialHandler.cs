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
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject interactButton;
    [Header("Objects to use Crafting")]
    [SerializeField] TextMeshProUGUI craftingTextComponent;
    [SerializeField] Dialogue craftingDialogue;
    [SerializeField] GameObject craftingPanel;
    [SerializeField] Image Hand;
    [SerializeField] InventoryCraftingPanel inventoryCraftingPanel;
    [SerializeField] SelectedCraftingPanel selectedCraftingPanel;
    [SerializeField] Button CraftButton;
    [SerializeField] Button CloseButton;
    [SerializeField] Button WeaponScreenButton;

    [Header("Objects to use After Crafting")]
    [SerializeField] Dialogue AfterCraftingDialogue;
    [SerializeField] GameObject RelicBar;
    [SerializeField] GameObject CloneImage;
    [Header("Objects to turn off")]
    [SerializeField] CraftInteractable craftingMachine;
    [SerializeField] CloneInteractable cloneTree;
    [Header("Objects to use when entering the Zoo for the first time")]
    [SerializeField] ZooIneractable zooIneractable;
    [SerializeField] Dialogue zooDialogue;
    [SerializeField] Button feedButtonFirstPanel;
    [SerializeField] Button ExitButtonZoo;
    [SerializeField] Button TuffCoinButtonFirstPanel;
    [SerializeField] GameObject firstSlot;

    bool typing;
    bool didPlayerContinue;
    bool didPlayerGetCloseToCrafting;
    bool didPlayerGetCloseToClonetree;

    bool didPlayerUseCrafting;


    private void Start()
    {
        if (BetweenSceneInfo.didBaseTutorialHappen)
        {
            Cancel();
            return;
        }
        Arrow.transform.position = interactButton.transform.position + (Vector3.up * 160);
        didPlayerGetCloseToCrafting = false;
        craftingMachine.gameObject.SetActive(false);
        cloneTree.gameObject.SetActive(false);
        zooIneractable.gameObject.SetActive(false);
        m_interactable.gameObject.SetActive(true);
        BetweenSceneInfo.didBaseTutorialHappen = true;
        StartCoroutine(BaseTutorialRoutine());

    }

    private void Cancel()
    {
        LeanTween.cancelAll();
        StopAllCoroutines();
        m_interactable.gameObject.SetActive(false);
        zooIneractable.firstInteraction = true;
        zooIneractable.gameObject.SetActive(true);
      
        CloseButton.onClick.RemoveListener(Cancel);
        WeaponScreenButton.enabled = true;

        if (didPlayerUseCrafting)
        {
            TurnOnOffCraftingSelectedPanel(true);
            TurnOnOffCraftingResourceInventory(true);
        }
  

        this.gameObject.SetActive(false);

    }

    IEnumerator BaseTutorialRoutine()
    {
        m_interactable.onInteract.AddListener(() => didPlayerContinue = true);
        craftingMachine.m_detection.OnInProximity.AddListener(() => didPlayerGetCloseToCrafting = true);
        cloneTree.m_detection.OnInProximity.AddListener(() => didPlayerGetCloseToClonetree = true);

        yield return TypeStart(startingDialogue);
        yield return new WaitUntil(() => didPlayerUseCrafting);
        
        craftingPanel.SetActive(true);
        StartPanel.SetActive(false);
        yield return CraftingTutorial(craftingDialogue);
        yield return CloneTreeTutorial(AfterCraftingDialogue);

        cloneTree.gameObject.SetActive(true);
        craftingMachine.gameObject.SetActive(true);
        zooIneractable.gameObject.SetActive(true);
        cloneTree.m_detection.enabled = false;
        craftingMachine.enabled = false;
        m_interactable.onInteract.RemoveListener(() => didPlayerContinue = true);
        craftingMachine.m_detection.OnInProximity.RemoveListener(() => didPlayerGetCloseToCrafting = true);
        cloneTree.m_detection.OnInProximity.RemoveListener(() => didPlayerGetCloseToClonetree = true);
        m_interactable.gameObject.SetActive(false);
    }

    public void Print(Dialogue dialogue)
    {
        if (typing)
        {
            //Debug.Log("Tried to type new dialogue mid typing");
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
            yield return HandleLine(dialogue.lines[i], startingTextComponent);
            OnLineOverStart(i);
            yield return new WaitForSeconds(1.5f);

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
            Arrow.SetActive(true);
        }
        if (index == 2)
        {
            StartCoroutine(KeepArrowOnCraftingTable());
        }
    }
    IEnumerator CraftingTutorial(Dialogue dialogue)
    {
        typing = true;
        Arrow.SetActive(false);
        WeaponScreenButton.enabled = false;
        TurnOnOffCraftingSelectedPanel(false);
        TurnOnOffCraftingResourceInventory(false);

        CloseButton.onClick.AddListener(Cancel);
        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i], craftingTextComponent);



            yield return new WaitForSeconds(0.25f);
            if (i == 0)
            {
                Hand.gameObject.SetActive(true);
                LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[0].transform.position, 1);
                inventoryCraftingPanel.createdSlotsButtons[0].enabled = true;
                yield return new WaitUntil(() => selectedCraftingPanel.SelectedSlots[0].Occupied);
            }
            else if (i == 1)
            {

                LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[1].transform.position, 1);

                inventoryCraftingPanel.createdSlotsButtons[1].enabled = true;
                yield return new WaitUntil(() => selectedCraftingPanel.SelectedSlots[1].Occupied);
                inventoryCraftingPanel.createdSlotsButtons[1].enabled = false;

            }
            else if (i == 2)
            {
                if (selectedCraftingPanel.SelectedSlots[1].Occupied)
                {
                    selectedCraftingPanel.SelectedSlots[1].slot.m_button.enabled = true;
                    LeanTween.move(Hand.gameObject, selectedCraftingPanel.SelectedSlots[1].slot.transform.position, 1);
                    yield return new WaitUntil(() => !selectedCraftingPanel.SelectedSlots[1].Occupied);
                }

                LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[2].transform.position, 1);

            }
            else if (i == 3)
            {
                inventoryCraftingPanel.createdSlotsButtons[2].enabled = true;
                yield return new WaitUntil(() => (selectedCraftingPanel.SelectedSlots[1].Occupied && selectedCraftingPanel.SelectedSlots[1].slot.MyItemType == ItemType.Rune));
                //Hand.transform.position = CraftButton.transform.position;
                CraftButton.enabled = false;

            }
            else if (i == 4)
            {
                CraftButton.enabled = true;
                LeanTween.move(Hand.gameObject, CraftButton.transform.position, 1);
                bool didCraft = false;
                CraftButton.onClick.AddListener(() => didCraft = true);
                yield return new WaitUntil(() => didCraft);
                LeanTween.move(Hand.gameObject, CloseButton.transform.position, 1);


            }
            else if (i == 5)
            {
                CloseButton.onClick.RemoveListener(Cancel);
                CloseButton.enabled = true;
                bool didClose = false;
                CloseButton.onClick.AddListener(() => didClose = true);
                yield return new WaitUntil(() => didClose);
                Hand.gameObject.SetActive(false);


            }
        }
        TurnOnOffCraftingSelectedPanel(true);
        TurnOnOffCraftingResourceInventory(true);
        WeaponScreenButton.enabled = true;
        craftingPanel.SetActive(false);

        typing = false;
    }

    private void TurnOnOffCraftingSelectedPanel(bool _enabled)
    {
        
        for (int i = 0; i < selectedCraftingPanel.SelectedSlots.Count; i++)
        {
            selectedCraftingPanel.SelectedSlots[i].slot.m_button.enabled = _enabled;
        }
    }

    private void TurnOnOffCraftingResourceInventory(bool _enabled)
    {
        for (int i = 0; i < inventoryCraftingPanel.CreatedSlotsGetter.Count; i++)
        {
            inventoryCraftingPanel.createdSlotsButtons[i].enabled = _enabled;

        }
    }

    public void CallZooTutorial()
    {
        StartCoroutine(ZooTutorial(zooDialogue));
    }

    IEnumerator ZooTutorial(Dialogue dialogue)
    {
        typing = true;
        Arrow.SetActive(false);
        ExitButtonZoo.enabled = false;
        craftingPanel.SetActive(true);
        StartPanel.SetActive(false);
        didPlayerUseCrafting = false;
        TextMeshProUGUI textComponent = craftingTextComponent;
        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i], textComponent);



            yield return new WaitForSeconds(0.25f);
            if (i == 0)
            {
                bool wasClicked = false;
                void setWasClickedTrue() => wasClicked = true;
                Hand.gameObject.SetActive(true);
                LeanTween.move(Hand.gameObject, feedButtonFirstPanel.transform.position, 1);
                feedButtonFirstPanel.onClick.AddListener(setWasClickedTrue);
                yield return new WaitUntil(() => wasClicked == true);
                feedButtonFirstPanel.onClick.RemoveListener(setWasClickedTrue);
                if (TuffCoinButtonFirstPanel.gameObject.activeInHierarchy)
                {
                    TuffCoinButtonFirstPanel.enabled = false;
                }
                //LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[0].transform.position, 1);

            }
            else if (i == 1)
            {
                bool wasClicked = false;
                void setWasClickedTrue() => wasClicked = true;
                LeanTween.move(Hand.gameObject, ExitButtonZoo.transform.position, 1);
                ExitButtonZoo.enabled = true;

                ExitButtonZoo.onClick.AddListener(setWasClickedTrue);
                yield return new WaitUntil(() => wasClicked);
                ExitButtonZoo.onClick.RemoveListener(setWasClickedTrue);
                textComponent = startingTextComponent;
                craftingPanel.SetActive(false);
                StartPanel.SetActive(true);
                zooIneractable.gameObject.SetActive(false);
                craftingMachine.gameObject.SetActive(false);
                cloneTree.gameObject.SetActive(false);
                Hand.gameObject.SetActive(false);
                // LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[1].transform.position, 1);



            }
            else if (i == 2)
            {

                m_interactable.gameObject.SetActive(true);
                zooIneractable.gameObject.SetActive(false);
                Arrow.gameObject.SetActive(true);
                Arrow.transform.localScale = new Vector3(Arrow.transform.localScale.x, -Arrow.transform.localScale.y, Arrow.transform.localScale.z);

                while (!didPlayerContinue)
                {
                    Arrow.transform.position = Camera.main.WorldToScreenPoint(firstSlot.transform.position + (Vector3.up));
                    yield return new WaitForEndOfFrame();
                }
                // 
                didPlayerContinue = false;

            }
            else if (i == 3)
            {
                zooIneractable.gameObject.SetActive(true);
                m_interactable.gameObject.SetActive(false);
                bool interactedWithZoo = false;
                void SetInteractionTrue() => interactedWithZoo = true;
                zooIneractable.OnInteracted.AddListener(SetInteractionTrue);
                while (!interactedWithZoo)
                {
                    Arrow.transform.position = Camera.main.WorldToScreenPoint(firstSlot.transform.position + (Vector3.up));
                    yield return new WaitForEndOfFrame();
                }
                StartPanel.SetActive(false);
                craftingPanel.SetActive(true);
                textComponent = craftingTextComponent;
                zooIneractable.OnInteracted.RemoveListener(SetInteractionTrue);
                Arrow.transform.localScale = new Vector3(Arrow.transform.localScale.x, Arrow.transform.localScale.y, Arrow.transform.localScale.z);
                Arrow.gameObject.SetActive(false);
            }
            else if (i == 4)
            {
                Hand.gameObject.SetActive(true);
                TuffCoinButtonFirstPanel.enabled = true;
                LeanTween.move(Hand.gameObject, TuffCoinButtonFirstPanel.transform.position, 1);
                bool clickedTuffCoin = false;
                void SetInteractionTrue() => clickedTuffCoin = true;
                TuffCoinButtonFirstPanel.onClick.AddListener(SetInteractionTrue);
                yield return new WaitUntil(() => clickedTuffCoin);

            }
            else if (i == 5)
            {

                bool wasClicked = false;
                void setWasClickedTrue() => wasClicked = true;
                LeanTween.move(Hand.gameObject, ExitButtonZoo.transform.position, 1);
                ExitButtonZoo.onClick.AddListener(setWasClickedTrue);
                yield return new WaitUntil(() => wasClicked);
                ExitButtonZoo.onClick.RemoveListener(setWasClickedTrue);
                textComponent = startingTextComponent;
                textComponent.text = "Go and interact with the crafting station";
                craftingPanel.SetActive(false);
                StartPanel.SetActive(true);
                zooIneractable.gameObject.SetActive(false);
                craftingMachine.gameObject.SetActive(false);
                cloneTree.gameObject.SetActive(false);
                Hand.gameObject.SetActive(false);
                yield return KeepArrowOnCraftingTable();
                yield return new WaitUntil(() => didPlayerUseCrafting);
                didPlayerUseCrafting = false;

                Arrow.SetActive(false);
            }
            else if(i == 6)
            {
                Hand.gameObject.SetActive(true);
                LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[3].transform.position, 1);
                yield return new WaitUntil(() => selectedCraftingPanel.SelectedSlots[0].Occupied  && selectedCraftingPanel.SelectedSlots[0].slot.MyItemType == ItemType.TuffCoin );
            }
            else if (i == 7)
            {
                LeanTween.move(Hand.gameObject, CraftButton.transform.position, 1);
                bool wasCrafted = false;
                void SetCrafted() => wasCrafted = true;
                CraftButton.onClick.AddListener(SetCrafted);
                yield return new WaitUntil(() => wasCrafted);
                CraftButton.onClick.RemoveListener(SetCrafted);
                cloneTree.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                this.gameObject.SetActive(false);
            }
            //else if(i == 8)
            //{
               
            //}
        }

        craftingPanel.SetActive(false);


        typing = false;
    }
    IEnumerator CloneTreeTutorial(Dialogue dialogue)
    {
        craftingMachine.gameObject.SetActive(false);
        m_interactable.gameObject.SetActive(true);
        StartPanel.gameObject.SetActive(true);
        typing = true;

        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i], startingTextComponent);
            //Debug.Log(i);
            if (i == 0)
            {
                Arrow.gameObject.SetActive(true);
                Arrow.transform.position = interactButton.transform.position + (Vector3.up * 160);
            }
            if (i == 1)
            {
                Arrow.transform.position = RelicBar.transform.position + (Vector3.up * 100);
            }
            if (i == 2)
            {
                yield return KeepArrowOnCloneTreeTable();
            }
            if (i == 3)
            {
                Arrow.transform.position = CloneImage.transform.position + (Vector3.down * 60);
                Arrow.transform.localScale = new Vector3(1, -1, 1);

            }
            if (i == 4)
            {
                Arrow.gameObject.SetActive(false);
                yield return new WaitForSeconds(2);
                StartPanel.SetActive(false);
                cloneTree.Interact();
            }

            yield return new WaitForSeconds(1f);
            if (i != dialogue.lines.Length - 1)
            {
                yield return new WaitUntil(() => didPlayerContinue);
            }


            didPlayerContinue = false;

        }
        typing = false;

    }



    IEnumerator KeepArrowOnCraftingTable()
    {
        Arrow.gameObject.SetActive(true);
        craftingMachine.gameObject.SetActive(true);
        m_interactable.gameObject.SetActive(false);
        void SetPlayerUseCrafting() => didPlayerUseCrafting =true;
        craftingMachine.OnInteract.AddListener(() => SetPlayerUseCrafting());
        while (!didPlayerGetCloseToCrafting)
        {
            Arrow.transform.position = Camera.main.WorldToScreenPoint(craftingMachine.transform.position + (Vector3.up * 2));
            yield return new WaitForEndOfFrame();
        }
        

        Arrow.transform.position = interactButton.transform.position + (Vector3.up * 160);
    }
    IEnumerator KeepArrowOnCloneTreeTable()
    {

        while (!didPlayerContinue)
        {
            Arrow.transform.position = Camera.main.WorldToScreenPoint(cloneTree.transform.position + (Vector3.up * 2));
            yield return new WaitForEndOfFrame();
        }


    }
    IEnumerator HandleLine(string Line, TextMeshProUGUI textComponent)
    {
        yield return null;
        textComponent.text = string.Empty;
        textComponent.text = Line;
        //foreach (char ot in Line)
        //{
        //    textComponent.text += ot;
        //    yield return new WaitForSeconds(typeIntervals);
        //}
    }
    private void OnDisable()
    {
     
        Cancel();

    }
}
