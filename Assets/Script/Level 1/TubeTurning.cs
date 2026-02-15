using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TubeTurning : MonoBehaviour
{
    public GameObject Tube1;
    public GameObject Tube2;
    public GameObject Tube3;
    public GameObject Tube4;
    public int TubeCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                TubeCount++;
                if (TubeCount >= 5)
                {
                    TubeCount = 1;
                }
            }
        }

        if (TubeCount == 1) {
            Tube1.SetActive(true);
            Tube2.SetActive(false);
            Tube3.SetActive(false);
            Tube4.SetActive(false);
        }

        if (TubeCount == 2)
        {
            Tube1.SetActive(false);
            Tube2.SetActive(true);
            Tube3.SetActive(false);
            Tube4.SetActive(false);
        }

        if (TubeCount == 3)
        {
            Tube1.SetActive(false);
            Tube2.SetActive(false);
            Tube3.SetActive(true);
            Tube4.SetActive(false);
        }

        if (TubeCount == 4)
        {
            Tube1.SetActive(false);
            Tube2.SetActive(false);
            Tube3.SetActive(false);
            Tube4.SetActive(true);
        }
    }
}
