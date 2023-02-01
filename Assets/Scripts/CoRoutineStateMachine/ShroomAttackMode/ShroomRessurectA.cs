using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomRessurectA : ShroomBaseStateA
{
    public override IEnumerator StateRoutine()
    {
        handler.Actor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
        handler.Anim.Play("Ressurect");
        handler.Rb.velocity = Vector2.zero;
        handler.Actor.Heal(new DamageHandler() { amount = handler.Actor.maxHP });
        transform.parent.transform.position = handler.CurrentCap.transform.position;
        SoundManager.Play(SoundManager.Sound.MushroomEnemyRevive, handler.m_audioSource);
        yield return new WaitUntil(() => handler.DoneRessurecting);
        handler.Actor.RemoveStatusEffect(new Invulnerability());
        handler.DoneRessurecting = false;
        handler.CurrentCap.gameObject.SetActive(false);
        handler.SwitchToOtherMode();
    }

    internal override bool myCondition()
    {
        return true;
    }
}
