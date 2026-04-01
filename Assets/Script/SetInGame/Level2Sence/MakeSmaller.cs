using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSmaller : MonoBehaviour
{
    public float addNum = 0.8f;
    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x>=0)
        transform.localScale-= new Vector3(addNum * Time.deltaTime, addNum * Time.deltaTime, 0);
    }
}
