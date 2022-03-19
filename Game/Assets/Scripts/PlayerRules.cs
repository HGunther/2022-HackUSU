using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRules : MonoBehaviour
{
    GameState gameState_REF;

    // Start is called before the first frame update
    void Start()
    {
        gameState_REF = FindObjectOfType<GameState>();
        if (!gameState_REF){
            Debug.LogWarning("PlayerRules could not find a GameState object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameState_REF){
            gameState_REF = FindObjectOfType<GameState>();
            if (!gameState_REF){
                Debug.LogWarning("PlayerRules could not find a GameState object");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        // if (gameState_REF == null){
        //     Debug.LogError("PlayerRules never found a GameState and now requires it to handle collisions");
        // }

        // if (col.gameObject.GetComponent<OrbBehavior>()){
        //     gameState_REF.GameOver();
        // }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (gameState_REF == null){
            Debug.LogError("PlayerRules never found a GameState and now requires it to handle collisions");
        }

        if (col.gameObject.GetComponent<OrbBehavior>()){
            gameState_REF.GameOver();
        }
    }
}
