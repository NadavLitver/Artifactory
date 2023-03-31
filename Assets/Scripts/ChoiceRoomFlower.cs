using UnityEngine;

public class ChoiceRoomFlower : Interactable
{
    private RelicChoiceInteractable left;
    private RelicChoiceInteractable right;
    [SerializeField] private Animator anim;
    bool choseRelic;
    public AudioSource m_audioSource;
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
        GameManager.Instance.assets.RelicChoicePanel.CacheLeftRelic(left.MyRelic);
        GameManager.Instance.assets.RelicChoicePanel.CacheRightRelic(right.MyRelic);
        GameManager.Instance.assets.RelicChoicePanel.CacheFlower(this);
        GameManager.Instance.assets.RelicChoicePanel.gameObject.SetActive(true);
    }
}
