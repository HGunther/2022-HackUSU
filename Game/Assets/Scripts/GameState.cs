using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{

    public Vector2 screenBounds;
    public GameObject GameOver_Widget;
    public bool isGameOver = false;
    bool hasGameStarted = false;
    public OrbPoolBehavior OrbPool;
    public MLDodgingAgent player;

    public int Score;
    private float TimeToNextPoint = 0.1f;
    private float TimeCounter;
    public Text ScoreText;

    void Start()
    {
        isGameOver = false;
        hasGameStarted = false;
        Time.timeScale = 1;

        var cameraHeight = Camera.main.orthographicSize * 2;
        var cameraWidth = Camera.main.aspect * cameraHeight;
        screenBounds = new Vector2(cameraWidth, cameraHeight);
        Debug.Log("Screen size is " + screenBounds.ToString());  

        Score = 0;
        TimeCounter = 0f; 
    }

    void Update()
    {
        if (!hasGameStarted){
            hasGameStarted = true;
        }

        TimeCounter += Time.deltaTime;
        if(TimeCounter > TimeToNextPoint){
            TimeCounter -= TimeToNextPoint;
            Score++;
            if (ScoreText){
                ScoreText.text = Score.ToString("000000");
            }
        }
    }

    public void ResetGame(){
        if (hasGameStarted){
            hasGameStarted = false;
            OrbPool.CollectAll();
            Score = 0;
            player.ResetPlayer();
        }
    }

    public void GameOver(){
        if (isGameOver){
            return;
        }
        
        isGameOver = true;
        Instantiate(GameOver_Widget, new Vector3(), new Quaternion());
        Time.timeScale = 0;
    }

    public void PlayAgain(){
        Debug.Log("Reloading level");
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene is '" + scene.name + "'.");
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }
    public void QuitGame(){
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void Collect(int i){
        OrbPool.Collect(i);
    }

    public void Launch(Vector2 i_Velocity, float i_StartX, float i_Scale){
        OrbPool.Launch(i_Velocity, i_StartX, i_Scale);
    }

    public int GetActiveCount(){
        return OrbPool.GetActiveCount();
    }

    public int GetInactiveCount(){
        return OrbPool.GetInactiveCount();
    }

    public int GetTotalCount(){
        return OrbPool.GetTotalCount();
    }

//Testing Functions
    public void RandomLaunch(){
        OrbPool.RandomLaunch();
    }

    public void CollectFirstActive(){
        OrbPool.CollectFirstActive();
    }


}
