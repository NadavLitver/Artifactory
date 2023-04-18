using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Relic", menuName = "Relic")]
public class Relic : ScriptableObject
{
    [SerializeField] StatusEffectEnum myEffect;
    [SerializeField] private string summery;

    public StatusEffect MyEffect { get => GameManager.Instance.generalFunctions.GetStatusFromType(myEffect); }
    public StatusEffectEnum MyEffectEnum { get => myEffect; set => myEffect = value; }
    public string Summery { get => summery; set => summery = value; }
}
