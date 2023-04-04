using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IZoomAble 
{

    public ScrollRect Scroller();
    public void ZoomIn();
    public void ZoomOut();

    public void ClampZoom();
    
}
