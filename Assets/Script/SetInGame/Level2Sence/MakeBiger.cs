using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBiger : MonoBehaviour
{
    public bool start=false;
    public double addTime=0;
    public float addNum=0.5f;

    void Update()
    {
        if(start)
        {
            addTime+=Time.deltaTime;
            this.gameObject.transform.localScale += new Vector3(addNum*Time.deltaTime, addNum*Time.deltaTime, 0);
        }
    }
}
