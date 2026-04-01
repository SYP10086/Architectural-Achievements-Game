using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectCheck : MonoBehaviour
{
    GameObject text;
    private void Start()
    {
        text = GameObject.Find("UI/Text (Legacy)");
        text.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{

        //    if (Collect.collectNumber >= 4)
        //    {
        //        Collect.collectNumber = 0;
        //        MakeBiger.start = true;
        //    }
        //    else if(!MakeBiger.start)
        //    {
        //        text.SetActive(true);
        //        text.GetComponent<Text>().text = $"ªπ≤Ó{4- Collect.collectNumber}∏ˆ£¨‘Ÿ’“’“∞…";
        //    }
        //}
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(text!=null)
                text .SetActive(false);
        }
    }
}
