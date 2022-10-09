using UnityEngine.Events;

public interface IDamagable
{


    void GetHit(Ability m_ability);
    void GetHit(Ability m_ability, Actor host);

    void TakeDamage(DamageHandler dmgHandler);
    void TakeDamage(DamageHandler dmgHandler, Actor host);

    public UnityEvent<DamageHandler> onTakeDamage { get; set; }

}
