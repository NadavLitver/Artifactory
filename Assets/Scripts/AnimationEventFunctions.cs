using UnityEngine;

public class AnimationEventFunctions : MonoBehaviour
{
    [SerializeField] Animator whooshAnimator;
    private  int FirstAttackWhooshHash;
    private  int SecondAttackWhooshHash;
    private  int ThirdAttackWhooshHash;
    private  int DashWhooshHash;
    private  int FirstJumpAttackHash;
    private  int SecondJumpAttackHash;
    private  int ThirdJumpAttackHash;
    private  int FirstRunAttackHash;
    private  int SecondRunAttackHash;
    private int PickaxeAirAttackWhooshHash;
    private int PickaxeAirAttackLandWhooshHash;
    private int PickaxeFirstAttackWhooshHash;
    private int PickaxeSecondAttackWhooshHash;
    private int CannonShootWhoosh;
    private int CannonMuzzleFlashHash;



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
        PickaxeAirAttackWhooshHash = Animator.StringToHash("PickaxeAirAttackWhoosh");
        PickaxeFirstAttackWhooshHash = Animator.StringToHash("PickaxeFirstAttackWhoosh");
        PickaxeSecondAttackWhooshHash = Animator.StringToHash("PickaxeSecondAttackWhoosh");
        PickaxeAirAttackLandWhooshHash = Animator.StringToHash("PickaxeAirAttackLandWhoosh");
        CannonShootWhoosh = Animator.StringToHash("CannonShootWhoosh");
        CannonMuzzleFlashHash = Animator.StringToHash("MuzzleFlash");
    }

    public void PlayCannonMuzzleFlash()
    {
        PlayWhooshAnimation(CannonMuzzleFlashHash);

    }
    public void PlayPickaxeSecondAttackWhoosh()
    {
        PlayWhooshAnimation(PickaxeSecondAttackWhooshHash);

    }
    public void PlayPickaxeFirstAttackWhoosh()
    {
        PlayWhooshAnimation(PickaxeFirstAttackWhooshHash);

    }
    public void PlayPickaxeAirAttackWhoosh()
    {
        PlayWhooshAnimation(PickaxeAirAttackWhooshHash);

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
    public void PlayPickaxeAirAttackLandWhoosh()
    {
        PlayWhooshAnimation(PickaxeAirAttackLandWhooshHash);

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
