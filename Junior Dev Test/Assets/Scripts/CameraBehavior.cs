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

    private Game _game;
    private InputHandler _inputHandler;
    private float _zoom = 0;
    private float _fov;

    private void Start()
    {
        // Init full zoomed out
        _fov = maxFOV;
        // init inputHandler
        _inputHandler = GameObject.Find("Player Controller").GetComponent<InputHandler>();
        _game = GameObject.Find("Game").GetComponent<Game>();
    }

    void Update()
    {
        UpdateZoom();
        UpdateBoundaries();
        
        if (_inputHandler.isScrollActive && !_inputHandler.isBuilding)
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
        
        // If we want to make preview of tower + tower range we can check mouse position on Update
        //CheckMouse();
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

    private Vector3 CheckMouse()
    {
        var worldPosition = new Vector3();
        if (!_inputHandler.isBuilding || Camera.main == null) return worldPosition;
        
        var mousePosition = _inputHandler.aim;
        
        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z));
        return worldPosition;
    }

    public void Build()
    {
        if (!_inputHandler.isBuilding) return;
        
        var selectedTower = _inputHandler.selectedTower;
        if (_game.Buy(selectedTower.GetComponent<Tower>().stats.cost))
        {
            Instantiate(_inputHandler.selectedTower, CheckMouse(), new Quaternion());
        }
    }
}
