using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSliderInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Slider musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
        AudioSource musicSource = GameObject.Find("Background Music").GetComponent<AudioSource>();
        musicSlider.value = musicSource.volume;
    }
}