using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChosseBigger : MonoBehaviour
{
    public double time = 0, T = 0.1;
    public GameObject cave,buttom1,buttom2;
    RectTransform rectTransform1, rectTransform2;
    bool[] bigger=new bool[2];
    private void Start()
    {
        cave = GameObject.Find("Canvas");
        rectTransform1 = buttom1.GetComponent<RectTransform>();
        rectTransform2 = buttom2.GetComponent<RectTransform>();
    }
    void Update()
    {
        time -= Time.deltaTime;
        if (time<=0)
        {
            GameObject but = GetOverUI(cave);
            Debug.Log((but != null ? but.name : " "));
            time = T; 
            if("Text(Legacy)"== (but!=null?but.name:" "))
            {
                bigger[0] = true;
            }
            else
            {
                bigger[0] = false;
            }
            if ("Text (Legacy) (1)" == (but != null ? but.name : " "))
            {
                bigger[1] = true;
            }
            else
            {
                bigger[1] = false;
            }
        }
        if (bigger[0])
        {
            
            if (rectTransform1.localScale.x <= 1.2)
                rectTransform1.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
        }
        else {
            if (rectTransform1.localScale.x > 1)
                rectTransform1.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
        }
        if (bigger[1])
        {

            if (rectTransform2.localScale.x <= 1.2)
                rectTransform2.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0);
        }
        else
        {
            if (rectTransform2.localScale.x > 1)
                rectTransform2.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
        }
    }
    public GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[0].gameObject;
        }
        return null;
    }
}
