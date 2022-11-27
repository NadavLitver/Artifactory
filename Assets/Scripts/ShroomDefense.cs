using UnityEngine;

public class ShroomDefense : State
{
    [SerializeField] float defenseCoolDown;
    [SerializeField] float pushBackForce;
    StoneShroomStateHandler handler;
    float lastDefended;
    bool defending;
    bool entered;

    public override State RunCurrentState()
    {
        if (!entered)
        {
            entered = true;
            handler.Anim.SetTrigger(handler.Defendhash);
        }
        handler.RB.velocity = Vector2.zero;
        if (Time.time - lastDefended >= defenseCoolDown)
        {
            handler.ShroomActor.TakeDamageGFX.AddListener(PushBack);
            handler.ShroomActor.OnStatusEffectRemoved.AddListener(SetDefendingOff);
            handler.ShroomActor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
            lastDefended = Time.time;
            defending = true;
        }
        if (defending)
        {
            return this;
        }
        else if (handler.Enraged)
        {
            entered = false;

            return handler.ShroomThrow;
        }
        entered = false;

        return handler.ShroomNotice;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }


    public void PushBack()
    {
        Vector2 pushbackdir = (GameManager.Instance.assets.playerActor.transform.position - transform.position) * -1;
        handler.RB.AddForce(new Vector2(pushBackForce * pushbackdir.x, 0), ForceMode2D.Impulse);
        handler.ShroomActor.TakeDamageGFX.RemoveListener(PushBack);
    }

    public void SetDefendingOff(StatusEffect effect)
    {
        if (effect is Invulnerability)
        {
            defending = false;
        }
    }
}
