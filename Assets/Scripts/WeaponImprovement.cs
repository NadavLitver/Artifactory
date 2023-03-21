using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeaponEnum
{
    Sword,
    Axe,
    Gun
}

[CreateAssetMenu(fileName = "New Crafted Weapon Upgrade", menuName = "Crafted Weapon Upgrade")]

public class WeaponImprovement : CraftedItem
{
    [SerializeField] private float damageMod;
    [SerializeField] private WeaponEnum weaponType;
    public override void Obtain()
    {
        GameManager.Instance.assets.playerActor.WeapoonManager.CurrentWeapon.OnActorHit.AddListener(AddWeaponDamage);
    }

    public override void SetUp()
    {
       //throw new System.NotImplementedException();
    }
    private void AddWeaponDamage(Actor target, Ability ability)
    {
        switch (weaponType)
        {
            case WeaponEnum.Sword:
                if (GameManager.Instance.assets.playerActor.WeapoonManager.CurrentWeapon is BasicSword)
                {
                    ability.DamageHandler.AddModifier(damageMod);
                }
                break;
            case WeaponEnum.Axe:
                if (GameManager.Instance.assets.playerActor.WeapoonManager.CurrentWeapon is BasicPickaxe)
                {
                    ability.DamageHandler.AddModifier(damageMod);
                }
                break;
            case WeaponEnum.Gun:
                if (GameManager.Instance.assets.playerActor.WeapoonManager.CurrentWeapon is Cannon)
                {
                    ability.DamageHandler.AddModifier(damageMod);
                }
                break;
            default:
                break;
        }

        
    }

}
