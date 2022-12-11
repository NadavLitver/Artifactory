using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapConnectionPoint : MonoBehaviour
{
    [SerializeField] ExitLocationInfo locationInfo;
    [SerializeField] RectTransform line;
    public ExitLocationInfo LocationInfo { get => locationInfo; set => locationInfo = value; }
    public RectTransform Line { get => line; set => line = value; }

    public void ConnectLine(Transform destenation, float offset = 1)
    {
        Vector3 direction = destenation.transform.position - transform.position;
        //RectTransform lineRect = (RectTransform)line.transform.GetChild(0).transform;
        line.gameObject.SetActive(true);
        Line.sizeDelta = new Vector2(direction.magnitude * offset, 8);
        if (locationInfo.HorizontalPos == ExitLocationHorizontal.LEFT)
        {
            line.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        line.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
