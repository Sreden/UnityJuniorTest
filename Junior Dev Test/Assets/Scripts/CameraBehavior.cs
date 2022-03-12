using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private float minFOV, maxFOV, multiplier;
    [SerializeField] private float maxX, zoomedMaxX;
    [SerializeField] private float maxY, zoomedMaxY;
    private float actualMaxX, actualMaxY;
    
    
    private InputHandler _inputHandler;
    private float _zoom = 0;
    private float _fov;
    
    void Start()
    {
        // Init full zoomed out
        _fov = maxFOV;
        // init inputHandler
        _inputHandler = GameObject.Find("Player Controller").GetComponent<InputHandler>();
    }

    void Update()
    {
        UpdateZoom();
        UpdateBoundaries();
        
        if (_inputHandler.isScrollActive)
        {
            transform.Translate(-_inputHandler.scroll * 0.015f);
        }
        if (transform.position.x > actualMaxX)
        {
            transform.position = new Vector3(actualMaxX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -actualMaxX)
        {
            transform.position = new Vector3(-actualMaxX, transform.position.y, transform.position.z);
        }
        if (transform.position.y > actualMaxY)
        {
            transform.position = new Vector3(transform.position.x, actualMaxY, transform.position.z);
        }
        if (transform.position.y < -actualMaxY)
        {
            transform.position = new Vector3(transform.position.x, -actualMaxY, transform.position.z);
        }
    }

    /// <summary>
    /// Handle zoom
    /// </summary>
    private void UpdateZoom()
    {
        // Refactor zoom to be -1 0 or 1
        _zoom = (_inputHandler.zoom > 0) ? 1 : (_inputHandler.zoom < 0) ? -1 : 0;
        // Add zoom * multiplier
        _fov += -_zoom * multiplier;
        
        // Check boundaries
        if (_fov > maxFOV) _fov = maxFOV;
        else if (_fov < minFOV) _fov = minFOV;
        
        // Update Camera
        GetComponent<Camera>().fieldOfView = _fov;
    }
    
    /// <summary>
    /// Update Boundaries with actual zooming level
    /// </summary>
    private void UpdateBoundaries()
    {
        var fovPercent = (_fov - minFOV) / (maxFOV - minFOV);
        
        actualMaxX = Mathf.Lerp(zoomedMaxX, maxX, fovPercent);
        actualMaxY = Mathf.Lerp(zoomedMaxY, maxY, fovPercent);
    }
}
