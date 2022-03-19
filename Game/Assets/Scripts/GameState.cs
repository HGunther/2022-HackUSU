using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public Vector2 screenBounds;
    public GameObject GameOver_Widget;
    public bool isGameOver = false;
    public OrbPoolBehavior OrbPool;

    void Start()
    {
        var cameraHeight = Camera.main.orthographicSize * 2;
        var cameraWidth = Camera.main.aspect * cameraHeight;
        screenBounds = new Vector2(cameraWidth, cameraHeight);
        Debug.Log("Screen size is " + screenBounds.ToString());   

        //OrbPool.Start();
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
