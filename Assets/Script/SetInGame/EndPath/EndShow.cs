using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndShow : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    double targetPointx1, targetPointy1;
    GameObject O1, O2, O3;
    int part=1;
    [SerializeField]
    double decreaseSpeed, moveSpeed;
    float t,d=0.5f;
    //Vector3 v = (B - A).normalized;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        O1 = GameObject.Find("2Í¤×Ó");
        O2 = GameObject.Find("4ËÂÃí");
        O3 = GameObject.Find("3Â¥");
    }
    Vector3 C(Vector3 v)
    {
        return new Vector3(v.x,v.y,0);
    }
    void Update()
    {
        if (EndInformation.end)
        {
            switch (part)
            {
                case 1:
                    if(Vector3.Distance(C(O1.transform.position), C(transform.position)) >= d)
                    transform.position += (C(O1.transform.position)- C(transform.position)).normalized * (float)moveSpeed * Time.deltaTime;
                    if (cam.orthographicSize > 2)
                        cam.orthographicSize -= (float)decreaseSpeed * Time.deltaTime;
                    else
                        cam.orthographicSize = 2;
                    if (cam.orthographicSize==2&& Vector3.Distance(C(O1.transform.position), C(transform.position))<=d)
                    {
                        t += Time.deltaTime;
                        if(t>=3)
                        {
                            part = 2;
                            t = 0;
                        }
                    } 
                    break;
                case 2:
                    if (Vector3.Distance(new Vector3((float)targetPointx1, (float)targetPointy1, 0), C(transform.position)) >= d)
                        transform.position += (new Vector3((float)targetPointx1, (float)targetPointy1,0)- C(transform.position)).normalized * (float)moveSpeed * Time.deltaTime;
                    if (cam.orthographicSize <3)
                        cam.orthographicSize += (float)decreaseSpeed * Time.deltaTime;
                    else
                        cam.orthographicSize = 3;
                    if (cam.orthographicSize == 3 && Vector3.Distance(new Vector3((float)targetPointx1, (float)targetPointy1, 0), C(transform.position)) <= d)
                    {
                        part = 3;
                    }
                    break;
                case 3:
                    if (Vector3.Distance(C(O2.transform.position), C(transform.position)) >= d)
                        transform.position += (C(O2.transform.position) - C(transform.position)).normalized * (float)moveSpeed * Time.deltaTime;
                    if (cam.orthographicSize > 2)
                        cam.orthographicSize -= (float)decreaseSpeed * Time.deltaTime;
                    else
                        cam.orthographicSize = 2;
                    if (cam.orthographicSize == 2 && Vector3.Distance(C(O2.transform.position), C(transform.position)) <= d)
                    {
                        t += Time.deltaTime;
                        if (t >= 3)
                        {
                            part = 4;
                            t = 0;
                        }
                    }
                    break;
                case 4:
                    if (Vector3.Distance(C(O3.transform.position), C(transform.position)) >= d)
                        transform.position += (C(O3.transform.position) - C(transform.position)).normalized * (float)moveSpeed * Time.deltaTime;
                    if (cam.orthographicSize > 2)
                        cam.orthographicSize -= (float)decreaseSpeed * Time.deltaTime;
                    else
                        cam.orthographicSize = 2;
                    if (cam.orthographicSize == 2 && Vector3.Distance(C(O3.transform.position), C(transform.position)) <= d)
                    {
                        t += Time.deltaTime;
                        if (t >= 3)
                        {
                            part = 5;
                            t = 0;
                        }
                    }
                    break;
                case 5:
                    if (Vector3.Distance(new Vector3(0,0,0), C(transform.position)) >= d)
                        transform.position += (new Vector3(0, 0, 0) - C(transform.position)).normalized * (float)moveSpeed * Time.deltaTime;
                    if (cam.orthographicSize <=3 )
                        cam.orthographicSize += (float)decreaseSpeed * Time.deltaTime;
                    else
                        cam.orthographicSize = 3;
                    if (cam.orthographicSize == 3 && Vector3.Distance(new Vector3(0, 0, 0), C(transform.position)) <= d)
                    {
                        t += Time.deltaTime;
                        if (t >= 0.1)
                        {
                            part = 6;
                            t = 0;

                        }
                    }
                    break;
                case 6:
                        transform.position = (new Vector3(0, 0, transform.position.z));
                    if (cam.orthographicSize <= 5)
                        cam.orthographicSize += (float)decreaseSpeed * Time.deltaTime;
                    else
                        cam.orthographicSize = 5;
                    if (cam.orthographicSize == 5)
                    {
                        t += Time.deltaTime;
                        if (t >= 1)
                        {
                            part = 0;
                            t = 0;
                            EndInformation.endOver = true;
                            EndInformation.end = false;
                        }
                    }
                    break;
            }
        }
    }
    
}
