using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakeBiger : MonoBehaviour
{
    static public bool start=false;
    static public double addTime=0;
    public float addNum=0.5f;
    public GameObject mainCamera;
    void Update()
    {
        if(start)
        {
            addTime+=Time.deltaTime;
            this.gameObject.transform.localPosition= new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y,0);
            this.gameObject.transform.localScale += new Vector3(addNum*Time.deltaTime, addNum*Time.deltaTime, 0);
            if (addTime >= 1)
            {
                start = false;
                addTime = 0;
                SceneManager.LoadScene("Level 2 After");
            }
        }
        else
        {
            this.gameObject.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y, 0);
            this.gameObject.transform.localScale=Vector3.zero;
        }
    }
}
