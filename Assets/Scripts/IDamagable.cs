using UnityEngine.Events;

public interface IDamagable
{
    void GetHit(Ability m_ability);

    void TakeDamage(DamageHandler dmgHandler);

    public UnityEvent<DamageHandler> onTakeDamage { get; set; }

}
