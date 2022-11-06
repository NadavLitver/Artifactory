using UnityEngine;

public class SpikedFrogAnimationEvents : MonoBehaviour
{
    [SerializeField] GameObject fatherObject;
    public void DestroySelf()
    {
        fatherObject.SetActive(false);
    }
}
