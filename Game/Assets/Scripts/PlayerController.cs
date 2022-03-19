using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 startPos;

    bool mouseControl = false;
    Vector3 mousePos;
    public float mouseDeadzone = 5.0f;

    GameState gameState_REF;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        mousePos = Input.mousePosition;

        gameState_REF = FindObjectOfType<GameState>();
        if (gameState_REF == null){
            Debug.LogWarning("PlayerController could not find a GameState object");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameState_REF.isGameOver){
            GetInput();
        }
    }

    void GetInput(){
        // Detect input scheme
        if (mouseControl){
            // Detect change from mouse to keyboard
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)){
                Debug.Log("Switching to keyboard input");
                mouseControl = false;
            }
        } else {
            // Detect change from keyboard to mouse
            var mouseDif = Input.mousePosition - mousePos;
            if (mouseDif.magnitude > mouseDeadzone){
                mouseControl = true;
                Debug.Log("Switching to mouse input");
            }
            mousePos = Input.mousePosition;
        }

        if (mouseControl){
            // Mouse control
            var worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var maxMovement = speed * Time.deltaTime;
            if (Mathf.Abs(worldMousePos.x - transform.position.x) >= maxMovement){
                // move the full distance
                if (worldMousePos.x > transform.position.x){
                    // move right
                    transform.Translate(maxMovement, 0f, 0f);
                } else {
                    // move left
                    transform.Translate(-maxMovement, 0f, 0f);
                }
            } else {
                // player almost to mouse - don't move full distance
                transform.position = new Vector3( worldMousePos.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            // Keyboard control
            if (Input.GetKey(KeyCode.D)){
                // Move right
                transform.Translate(speed*Time.deltaTime, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.A)){
                // Move left
                transform.Translate(-speed*Time.deltaTime, 0f, 0f);
            }
        }     
    }

    void OnCollisionEnter2D(Collision2D col){
        if (gameState_REF == null){
            Debug.LogError("PlayerController never found a GameState and now requires it to handle collisions");
        }

        if (col.gameObject.GetComponent<OrbBehavior>()){
            gameState_REF.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (gameState_REF == null){
            Debug.LogError("PlayerController never found a GameState and now requires it to handle collisions");
        }

        if (col.gameObject.GetComponent<OrbBehavior>()){
            gameState_REF.GameOver();
        }
    }
}
