using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLDodgingAgent : Agent
{
    GameState gameState;
    GameObject player;
    public float speed = 10.0f;
    public Vector3 startPos;

    bool mouseControl = false;
    Vector3 mousePos;
    public float mouseDeadzone = 5.0f;

    public float totalMovementDuringGame;
    public float lastFrameXPos;

    void Start()
    {
        startPos = transform.position;
        mousePos = Input.mousePosition;

        gameState = FindObjectOfType<GameState>();
        if (!gameState){
            Debug.LogWarning("MLDodgingAgent could not find a GameState object");
        }
        startPos = transform.position;

        gameState = (GameState)FindObjectOfType<GameState>();
        player = (GameObject)FindObjectOfType<PlayerRules>().gameObject;

        totalMovementDuringGame = 0f;
        lastFrameXPos = startPos.x;
    }

    void Update(){
        if (!gameState){
            gameState = FindObjectOfType<GameState>();
            if (!gameState){
                Debug.LogWarning("MLDodgingAgent could not find a GameState object");
            }
        }
    }

    public override void OnEpisodeBegin(){
        gameState.ResetGame();
    }

    public void ResetPlayer(){
        transform.position = startPos;
        totalMovementDuringGame = 0f;
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(player.transform.position);

        List<GameObject> OrbList = gameState.GetOrbList();
        foreach(GameObject orb in OrbList){
            sensor.AddObservation(orb.GetComponent<OrbBehavior>().Velocity);
            sensor.AddObservation(orb.transform.position);
            sensor.AddObservation(orb.GetComponent<OrbBehavior>().Scale);
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers){
        var movement = speed * Time.deltaTime * actionBuffers.ContinuousActions[0];
        transform.Translate(movement, 0f, 0f);


        totalMovementDuringGame += Mathf.Abs(lastFrameXPos - transform.position.x);
        lastFrameXPos = transform.position.x;

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
