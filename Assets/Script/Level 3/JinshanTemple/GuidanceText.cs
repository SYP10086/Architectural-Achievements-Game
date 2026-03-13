using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceText : MonoBehaviour
{
    private int lastDetectedValue;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject text;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        lastDetectedValue = -1;
        camera = Camera.main;
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

        if (Input.GetMouseButtonDown(0))
        {
            ToggleObject();
        }
    }
    void HandleDestinationCountChange(int currentValue) 
    { 
        switch(DestinationMovement.DestinationCount)
        {
            
            case 0:
                text.SetActive(true); //后面改其他文本          
                camera.GetComponent<CameraBlurDarken>().enabled = true;
                exitButton.SetActive(true); break;
            case 1:
                text.SetActive(true); //后面改其他文本
                camera.GetComponent<CameraBlurDarken>().enabled = true;
                exitButton.SetActive(true); break;
            case 2:
                text.SetActive(true); //后面改其他文本
            camera.GetComponent<CameraBlurDarken>().enabled = true;
            exitButton.SetActive(true); break;
            case 3:
                text.SetActive(true); //后面改其他文本
            camera.GetComponent<CameraBlurDarken>().enabled = true;
            exitButton.SetActive(true); break;
            case 4:
                text.SetActive(true); //后面改其他文本
            camera.GetComponent<CameraBlurDarken>().enabled = true;
            exitButton.SetActive(true); break;

        }
    }

    public void ToggleObject()
    {
        text.SetActive(false); 
        exitButton.SetActive(false);
        camera.GetComponent<CameraBlurDarken>().enabled = false;
    }

}
