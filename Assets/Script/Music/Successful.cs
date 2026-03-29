using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Successful : MonoBehaviour
{
    static AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.volume = PlayerPrefs.GetFloat("“Ù–ß“Ù¡ø", 0.5f);
    }
    static public void PlaySuccessMusic()
    {
        m_AudioSource.volume = MusicSliderControl.hitVoice;
        m_AudioSource.Play();
    }
}
