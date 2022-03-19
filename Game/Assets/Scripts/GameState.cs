using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public Vector2 screenBounds;

    void Start()
    {
        var cameraHeight = Camera.main.orthographicSize * 2;
        var cameraWidth = Camera.main.aspect * cameraHeight;
        screenBounds = new Vector2(cameraWidth, cameraHeight);        
    }

    void Update()
    {
        
    }
}
