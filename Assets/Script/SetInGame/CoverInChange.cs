using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CoverInChange : MonoBehaviour
{
    [SerializeField]
    GameObject R, L;
    [SerializeField]
    AchieveDetect detect;
    Transform L1, R1;
    public int n;
    private void Start()
    {
        L1=L.transform;
        R1=R.transform;
    }
    private void Update()
    {
        if (detect.start)
        {
            L.transform.position -= new Vector3(L1.position.x/ (float)detect.delaytime * Time.deltaTime*n, 0,0);
            R.transform.position -= new Vector3(R1.position.x / (float)detect.delaytime * Time.deltaTime*n, 0, 0);
        }
    }
}
