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

    public static int GM_NO = 0;

    //gamemode
    public static bool is_Timed;
    private bool is_Paused = false;

    //audio
    AudioSource dingAudio;
    AudioSource dingAudioAlt;
    AudioSource boomAudio;

    // Circle Prefabs
    public GameObject BadCircle_prefab;
    public GameObject Circle_prefab;
    public GameObject PowerCircle_prefab;
    private GameObject BadCircle;
    private GameObject GoodCircle;
    private GameObject PowerCircle;

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
        if(time == 30){
            GM_NO = 0;
        } else if(time == 60) {
            GM_NO = 1;
        } else {
            GM_NO = 2;
        }
        score = 0;
        StartCoroutine("Countdown");
        dingAudio = GameObject.Find("Ding Sound").GetComponent<AudioSource>();
        dingAudioAlt = GameObject.Find("Ding Sound Alt").GetComponent<AudioSource>();
        boomAudio = GameObject.Find("Boom Sound").GetComponent<AudioSource>();
        Bad_Circle.UnPause();
        Circle.UnPause();
        // PowerCircle.UnPause();
        SpawnCircles();
    }

    public void StartLivesGM(int lives)
    {
        Time.timeScale = 1;
        is_Timed = false;
        timer = lives;
        if(lives == 3){
            GM_NO = 3;
        } else if(lives == 6) {
            GM_NO = 4;
        } else {
            GM_NO = 5;
        }
        score = 0;
        dingAudio = GameObject.Find("Ding Sound").GetComponent<AudioSource>();
        dingAudioAlt = GameObject.Find("Ding Sound Alt").GetComponent<AudioSource>();
        boomAudio = GameObject.Find("Boom Sound").GetComponent<AudioSource>();
        Bad_Circle.UnPause();
        Circle.UnPause();
        // PowerCircle.UnPause();
        SpawnCircles();
    }
    
    void ClearCircles()
    {
        Destroy(GoodCircle);
        //if PowerCircle exists, destroy it
        if (PowerCircle != null)
        {
            Destroy(PowerCircle);
        }
        Destroy(BadCircle);
    }

    void SpawnCircles()
    {
        ClearCircles();
        Vector3 loc1;
        Vector3 loc2;
        Vector3 loc3;
        // pick two locations that are more than 2f apart
        loc1 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
        loc2 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
        while (Vector3.Distance(loc1, loc2) < 2f) {
            loc2 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
        }
        // make loc3 only 22% times
        if (Random.Range(0, 100) >= 88) {
            loc3 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
            while (Vector3.Distance(loc1, loc3) < 2f || Vector3.Distance(loc2, loc3) < 2f) {
                loc3 = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 3f), 0);
            }
        } else {
            loc3 = new Vector3(15,15,1);
        }
        GoodCircle = Instantiate(Circle_prefab, loc1, Quaternion.identity);
        BadCircle = Instantiate(BadCircle_prefab, loc2, Quaternion.identity);
        if(loc3.y != 1) {
            PowerCircle = Instantiate(PowerCircle_prefab, loc3, Quaternion.identity);
        }
    }

    void HitBackground()
    {
        PlayerPrefs.SetInt("BG_HITS_"+GM_NO, PlayerPrefs.GetInt("BG_HITS_"+GM_NO)+1);
        if (!is_Timed)
        {
            timer -= 1;
        }
        score -= 1;
        UpdateUI();
    }

    void HitGoodCircle()
    {
        PlayerPrefs.SetInt("CIRCLE_HITS_"+GM_NO, PlayerPrefs.GetInt("CIRCLE_HITS_"+GM_NO)+1);
        score += 1;
        UpdateUI();
        SpawnCircles();
        // play sound
        dingAudio.Play();
    }

    void HitPowerCircle()
    {
        PlayerPrefs.SetInt("PW_CIRCLE_HITS_"+GM_NO, PlayerPrefs.GetInt("PW_CIRCLE_HITS_"+GM_NO)+1);
        score += 2;
        UpdateUI();
        SpawnCircles();
        dingAudioAlt.Play();
    }

    public void HitBadCircle()
    {
        PlayerPrefs.SetInt("BAD_CIRCLE_HITS_"+GM_NO, PlayerPrefs.GetInt("BAD_CIRCLE_HITS_"+GM_NO)+1);
        if (!is_Timed)
        {
            timer -= 1;
        }
        score -= 2;
        UpdateUI();
        SpawnCircles();
        boomAudio.Play();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        hishscoretext.text = "High Score: " + PlayerPrefs.GetInt("HighScore_"+GM_NO);
        if (score > PlayerPrefs.GetInt("HighScore_"+GM_NO))
        {
            PlayerPrefs.SetInt("HighScore_"+GM_NO, score);
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
            //PowerCircle.UnPause();
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
                else if (PowerCircle.GetComponent<CircleCollider2D>().bounds.Contains(worldPos))
                {
                    HitPowerCircle();
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
