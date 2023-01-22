using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingSprite : MonoBehaviour
{
     Color colorOne = new Color(1,1,1,0.5f);
     Color colorTwo = new Color(1,1,1,1);
    Color Alpha;
    [SerializeField] SpriteRenderer m_renderer;
    [SerializeField] float pace = 1.5f;

    private void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
      
           

        Alpha = Color.Lerp(colorOne, colorTwo, Mathf.PingPong(Time.time, pace));
        m_renderer.color = Alpha;


    }
  
}
