using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueExecuter : MonoBehaviour
{
    [SerializeField] GameObject m_panel;
    [SerializeField] TextMeshProUGUI m_text;
    [SerializeField] Image speakerImage;

    [SerializeField] float typeIntervals;
    [SerializeField] float timeBetweenLines;
    bool typing;


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
    IEnumerator Type(Dialogue dialogue)
    {
        typing = true;
        m_panel.SetActive(true);
        if (ReferenceEquals(dialogue.m_sprite.sprite, null))
        {
            speakerImage.gameObject.SetActive(false);
        }
        else
        {
            speakerImage.gameObject.SetActive(true);
            speakerImage.sprite = dialogue.m_sprite.sprite;

        }
        for (int i = 0; i < dialogue.lines.Length; i++)
        {
            yield return HandleLine(dialogue.lines[i]);
            yield return new WaitForSeconds(timeBetweenLines);
        }
        typing = false;
        m_panel.SetActive(false);

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
