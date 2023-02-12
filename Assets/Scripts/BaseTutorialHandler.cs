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
    [Header("Objects to use After Crafting")]
    [SerializeField] Dialogue AfterCraftingDialogue;
    [SerializeField] GameObject RelicBar;
    [SerializeField] GameObject CloneImage;
    [Header("Objects to turn off")]
    [SerializeField] CraftInteractable craftingMachine;
    [SerializeField] CloneInteractable cloneTree;
    bool typing;
    bool didPlayerContinue;
    bool didPlayerGetCloseToCrafting;
    bool didPlayerGetCloseToClonetree;

    bool didPlayerUseCrafting;


    private void OnEnable()
    {
        if (BetweenSceneInfo.didBaseTutorialHappen)
        {
            this.gameObject.SetActive(false);
            return;
        }
        Arrow.transform.position = interactButton.transform.position + (Vector3.up * 160);
        didPlayerGetCloseToCrafting = false;
        craftingMachine.gameObject.SetActive(false);
        cloneTree.gameObject.SetActive(false);
        m_interactable.gameObject.SetActive(true);
        BetweenSceneInfo.didBaseTutorialHappen = true;
        StartCoroutine(BaseTutorialRoutine());

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
        yield return AfterCrafting(AfterCraftingDialogue);

        cloneTree.gameObject.SetActive(true);
        craftingMachine.gameObject.SetActive(true);
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
        for (int i = 0; i < inventoryCraftingPanel.CreatedSlotsGetter.Count; i++)
        {
            inventoryCraftingPanel.createdSlotsButtons[i].enabled = false;

        }


        CloseButton.enabled = false;
        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i], craftingTextComponent);



            yield return new WaitForSeconds(0.5f);
            if (i == 0)
            {
                Hand.gameObject.SetActive(true);
                // Hand.transform.position = inventoryCraftingPanel.CreatedSlotsGetter[0].transform.position;
                LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[0].transform.position, 1);
                inventoryCraftingPanel.createdSlotsButtons[0].enabled = true;
                yield return new WaitUntil(() => selectedCraftingPanel.SelectedSlots[0].Occupied);
            }
            else if (i == 1)
            {
                //   Hand.transform.position = inventoryCraftingPanel.CreatedSlotsGetter[1].transform.position;
                LeanTween.move(Hand.gameObject, inventoryCraftingPanel.CreatedSlotsGetter[1].transform.position, 1);

                inventoryCraftingPanel.createdSlotsButtons[1].enabled = true;
                yield return new WaitUntil(() => selectedCraftingPanel.SelectedSlots[1].Occupied);
                inventoryCraftingPanel.createdSlotsButtons[1].enabled = false;

            }
            else if (i == 2)//use selected panel to check for cancelling the branch choice
            {
                if (selectedCraftingPanel.SelectedSlots[1].Occupied)
                {
                    LeanTween.move(Hand.gameObject, selectedCraftingPanel.SelectedSlots[1].slot.transform.position, 1);
                    yield return new WaitUntil(() => !selectedCraftingPanel.SelectedSlots[1].Occupied);
                }
                //Hand.transform.position =  inventoryCraftingPanel.CreatedSlotsGetter[2].transform.position;

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
                CloseButton.enabled = true;
                bool didClose = false;
                CloseButton.onClick.AddListener(() => didClose = true);
                yield return new WaitUntil(() => didClose);
                Hand.gameObject.SetActive(false);


            }
        }
        for (int i = 0; i < inventoryCraftingPanel.CreatedSlotsGetter.Count; i++)
        {
            inventoryCraftingPanel.createdSlotsButtons[i].enabled = true;

        }
        craftingPanel.SetActive(false);

        typing = false;
    }

    IEnumerator AfterCrafting(Dialogue dialogue)
    {
        craftingMachine.gameObject.SetActive(false);
        m_interactable.gameObject.SetActive(true);
        StartPanel.gameObject.SetActive(true);
        typing = true;

        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i], startingTextComponent);
            Debug.Log(i);
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
                Arrow.transform.position = CloneImage.transform.position + (Vector3.down *60);
                Arrow.transform.localScale = new Vector3(1, -1, 1);
                // yield return KeepArrowOnCloneTreeTable();

                //Hand.transform.position = interactButton.transform.position + (Vector3.up * 160); 
            }
            if (i == 4)
            {
                Arrow.gameObject.SetActive(false);
                yield return new WaitForSeconds(2);
                StartPanel.SetActive(false);
                cloneTree.Interact();
            }
            if (i == 5)
            {
                // cloneTree.gameObject.SetActive(true);

            }
            if (i == 6)
            {

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
        craftingMachine.gameObject.SetActive(true);
        m_interactable.gameObject.SetActive(false);
        craftingMachine.OnInteract.AddListener(() => didPlayerUseCrafting = true);
        while (!didPlayerGetCloseToCrafting)
        {
            Arrow.transform.position = Camera.main.WorldToScreenPoint(craftingMachine.transform.position + (Vector3.up * 2));
            yield return new WaitForEndOfFrame();
        }
        Arrow.transform.position = interactButton.transform.position + (Vector3.up * 160);
    }
    IEnumerator KeepArrowOnCloneTreeTable()
    {
        // cloneTree.gameObject.SetActive(true);

        while (!didPlayerContinue)
        {
            Arrow.transform.position = Camera.main.WorldToScreenPoint(cloneTree.transform.position + (Vector3.up * 2));
            yield return new WaitForEndOfFrame();
        }
        //Arrow.transform.position = interactButton.transform.position + (Vector3.up * 160);
        // cloneTree.gameObject.SetActive(false);

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
        LeanTween.cancelAll();
    }
}
