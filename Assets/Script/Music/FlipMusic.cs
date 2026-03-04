using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMusic : MonoBehaviour
{
    AudioSource m_AudioSource;
    public double waitTime = 0.5;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.volume = PlayerPrefs.GetFloat("ÒôÐ§ÒôÁ¿", 0.5f);
    }
    void Update()
    {
        m_AudioSource.volume = MusicSliderControl.hitVoice;
        if(waitTime > 0)
            waitTime-= Time.deltaTime;
            else if(waitTime != -100)
        {
m_AudioSource.Play();
            waitTime = -100;
        }
            

    }
}
