using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliderInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Slider soundSlider = GameObject.Find("Sound Slider").GetComponent<Slider>();
        AudioSource soundSource = GameObject.Find("Boom Sound").GetComponent<AudioSource>();
        soundSlider.value = soundSource.volume;
    }
}