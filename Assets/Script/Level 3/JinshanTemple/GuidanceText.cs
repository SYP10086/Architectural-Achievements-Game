using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidanceText : MonoBehaviour
{
    private int lastDetectedValue;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject text;          
    [SerializeField] private TextMeshPro targetText;   
    private Camera camera;

    void Start()
    {
        lastDetectedValue = -1;
        camera = Camera.main;

    }

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
        if (targetText == null) return;

        if (!targetText.enabled)
            targetText.enabled = true;

        if (text != null) text.SetActive(true);

        camera.GetComponent<CameraBlurDarken>().enabled = true;
        exitButton.SetActive(true);
    }

    public void ToggleObject()
    {
        if (targetText != null)
        {
            targetText.text = "";
            targetText.enabled = false;
        }

        if (text != null) text.SetActive(false);
        exitButton.SetActive(false);
        camera.GetComponent<CameraBlurDarken>().enabled = false;
    }
}