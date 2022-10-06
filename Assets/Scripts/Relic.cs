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
    private StatusEffect effect;
    [SerializeField] RelicEffectsEnum myEffect;




    public RelicEffectsEnum MyEffect { get => myEffect; set => myEffect = value; }

}
