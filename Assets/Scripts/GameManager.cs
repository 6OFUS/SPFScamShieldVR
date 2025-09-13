/*
    Author: Kevin Heng
    Date: 06/08/2025
    Description: The GameManager class is used to store variables throughout the game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// One instance of GameManager
    /// </summary>
    public static GameManager Instance;
    /// <summary>
    /// Store previous scene index number
    /// </summary>
    public int prevSceneIndexNum = -1;
    /// <summary>
    /// Store previous scenario in that scene number
    /// </summary>
    public int prevScenario = -1;
    /// <summary>
    /// Boolean if player restarts scenario
    /// </summary>
    public bool isRestart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
