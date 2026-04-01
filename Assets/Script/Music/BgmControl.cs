using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmControl : MonoBehaviour
{
    AudioSource m_AudioSource;
    static double time;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Beginning")
            time = 0;
        m_AudioSource=GetComponent<AudioSource>();
        m_AudioSource.volume = PlayerPrefs.GetFloat("“Ù¿÷“Ù¡ø", 0.5f);
        m_AudioSource.time = (float)time;
    }
    void Update()
    {
        m_AudioSource.volume = MusicSliderControl.musicVoice;
        time = m_AudioSource.time;
    }
}
