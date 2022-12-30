using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileControlsHandler : MonoBehaviour
{
    [SerializeField] private RectTransform[] weaponButtons;
    [SerializeField] private RectTransform mobilityButton;
    // [SerializeField] private MobilityButton mobilityButtonRef;
    //[SerializeField] private RectTransform mobilityButtonRef;
    [SerializeField] private RectTransform AttackButton;
    [SerializeField] private RectTransform InteractButton;
    [SerializeField] private Sprite[] MobilitySprites;
    [SerializeField] private Image MobilityImageRef;
    bool isAttackOn = true;

    public void SetMobilityOnButton(int index)
    {

        mobilityButton.position = weaponButtons[index].position;
        // mobilityButtonRef.ChangeSpriteByIndex(index);
        MobilityImageRef.sprite = MobilitySprites[index];
        for (int i = 0; i < weaponButtons.Length; i++)
            weaponButtons[i].gameObject.SetActive(index != i);



    }
    public void SetInteractable()
    {
        isAttackOn = false;
        AttackButton.gameObject.SetActive(isAttackOn);
        InteractButton.gameObject.SetActive(!isAttackOn);


    }
    public void SetAttack()
    {
        isAttackOn = true;
        AttackButton.gameObject.SetActive(isAttackOn);
        InteractButton.gameObject.SetActive(!isAttackOn);

    }

}
