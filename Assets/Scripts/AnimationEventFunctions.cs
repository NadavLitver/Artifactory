using UnityEngine;

public class AnimationEventFunctions : MonoBehaviour
{
    [SerializeField] Animator whooshAnimator;
    private static int FirstAttackWhooshHash;
    private static int SecondAttackWhooshHash;
    private static int ThirdAttackWhooshHash;
    private static int DashWhooshHash;
    private static int FirstJumpAttackHash;
    private static int SecondJumpAttackHash;
    private static int ThirdJumpAttackHash;
    private static int FirstRunAttackHash;
    private static int SecondRunAttackHash;




    public void Start()
    {
        FirstAttackWhooshHash = Animator.StringToHash("Sword_Whoosh_FirstAttack");
        SecondAttackWhooshHash = Animator.StringToHash("Sword_Whoosh_SecondAttack");
        ThirdAttackWhooshHash = Animator.StringToHash("Sword_Whoosh_ThirdAttack");
        DashWhooshHash = Animator.StringToHash("DashWhooshAnimation");
        FirstJumpAttackHash = Animator.StringToHash("JumpFirstAttackWhoosh");
        SecondJumpAttackHash = Animator.StringToHash("JumpSecondAttackWhoosh");
        ThirdJumpAttackHash = Animator.StringToHash("JumpThirdAttackWhoosh");
        FirstRunAttackHash = Animator.StringToHash("RunFirstAttackWhoosh");
        SecondRunAttackHash = Animator.StringToHash("RunSecondAttackWhoosh");



    }
    public void PlaySwordFirstAttackWhoosh()
    {
        PlayWhooshAnimation(FirstAttackWhooshHash);

    }
    public void PlaySwordSecondAttackWhoosh()
    {
        PlayWhooshAnimation(SecondAttackWhooshHash);

    }
    public void PlaySwordThirdAttackWhoosh()
    {
        PlayWhooshAnimation(ThirdAttackWhooshHash);

    }
    public void PlayDashWhoosh()
    {
        PlayWhooshAnimation(DashWhooshHash);
    }
    public void PlayJumpAttackWhoosh()
    {
        PlayWhooshAnimation(FirstJumpAttackHash);

    }
    public void PlaySecondJumpAttackWhoosh()
    {
        PlayWhooshAnimation(SecondJumpAttackHash);

    }
    public void PlayThirdJumpAttackWhoosh()
    {
        PlayWhooshAnimation(ThirdJumpAttackHash);

    }
    public void PlayFirstRunAttackWhoosh()
    {
        PlayWhooshAnimation(FirstRunAttackHash);

    }
    public void PlaySecondRunAttackWhoosh()
    {
        PlayWhooshAnimation(SecondRunAttackHash);

    }
  
    private void PlayWhooshAnimation(string name)
    {
        whooshAnimator.Play(name);

    }
    private void PlayWhooshAnimation(int hash)
    {
        whooshAnimator.Play(hash);

    }
}
