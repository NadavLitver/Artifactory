using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> playerWeapons = new List<Weapon>();
    [SerializeField] Weapon currentWeapon;
    Actor m_actor;
    public List<Weapon> PlayerWeapons { get => playerWeapons; set => playerWeapons = value; }
    public Weapon CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }

    public UnityEvent OnSwitchWeapon;

    private void Start()
    {
        EquipWeapon(playerWeapons[0]);
        m_actor = GetComponentInParent<Actor>();
        if (ReferenceEquals(m_actor, null))
        {
            Debug.LogError("Ability Combo on" + gameObject.name + " is Null");
        }
        OnSwitchWeapon.AddListener(m_actor.DisableOnInAnim);
    }


    public void SwitchCurrentWeapon(bool previous)
    {
        if (previous)
        {
            EquipWeapon(GetPreviousWeapon());
        }
        else
        {
            EquipWeapon(GetNextWeapon());
        }
    }
    public void ToggleIsInAttackAnim()
    {
        m_actor.IsInAttackAnim = !m_actor.IsInAttackAnim;
    }
    private Weapon GetNextWeapon()
    {
        if (currentWeapon = playerWeapons[playerWeapons.Count - 1])
        {
            return playerWeapons[0];
        }

        for (int i = 0; i < playerWeapons.Count; i++)
        {
            if (playerWeapons[i] == currentWeapon)
            {
                return playerWeapons[i + 1];
            }
        }

        return null;


    }

    private Weapon GetPreviousWeapon()
    {
        if (currentWeapon == playerWeapons[0])
        {
            return playerWeapons[playerWeapons.Count - 1];
        }

        for (int i = 0; i < playerWeapons.Count; i++)
        {
            if (playerWeapons[i] == currentWeapon)
            {
                return playerWeapons[i - 1];
            }
        }

        return null;
    }

    public void EquipWeapon(Weapon givenWeapon)
    {
        if (ReferenceEquals(givenWeapon, currentWeapon))
        {
            //currentWeapon.Mobility();
            return;
        }

        if (currentWeapon != null)
        {
            currentWeapon.UnSubscribe();
            currentWeapon.gameObject.SetActive(false);
        }
        currentWeapon = givenWeapon;
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.Initialize();
        if (!GameManager.Instance.assets.PlayerController.GetIsGrounded)
        {
            currentWeapon.Mobility();
        }

        OnSwitchWeapon?.Invoke();
        
        //m_actor.IsInAttackAnim = false;
    }


    public void DisableInAttackAnim()
    {

        m_actor.DisableOnInAnim();
    }
    public void EnableInAttackAnim()
    {

        m_actor.EnableOnInAnim();
    }

}
