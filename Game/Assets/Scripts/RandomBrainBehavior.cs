using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBrainBehavior : MonoBehaviour
{
    public float TimeBetweenLaunch = 1f;
    private float TimeCounter;

    GameState gameState;

    void Start()
    {
        TimeCounter = 0f;
        gameState = (GameState)FindObjectOfType<GameState>();
    }

    
    void Update()
    {
        TimeCounter += Time.deltaTime;
        if(TimeCounter > TimeBetweenLaunch){
            TimeCounter -= TimeBetweenLaunch;
            if(gameState.GetInactiveCount() > 0){
                int r_orb = Random.Range(0, 5);
                for(int i = 0; i < r_orb; i++){
                    ConfigAndLaunchOrb();
                }
            }
        }
        
    }


    void ConfigAndLaunchOrb(){
        float r_speed = Random.Range(1.5f, 10f);
        float r_angle = Random.Range(Mathf.Deg2Rad*-105f, Mathf.Deg2Rad*-75f);
        Vector2 r_vel = new Vector2(Mathf.Cos(r_angle), Mathf.Sin(r_angle)) * r_speed;
        float r_scale = Random.Range(0.5f, 1.5f);
        float r_startX = Random.Range(-gameState.screenBounds.x/2, gameState.screenBounds.x/2);

        gameState.Launch(r_vel, r_startX, r_scale);
    }
}
