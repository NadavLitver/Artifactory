using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Relic", menuName = "Relic")]
public class Relic : ScriptableObject
{
    [SerializeField] StatusEffectEnum myEffect;

    public StatusEffect MyEffect { get => GameManager.Instance.generalFunctions.GetStatusFromType(myEffect); }
    public StatusEffectEnum MyEffectEnum { get => myEffect; set => myEffect = value; }

}
