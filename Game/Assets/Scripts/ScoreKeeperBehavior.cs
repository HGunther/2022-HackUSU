using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeperBehavior : MonoBehaviour
{
    public int Score;

    private float TimeCounter;
    public Text ScoreText;

    public bool IsGameOver;
    void Start()
    {
        Score = 0;
        IsGameOver = false;
        ScoreText.enabled = true;
    }

    // Update is called once per frame
    void Update(){
        TimeCounter += Time.deltaTime;
        if(TimeCounter > 1f){
            TimeCounter -= 1f;
            Score++;
            ScoreText.text = Score.ToString("0000");
        }
    }
}
