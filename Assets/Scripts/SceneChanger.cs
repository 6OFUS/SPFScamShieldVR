using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// Reference Fade script
    /// </summary>
    [SerializeField] private Fade fade;
    /// <summary>
    /// Target scene index number
    /// </summary>
    public int sceneIndexNum;

    /// <summary>
    /// Function to change scene
    /// </summary>
    public void ChangeScene()
    {
        Debug.Log("Changing scene...");
        StartCoroutine(FadeThenChange(sceneIndexNum));
    }

    /// <summary>
    /// Function to fade out then change scene
    /// </summary>
    /// <param name="index">Target scene index</param>
    /// <returns></returns>
    private IEnumerator FadeThenChange(int index)
    {
        yield return fade.Transition(1f); // Wait for fade to complete
        SceneManager.LoadScene(index); // Load scene AFTER fade
    }

    /// <summary>
    /// Scene is picked at random
    /// </summary>
    public void RandomScene()
    {
        int randomIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings);

        while (randomIndex == GameManager.Instance.prevSceneIndexNum)
        {
            randomIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        }

        GameManager.Instance.prevSceneIndexNum = randomIndex;
        StartCoroutine(FadeThenChange(randomIndex));
    }

    /// <summary>
    /// Restart current scene
    /// </summary>
    public void RestartScene()
    {
        GameManager.Instance.isRestart = true;
        StartCoroutine(FadeThenChange(sceneIndexNum));

    }
}
