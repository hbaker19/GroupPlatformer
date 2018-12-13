using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistant : MonoBehaviour {

    public static Persistant persistant;
    public int score = 0;
    public int lives = 3;
    private int scoreThreshold;
    public int scorePerExtraLife = 1000;
    public int ammunition = 0;

    private void Awake()
    {
        if(persistant == null)
        {
            DontDestroyOnLoad(gameObject);
            persistant = this;
            scoreThreshold = scorePerExtraLife;
        }
        else if(persistant != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) { Debug.Log(score); }
        if(Input.GetKeyDown(KeyCode.L)) { Debug.Log(lives); }
        if(Input.GetKeyDown(KeyCode.Equals)) { score += 100; }
        if (Input.GetKeyDown(KeyCode.Minus)) { score -= 100; }
        if(Input.GetKeyDown(KeyCode.Ampersand)) { ammunition++; }
        if (score >= scoreThreshold)
        {
            lives++;
            scoreThreshold += scorePerExtraLife;
        }
    }
}
