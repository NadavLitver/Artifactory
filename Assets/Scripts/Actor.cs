using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public float currentHP { get; private set; }
    [SerializeField, Range(0, 1337)] protected int maxHP;
    public UnityEvent onTakeDamage, onGetHealth, OnDeath;
    public void Awake()
    {
        currentHP = maxHP;
    }
    private void clampHP() => currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    public virtual void TakeDamage(float damage)
    {
        onTakeDamage?.Invoke();
        currentHP -= damage;
        Debug.Log(gameObject.name + "Recieved Damage at this amount" + damage + "and now has " + currentHP + " Amount of HP");

        if (currentHP < 0)
        {
            onActorDeath();
        }
        clampHP();

    }
    public virtual void onActorDeath()
    {
        Debug.Log(gameObject.name + "hasDead");
        OnDeath?.Invoke();
    }
    public virtual void GetHealth(float health)
    {
        onGetHealth?.Invoke();
        currentHP += health;
        Debug.Log(gameObject.name + "Recieved health at this amount" + health + "and now has " + currentHP + " Amount of HP");

        clampHP();


    }
}
