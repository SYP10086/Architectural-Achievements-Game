using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSoundControl : MonoBehaviour
{
    AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.volume = PlayerPrefs.GetFloat("“Ù–ß“Ù¡ø", 0.5f);
    }
    void Update()
    {
        m_AudioSource.volume = MusicSliderControl.hitVoice;
        if (Input.GetMouseButtonDown(0))
        {
            m_AudioSource.time = 0.05f;
            m_AudioSource.Play();
        }
            
    }
}
