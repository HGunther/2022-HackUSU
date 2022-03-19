using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryWallBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        int id = other.gameObject.GetComponent<OrbBehavior>().OrbID;
        Debug.Log("ID: " + id);
    }
}
