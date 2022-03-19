using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryWallBehavior : MonoBehaviour
{

    GameState gameState;

    void Start(){
        gameState = (GameState)FindObjectOfType<GameState>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<OrbBehavior>()){
            int id = other.gameObject.GetComponent<OrbBehavior>().OrbID;
            gameState.Collect(id);
        }
    }
}
