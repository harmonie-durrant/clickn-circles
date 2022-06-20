using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Game_Logic : MonoBehaviour
{
    //score and timer/lives
    public static int score;
    private int lives;
    public static int timer;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI hishscoretext;

    //gamemode
    public static bool is_Timed;
    private bool is_Paused = false;

    //audio
    AudioSource dingAudio;
    AudioSource boomAudio;

    // Circle Prefabs
    public GameObject BadCircle_prefab;
    public GameObject Circle_prefab;
    private GameObject BadCircle;
    private GameObject GoodCircle;

    IEnumerator Countdown() {
        for(;;) {
            timer -= 1;
            timerText.text = "Time: " + timer;
            if (timer == 0) {
                timerText.text = "Time's up!";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartTimedGM(int time)
    {
        Time.timeScale = 1;
        is_Timed = true;
        timer = time;
        score = 0;
        StartCoroutine("Countdown");
        dingAudio = GameObject.Find("Ding Sound").GetComponent<AudioSource>();
        Bad_Circle.UnPause();
        Circle.UnPause();
        SpawnCircles();
    }

    public void StartLivesGM(int lives)
    {
        Time.timeScale = 1;
        is_Timed = false;
        timer = lives;
        score = 0;
        dingAudio = GameObject.Find("Ding Sound").GetComponent<AudioSource>();
        Bad_Circle.UnPause();
        Circle.UnPause();
        SpawnCircles();
    }
    
    void ClearCircles()
    {
        Destroy(GoodCircle);
        Destroy(BadCircle);
    }

    void SpawnCircles()
    {
        ClearCircles();
        // pick two locations that are more than 2f apart
        Vector3 loc1 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
        Vector3 loc2 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
        while (Vector3.Distance(loc1, loc2) < 2f) {
            loc2 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
        }
        GoodCircle = Instantiate(Circle_prefab, loc1, Quaternion.identity);
        BadCircle = Instantiate(BadCircle_prefab, loc2, Quaternion.identity);
    }

    void HitBackground()
    {
        if (!is_Timed)
        {
            timer -= 1;
        }
        score -= 1;
        UpdateUI();
    }

    void HitGoodCircle()
    {
        score += 1;
        UpdateUI();
        SpawnCircles();
    }

    public void HitBadCircle()
    {
        if (!is_Timed)
        {
            timer -= 1;
        }
        score -= 2;
        UpdateUI();
        SpawnCircles();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        hishscoretext.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        if (is_Timed)
        {
            timerText.text = "Time: " + timer;
        }
        else
        {
            timerText.text = "Lives: " + timer;
        }
    }

    public void Pause()
    {
        if (is_Paused)
        {
            Bad_Circle.UnPause();
            Circle.UnPause();
            is_Paused = false;
            Time.timeScale = 1;
            if (is_Timed)
            {
                StartCoroutine("Countdown");
            }
        }
        else
        {
            Bad_Circle.Pause();
            Circle.Pause();
            is_Paused = true;
            Time.timeScale = 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (timer < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //check for if circle is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (!is_Paused)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                
                if (GoodCircle.GetComponent<CircleCollider2D>().bounds.Contains(worldPos))
                {
                    HitGoodCircle();
                }
                else if (BadCircle.GetComponent<CircleCollider2D>().bounds.Contains(worldPos))
                {
                    HitBadCircle();
                }
                else
                {
                    if ((mousePos.x > (Screen.width/15)) && (mousePos.y > (Screen.height/15))) {
                        HitBackground();
                    }
                }
            }
        }
    }
}
