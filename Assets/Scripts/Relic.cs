using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RelicEffectsEnum
{
    RubberDuck,
    LightningEmblem,
    HealingGoblet
}

public class Relic : MonoBehaviour
{
    [SerializeField] StatusEffectEnum myEffect;

    public StatusEffect MyEffect { get => GameManager.Instance.generalFunctions.GetStatusFromType(myEffect); }
    public StatusEffectEnum MyEffectEnum { get => myEffect; set => myEffect = value; }

}
