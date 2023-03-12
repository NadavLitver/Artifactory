using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public enum ChestSide
{
    Right,
    Left
}
public class RelicChoiceInteractable : MonoBehaviour
{
    public UnityEvent OnSelectedRelic;

    [SerializeField, ReadOnly] private Relic myRelic;
    [SerializeField] private ChestSide side;
    [SerializeField] private Animator anim;
    [SerializeField] private ChoiceRoomFlower refFlower;

    public Relic MyRelic { get => myRelic; }

    //[SerializeField] private SpriteRenderer relicSprite;

    private void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        myRelic = GameManager.Instance.RelicManager.GetFreeRelic();
        if (side == ChestSide.Left)
        {
            refFlower.CacheLeftRelic(this);
        }
        else
        {
            refFlower.CacheRightRelic(this);
        }
        //relicSprite.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(myRelic);
    }

  /*  public override void Interact()
    {
        RelicDrop drop = Instantiate(GameManager.Instance.assets.relicDropPrefab, transform.position, Quaternion.identity, transform);
        drop.CacheRelic(myRelic);
        OnSelectedRelic?.Invoke();
        switch (side)
        {
            case ChestSide.Right:
                anim.Play("PlayRight");
                break;
            case ChestSide.Left:
                anim.Play("PlayLeft");
                break;
            default:
                break;
        }
    }*/

    public void HideRelicSprite()
    {
        //relicSprite.enabled = false;
    }
}
