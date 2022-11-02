using UnityEngine;

public class AnimationEventFunctions : MonoBehaviour
{
    [SerializeField] Animator whooshAnimator;
  
    public void PlaySwordFirstAttackWhoosh()
    {
        PlayWhooshAnimation("Sword_Whoosh_FirstAttack");

    }
    public void PlaySwordSecondAttackWhoosh()
    {
        PlayWhooshAnimation("Sword_Whoosh_SecondAttack");

    }
    public void PlaySwordThirdAttackWhoosh()
    {
        PlayWhooshAnimation("Sword_Whoosh_ThirdAttack");

    }
    private void PlayWhooshAnimation(string name)
    {
        whooshAnimator.Play(name);

    }
}
