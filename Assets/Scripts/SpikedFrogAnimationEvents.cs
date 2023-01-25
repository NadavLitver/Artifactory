using UnityEngine;

public class SpikedFrogAnimationEvents : MonoBehaviour
{
    [SerializeField] GameObject fatherObject;
    
    public void DestroySelf()
    {
        Debug.Log("destryong self");
        fatherObject.SetActive(false);
    }
}
