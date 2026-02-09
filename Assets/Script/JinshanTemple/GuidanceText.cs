using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceText : MonoBehaviour
{
    private int lastDetectedValue;
    // Start is called before the first frame update
    void Start()
    {
        lastDetectedValue = -1;
    }

    // Update is called once per frame
    void Update()
    {
        int currentValue = DestinationMovement.DestinationCount;

        if (currentValue != lastDetectedValue)
        {

            HandleDestinationCountChange(currentValue);

            lastDetectedValue = currentValue;
        }
    }
    void HandleDestinationCountChange(int currentValue) 
    { 
        switch(DestinationMovement.DestinationCount)
        {
            case 0:
                gameObject.SetActive(true); break;//后面改其他文本

        }
    }

}
