using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLThrowingAgent : Agent
{
    GameState gameState;
    GameObject player;
    
    void Start()
    {
        gameState = (GameState)FindObjectOfType<GameState>();
        player = (GameObject)FindObjectOfType<PlayerRules>().gameObject;
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
        float speed = Mathf.Lerp(1.5f, 10f, actionBuffers.ContinuousActions[0]);
        float angle = Mathf.Lerp(Mathf.Deg2Rad*-105f, Mathf.Deg2Rad*-75f, actionBuffers.ContinuousActions[1]);
        float scale = Mathf.Lerp(0.5f, 1.5f, actionBuffers.ContinuousActions[2]);
        float startX = Mathf.Lerp(-5f, 5f, actionBuffers.ContinuousActions[3]);
       
        Vector2 vel = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;

        gameState.Launch(vel, startX, scale);
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[0] = Random.Range(0f,1f); //Speed
        continuousActionsOut[1] = Random.Range(0f,1f); //Angle
        continuousActionsOut[2] = Random.Range(0f,1f); //Scale
        continuousActionsOut[3] = Random.Range(0f,1f); //Startx
    }
}
