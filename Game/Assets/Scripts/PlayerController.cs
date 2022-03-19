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
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        mousePos = Input.mousePosition;

        var cameraHeight = Camera.main.orthographicSize * 2;
        var cameraWidth = Camera.main.aspect * cameraHeight;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput(){
        // Detect input scheme
        if (mouseControl){
            // Detect change from mouse to keyboard
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)){
                mouseControl = false;
            }
        } else {
            // Detect change from keyboard to mouse
            var mouseDif = Input.mousePosition - mousePos;
            if (mouseDif.magnitude > mouseDeadzone){
                mouseControl = true;
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
                    transform.position += new Vector3( maxMovement, 0, 0);
                } else {
                    // move left
                    transform.position -= new Vector3( maxMovement, 0, 0);
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
                transform.position += new Vector3( speed, 0, 0 ) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A)){
                // Move left
                transform.position -= new Vector3( speed, 0, 0 ) * Time.deltaTime;
            }
        }
    }

}
