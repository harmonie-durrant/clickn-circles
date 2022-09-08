using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class splashText : MonoBehaviour
{
    private float time = 0.0f;
    private float secs = 0.0f;
    public float interpolationPeriod = 0.01f;
    public bool Switch = false;
    public TextMeshProUGUI SplashText;
    public float shrink_speed = 1f;
    
    void Start()
    {
        string[] str1;
        str1 = new string[5]{ "Don't click the red circles!", "Beat my high score of 1 :)", "Who has the best reaction time?", "Get them circles before they get you!", "\"Just keep clickn', just keep clickn'!\"" };
        int randomnumber = Random.Range(0, 4);
        SplashText.text = str1[randomnumber];
    }

    void Update()
    {
        
        time += Time.deltaTime;
        if (time >= interpolationPeriod) {
            time = time - interpolationPeriod;
            if(secs >= 1.0f) {
                secs = 0.0f;
                Switch = !Switch;
            } else {
                secs += 1.0f;
            }
        }
        if(Switch) {
            transform.localScale -= new Vector3(1f * shrink_speed * Time.deltaTime, 1f * shrink_speed * Time.deltaTime, 1f * shrink_speed * Time.deltaTime);
        } else {
            transform.localScale += new Vector3(1f * shrink_speed * Time.deltaTime, 1f * shrink_speed * Time.deltaTime, 1f * shrink_speed * Time.deltaTime);
        }
    }
}
