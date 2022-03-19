using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLDodgingAgent : Agent
{
    public float speed = 10.0f;
    public Vector3 startPos;

    bool mouseControl = false;
    Vector3 mousePos;
    public float mouseDeadzone = 5.0f;

    GameState gameState_REF;

    void Start()
    {
        startPos = transform.position;
        mousePos = Input.mousePosition;

        gameState_REF = FindObjectOfType<GameState>();
        if (!gameState_REF){
            Debug.LogWarning("MLDodgingAgent could not find a GameState object");
        }
    }

    void Update(){
        if (!gameState_REF){
            gameState_REF = FindObjectOfType<GameState>();
            if (!gameState_REF){
                Debug.LogWarning("MLDodgingAgent could not find a GameState object");
            }
        }
    }

    public override void OnEpisodeBegin(){

    }

    public override void CollectObservations(VectorSensor sensor){

    }

    public override void OnActionReceived(ActionBuffers actionBuffers){
        var movement = speed * Time.deltaTime * actionBuffers.ContinuousActions[0];
        transform.Translate(movement, 0f, 0f);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;


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
                    continuousActionsOut[0] = 1;
                } else {
                    // move left
                    continuousActionsOut[0] = -1;
                }
            } else {
                // player almost to mouse - don't move full distance
                var distance = worldMousePos.x - transform.position.x;
                var neededRatio = maxMovement / distance;
                continuousActionsOut[0] = neededRatio;
            }
        }
        else
        {
            // Keyboard control
            if (Input.GetKey(KeyCode.D)){
                // Move right
                continuousActionsOut[0] = 1;
            }
            else if (Input.GetKey(KeyCode.A)){
                // Move left
                continuousActionsOut[0] = -1;
            } else {
                continuousActionsOut[0] = 0;
            }
        }     
    }


}
