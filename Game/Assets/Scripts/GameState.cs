using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public Vector2 screenBounds;
    public GameObject GameOver_Widget;
    public bool isGameOver = false;

    void Start()
    {
        var cameraHeight = Camera.main.orthographicSize * 2;
        var cameraWidth = Camera.main.aspect * cameraHeight;
        screenBounds = new Vector2(cameraWidth, cameraHeight);
        Debug.Log("Screen size is " + screenBounds.ToString());   
    }

    void Update()
    {
        
    }

    public void GameOver(){
        if (isGameOver){
            return;
        }
        
        isGameOver = true;
        Instantiate(GameOver_Widget, new Vector3(), new Quaternion());
    }
}
