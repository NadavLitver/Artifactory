using UnityEngine;

public class ShroomDefense : State
{
    [SerializeField] float defenseCoolDown;
    [SerializeField] float pushBackForce;
    StoneShroomStateHandler handler;
    float lastDefended;
    bool defending;

    public override State RunCurrentState()
    {
       
        if (Time.time - lastDefended >= defenseCoolDown)
        {
           // handler.ShroomActor.TakeDamageGFX.AddListener(PushBack);
            handler.RB.velocity = Vector2.zero;
            handler.ShroomActor.OnStatusEffectRemoved.AddListener(SetDefendingOff);
            handler.ShroomActor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
            lastDefended = Time.time;
            defending = true;
            Vector2 playerDir = handler.GetPlayerDirection();
            if (playerDir.x < 0 && handler.Flipper.IsLookingRight || playerDir.x > 0 && !handler.Flipper.IsLookingRight)
            {
                handler.Flipper.Flip();
            }
            handler.Anim.SetTrigger("Defend");

        }
        if (defending)
        {
            return this;
        }
        else if (handler.Enraged)
        {

            return handler.ShroomThrow;
        }

        return handler.ShroomNotice;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
        lastDefended = defenseCoolDown;
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
