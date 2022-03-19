using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehavior : MonoBehaviour
{
    public int OrbID;
    public bool Active = false;
    public Vector2 Velocity;
    public float StartX;
    public float Scale;


    GameState gameState;
 
    // Start is called before the first frame update
    void Start()
    {
        gameState = (GameState)FindObjectOfType<GameState>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Active){
            transform.Translate(Time.deltaTime * Velocity);
        }
    }

    public void ChangeParams(Vector2 i_Velocity, float i_StartX, float i_Scale){
        Velocity = i_Velocity;
        StartX = i_StartX;
        Scale = i_Scale;
    }

    public void Launch(){
        transform.position = new Vector3(StartX, gameState.screenBounds.y, 0f);
        transform.localScale = new Vector3(Scale, Scale, 0f);
        Active = true;
    }

    public void OnCollect(){
        Active = false;
        Velocity = Vector3.zero;
        transform.position = new Vector3(0f, gameState.screenBounds.y + 100, 0f);
    }
}
