using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomDefense : State
{
    [SerializeField] float DefenseCoolDown;
    StoneShroomStateHandler handler;
    float currentTime;
    public override State RunCurrentState()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= DefenseCoolDown)
        {
            Debug.Log("shroom is now defending");
            handler.ShroomActor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
            currentTime = 0;
            return handler.ShroomNotice;
        }
        return this;
    }
    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
