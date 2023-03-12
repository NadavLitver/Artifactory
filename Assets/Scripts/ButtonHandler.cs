using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler,IPointerClickHandler
{

    public TextMeshProUGUI m_selectedText;

    Color StartingColor;
    bool selected;
    private float startingSize;
    private float fontSizeGrowSpeed = 100;
    [SerializeField] GameObject ParticaleSystemOnClick;
    [SerializeField] Transform ParticaleWorldPosition;

    private void Start()
    {
        startingSize = m_selectedText.fontSize;
        StartingColor = m_selectedText.color;
        // Transperent = new Color(StartingColor.r, StartingColor.g, StartingColor.b, 0.2f);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_selectedText.enabled = true;
        selected = true;

    }
   

    public void OnPointerExit(PointerEventData eventData)
    {
        
        selected = false;

        m_selectedText.color = StartingColor;

        eventData.selectedObject = null;




    }

    public void OnSelect(BaseEventData eventData)
    {
        selected = true;
        m_selectedText.color = Color.gray;
       // LeanTween.delayedCall(1f, () => m_selectedText.color = Color.white);
     
      

        


    }
    public void OnDeselect(BaseEventData eventData)
    {
        selected = false;
        m_selectedText.color = StartingColor;

    }
    private void LateUpdate()
    {
        if (selected)
        {
            m_selectedText.fontSize = Mathf.MoveTowards(m_selectedText.fontSize, startingSize * 1.2f, Time.deltaTime * fontSizeGrowSpeed);
           
        }
        else
        {
            m_selectedText.fontSize = Mathf.MoveTowards(m_selectedText.fontSize, startingSize, Time.deltaTime * fontSizeGrowSpeed);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        Instantiate(ParticaleSystemOnClick, eventData.pointerCurrentRaycast.worldPosition, Quaternion.identity, null);
    }
    private void OnDisable()
    {
        selected = false;
        m_selectedText.color = StartingColor;
        
    }
   
}
