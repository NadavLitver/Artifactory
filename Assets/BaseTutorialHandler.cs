using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseTutorialHandler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float timeBetweenLines;
    [SerializeField] float typeIntervals;

    [Header("Objects to use")]
    [SerializeField] BaseTutorialInteractable m_interactable;
    [SerializeField] TextMeshProUGUI m_text;
    [SerializeField] Dialogue m_dialogue;

    [Header("Objects to turn off")]
    [SerializeField] CraftInteractable craftingMachine;
    [SerializeField] CloneInteractable cloneTree;
    bool startedBefore;
    bool typing;
    bool isPlayerContinue;
   
    private void OnEnable()
    {
        if (startedBefore)
        {
            this.gameObject.SetActive(false);
            return;
        }
        craftingMachine.gameObject.SetActive(false);
        cloneTree.gameObject.SetActive(false);
        m_interactable.gameObject.SetActive(true);
        startedBefore = true;
        StartCoroutine(BaseTutorialRoutine());
    }



    IEnumerator BaseTutorialRoutine()
    {
        m_interactable.onInteract.AddListener(() => isPlayerContinue = true);
        yield return Type(m_dialogue);

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
            StartCoroutine(Type(dialogue));
        }
    }
    internal IEnumerator Type(Dialogue dialogue)
    {
        typing = true;
      
        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i]);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(()=> isPlayerContinue);
            isPlayerContinue = false;
        }
        typing = false;
        

    }
    IEnumerator HandleLine(string Line)
    {
        m_text.text = string.Empty;
        foreach (char ot in Line)
        {
            m_text.text += ot;
            yield return new WaitForSeconds(typeIntervals);
        }
    }
}
