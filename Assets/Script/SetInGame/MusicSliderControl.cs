using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliderControl : MonoBehaviour
{
    static public float musicVoice, hitVoice;
    public int voiceMode; //0代表音乐，1代表音量
    Slider slider;
    public GameObject numberShower;
    public TextMeshProUGUI showText;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        showText = numberShower.GetComponent<TextMeshProUGUI>();
        slider.value= PlayerPrefs.GetFloat($"{numberShower.name}",0.5f);
        if (slider != null)
        {
            switch (voiceMode)
            {
                case 0:
                    musicVoice = slider.value;
                    break;
                case 1:
                    hitVoice = slider.value;
                    break;

            }
            showText.text = $"{System.Math.Round(slider.value, 2) * 100}%";
        }
    }
    public void ValueChange()
    {
        if (slider != null)
        {
            switch (voiceMode)
            {
                case 0:
                    musicVoice = slider.value;
                    break;
                case 1:
                    hitVoice = slider.value;
                    break;

            }
            showText.text = $"{System.Math.Round(slider.value, 2) * 100}%";
            PlayerPrefs.SetFloat($"{numberShower.name}", slider.value);
        }
    }

}
