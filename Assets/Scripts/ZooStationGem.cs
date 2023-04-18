using Sirenix.OdinInspector;
using UnityEngine;

public class ZooStationGem : MonoBehaviour
{
    [SerializeField] private GameObject greenGem;
    [SerializeField] private GameObject redGem;
    [SerializeField] private GameObject grayGem;
    [SerializeField] private ZooActiveSlot refSlot;

    public ZooActiveSlot RefSlot { get => refSlot; }

    private void Awake()
    {
        SetGrayActive();
    }
    public void CacheRefSlot(ZooActiveSlot slot)
    {
        refSlot = slot;
    }
    public void SetRedActive()
    {

        greenGem.SetActive(false);
        redGem.SetActive(true);
        grayGem.SetActive(false);
    }
    public void SetGrayActive()
    {
        greenGem.SetActive(false);
        redGem.SetActive(false);
        grayGem.SetActive(true);
    }
    public void SetGreenActive()
    {

        greenGem.SetActive(true);
        redGem.SetActive(false);
        grayGem.SetActive(false);
    }
}
