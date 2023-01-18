using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingSprite : MonoBehaviour
{
    [SerializeField] Color colorOne;
    [SerializeField] Color colorTwo;
    Color Alpha;
    [SerializeField] SpriteRenderer m_renderer;
    [SerializeField] float pace = 1.5f;

  
  
    void Update()
    {
      
           

        Alpha = Color.Lerp(colorOne, colorTwo, Mathf.PingPong(Time.time, pace));
        m_renderer.color = Alpha;


    }
  
}
