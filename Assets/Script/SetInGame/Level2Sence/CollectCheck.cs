using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(Collect.collectNumber>=4)
            {
                Collect.collectNumber = 0;
                MakeBiger.start = true;
            }
        }
    }
}
