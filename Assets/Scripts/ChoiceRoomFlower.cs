using UnityEngine;

public class ChoiceRoomFlower : Interactable
{
    private RelicChoiceInteractable left;
    private RelicChoiceInteractable right;
    [SerializeField] private Animator anim;
    bool choseRelic;
    public Animator Anim { get => anim; }
    public bool ChoseRelic { get => choseRelic; set => choseRelic = value; }

    public void CacheLeftRelic(RelicChoiceInteractable left)
    {
        this.left = left;
    }

    public void CacheRightRelic(RelicChoiceInteractable right)
    {
        this.right = right;
    }

    public override void Interact()
    {
        if (choseRelic)
        {
            return;
        }
        //open ui
    }
}
