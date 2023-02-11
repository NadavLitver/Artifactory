using UnityEngine;

public class CannonChargeFlipper : MonoBehaviour
{
    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
