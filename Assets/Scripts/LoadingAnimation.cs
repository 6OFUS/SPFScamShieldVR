/*
    Author: Kevin Heng
    Date: 26/06/2025
    Description: The LoadingAnimation class is used to handle the animation for a spinning loading UI
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}
