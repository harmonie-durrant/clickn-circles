using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class bestScoreText : MonoBehaviour
{
    public string GM_NO;
    public TextMeshProUGUI TextGO;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore_"+GM_NO)) {
            PlayerPrefs.SetInt("HighScore_"+GM_NO, 0);
        }
        TextGO.text = "Best: "+PlayerPrefs.GetInt("HighScore_"+GM_NO).ToString();
    }
}