using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class  Blur
{
    
    // Start is called before the first frame update
   
    public static void BlurGameObject(GameObject gameObject,Material material)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material = material;

    }
    public static void EndBlur(GameObject gameObject,Material material)
    { 
    Renderer renderer2 = gameObject.GetComponent<Renderer>();
        renderer2.material = material;
    
    }
}
