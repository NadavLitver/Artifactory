using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    public float currentHP { get; private set; }
    [SerializeField, Range(0, 1337)] internal int maxHP;
    public UnityEvent onTakeDamage, onGetHealth, OnDeath;
    public void Awake()
    {
        currentHP = maxHP;
    }
    private void ClampHP() => currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log(gameObject.name + "Recieved Damage at this amount" + damage + "and now has " + currentHP + " Amount of HP");
        onTakeDamage?.Invoke();

        if (currentHP < 1)
        {
            onActorDeath();
        }
        ClampHP();

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

        ClampHP();


    }
}
