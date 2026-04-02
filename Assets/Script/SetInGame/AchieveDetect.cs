using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchieveDetect : MonoBehaviour
{
    public double delaytime = 1.5,time1=0;
    public bool start=false;
    static public bool[] achieve= {false,false,false };
    //int[] shuxin
    //{
    //    get
    //    {
    //        return { };
            
    //    }
    //    set { }
    //}
    private void Awake()
    {
       achieve[0] = (PlayerPrefs.GetInt("achieve[0]", 0)==1);
       achieve[1] = (PlayerPrefs.GetInt("achieve[1]", 0)==1);
       achieve[2] = (PlayerPrefs.GetInt("achieve[2]", 0)==1);
    }
    private void Update()
    {
        if(start)
        {
            time1 += Time.deltaTime;
            if (time1 > delaytime) 
            {
                time1 = 0;
                start=false;
                SceneManager.LoadScene("Options");
            }
        }
    }
    public void PassLevel(int level)
    {
        achieve[level] = true;
        PlayerPrefs.SetInt($"achieve[{level}]",achieve[level] ? 1 : 0);
        Successful.PlaySuccessMusic();
        start=true;
    }
}
