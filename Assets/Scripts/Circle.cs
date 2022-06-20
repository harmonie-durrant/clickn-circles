using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{

    public Game_Logic game_logic;
    public float shrink_speed = 1f;

    public static bool is_Paused = false;

    // Start is called before the first frame update
    public void Start()
    {
        //find a game_logic object with tag "GameLogic"
        game_logic = GameObject.FindObjectOfType<Game_Logic>();
        // set a random size for the circle
        float size = Random.Range(2.5f, 4f);
        transform.localScale = new Vector3(size, size, size);
        // set a random color for the circle
        float c = Random.Range(0f, 1f);
        if (c < 0.5f) {
            GetComponent<Renderer>().material.color = Color.green;
        } else {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    public static void Pause()
    {
        is_Paused = true;
    }

    public static void UnPause()
    {
        is_Paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_Paused)
        {
            //decrease the size of the circle every frame
            transform.localScale -= new Vector3(1f * shrink_speed * Time.deltaTime, 1f * shrink_speed * Time.deltaTime, 1f * shrink_speed * Time.deltaTime);
            // destroy the circle when it's too small
            if (transform.localScale.x < 0.01f)
            {
                game_logic.HitBadCircle();
            }
        }
    }
}
