using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyHealthBar : MonoBehaviour
{
    //precentege = (value/total value)×100%.
    public float healthBarUpdateSpeed;
    private SpriteRenderer m_sr;
    private Actor m_actor;
    float currentPercent;
    float maxHpBarSize;
    private void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        m_actor = GetComponentInParent<Actor>();
       // m_actor.onTakeDamage.AddListener(UpdateHealthBar);
        maxHpBarSize = m_sr.size.x;
        currentPercent = (m_actor.currentHP / m_actor.maxHP);
        m_actor.TakeDamageEvent.AddListener(UpdateHealthBar);
    }
    public void UpdateHealthBar()
    {
        currentPercent = (m_actor.currentHP / m_actor.maxHP);//discared  * 100 to get it between 0 to 1
    }
    private void LateUpdate()
    {
        if (m_sr.size.x != currentPercent)
        {
            m_sr.size = Vector2.MoveTowards(m_sr.size, new Vector2((currentPercent * maxHpBarSize), m_sr.size.y), Time.deltaTime * healthBarUpdateSpeed);
            m_sr.color = Color.Lerp(Color.white, Color.red, 1 - currentPercent);
           
        }
    }
}
