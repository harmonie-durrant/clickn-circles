using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    // scoreText
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI mainMenuButton;
    public TextMeshProUGUI BG_COUNT_Text;
    public TextMeshProUGUI RED_COUNT_Text;
    public TextMeshProUGUI GREEN_COUNT_Text;
    public TextMeshProUGUI GOLD_COUNT_Text;
    // coroutine LoadNextScene
    IEnumerator LoadNextScene()
    {
        mainMenuButton.text = "Main Menu (5)";
        yield return new WaitForSeconds(1f);
        mainMenuButton.text = "Main Menu (4)";
        yield return new WaitForSeconds(1f);
        mainMenuButton.text = "Main Menu (3)";
        yield return new WaitForSeconds(1f);
        mainMenuButton.text = "Main Menu (2)";
        yield return new WaitForSeconds(1f);
        mainMenuButton.text = "Main Menu (1)";
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        // Load the main menu
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (Game_Logic.is_Timed)
        {
            titleText.text = "Time's up!";
        }
        else
        {
            titleText.text = "Game Over!";
        }
        int score = Game_Logic.score;
        scoreText.text = "Your Score Was: " + score;
        highScoreText.text = "Your High Score Is: " + PlayerPrefs.GetInt("HighScore_"+Game_Logic.GM_NO);
        BG_COUNT_Text.text = PlayerPrefs.GetInt("BG_HITS_"+Game_Logic.GM_NO).ToString();
        RED_COUNT_Text.text = PlayerPrefs.GetInt("BAD_CIRCLE_HITS_"+Game_Logic.GM_NO).ToString();
        GREEN_COUNT_Text.text = PlayerPrefs.GetInt("CIRCLE_HITS_"+Game_Logic.GM_NO).ToString();
        GOLD_COUNT_Text.text = PlayerPrefs.GetInt("PW_CIRCLE_HITS_"+Game_Logic.GM_NO).ToString();
        // wait 5 sconds and then load the next scene
        StartCoroutine(LoadNextScene());
    }
}