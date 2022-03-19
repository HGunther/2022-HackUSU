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
    
    void Start()
    {
        gameState = (GameState)FindObjectOfType<GameState>();
        player = (GameObject)FindObjectOfType<PlayerRules>().gameObject;
    }

    public override void OnEpisodeBegin(){

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

    }

}
