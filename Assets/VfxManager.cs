using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VisualEffect{
    WhiteHitEffect,
    RedHitEffect
}
public class VfxManager : MonoBehaviour
{
    [SerializeField] List<VisualEffectData> visualEffectDatas;

    public void Play(VisualEffect _effect,Vector2 worldPos)
    {
        Instantiate(GetEffectByEnum(_effect),worldPos,Quaternion.identity);
    }
    public void Play(VisualEffect _effect, Vector2 worldPos,Quaternion rotation)
    {
         Instantiate(GetEffectByEnum(_effect), worldPos, rotation);
    }
    public void Play(VisualEffect _effect, Transform Parent)
    {
         Instantiate(GetEffectByEnum(_effect), Parent);
    }

    //----------------------------------------------//

    public GameObject PlayAndGet(VisualEffect _effect, Vector2 worldPos)
    {
        return Instantiate(GetEffectByEnum(_effect), worldPos, Quaternion.identity);
       
    }

    public GameObject PlayAndGet(VisualEffect _effect, Vector2 worldPos, Quaternion rotation)
    {
        return Instantiate(GetEffectByEnum(_effect), worldPos, rotation);
       

    }
    public GameObject PlayAndGet(VisualEffect _effect, Transform Parent)
    {
        return Instantiate(GetEffectByEnum(_effect), Parent);
        

    }

    //----------------------------------------------//

    public GameObject GetEffectByEnum(VisualEffect _effect)
    {
        for (int i = 0; i < visualEffectDatas.Count; i++)
        {
            if(visualEffectDatas[i].m_effect == _effect)
            {
                return visualEffectDatas[i].effectPrefab;
            }
        }
        return null;
    }
}


[System.Serializable]
public class VisualEffectData
{
    public VisualEffect m_effect;
    public GameObject effectPrefab;
}