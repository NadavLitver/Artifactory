using UnityEngine;

public enum WeaponEnum
{
    Sword,
    Axe,
    Gun
}

[CreateAssetMenu(fileName = "New Crafted Weapon Upgrade", menuName = "Crafted Weapon Upgrade")]

public class WeaponImprovement : CraftedItem
{
    [SerializeField] private float damageMod;
    [SerializeField] private WeaponEnum m_weaponType;

    public WeaponEnum WeaponType { get => m_weaponType; }

    public override void Obtain()
    {
        GameManager.Instance.assets.playerActor.OnDealDamage.AddListener(AddWeaponDamage);
        GameManager.Instance.assets.prizePanel.CallShowPrizeFromWeaponImprovments(this);
    }

    public override void SetUp()
    {
        //throw new System.NotImplementedException();
    }
    private void AddWeaponDamage(DamageHandler damage, Actor target)
    {
        switch (m_weaponType)
        {
            case WeaponEnum.Sword:
                if (GameManager.Instance.assets.playerActor.WeaponManager.CurrentWeapon is BasicSword)
                {
                    Debug.Log("sword damage increased");
                    damage.AddModifier(damageMod);
                }
                break;
            case WeaponEnum.Axe:
                if (GameManager.Instance.assets.playerActor.WeaponManager.CurrentWeapon is BasicPickaxe)
                {
                    damage.AddModifier(damageMod);
                }
                break;
            case WeaponEnum.Gun:
                if (GameManager.Instance.assets.playerActor.WeaponManager.CurrentWeapon is Cannon)
                {
                    damage.AddModifier(damageMod);
                }
                break;
            default:
                break;
        }


    }

}
