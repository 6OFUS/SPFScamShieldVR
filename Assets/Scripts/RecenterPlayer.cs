/*
    Author: Kevin Heng
    Date: 12/08/2025
    Description: The RecenterPlayer class is used to ensure player spawns at correct position when scene loads
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class RecenterPlayer : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform xrOrigin;

    void Start()
    {
        // Teleport XR Origin to spawn
        Vector3 offset = xrOrigin.position - Camera.main.transform.position;
        offset.y = 0;
        xrOrigin.position = spawnPoint.position + offset;

        float yaw = spawnPoint.eulerAngles.y - Camera.main.transform.eulerAngles.y;
        xrOrigin.Rotate(Vector3.up, yaw);

        // Recenter via XR Input Subsystem
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances(subsystems);
        foreach (var subsystem in subsystems)
        {
            subsystem.TryRecenter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
