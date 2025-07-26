using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Fade fade;
    public int sceneIndexNum;

    public void ChangeScene()
    {
        Debug.Log("Changing scene...");
        StartCoroutine(FadeThenChange());
    }

    private IEnumerator FadeThenChange()
    {
        yield return fade.Transition(1f); // Wait for fade to complete
        SceneManager.LoadScene(sceneIndexNum); // Load scene AFTER fade
    }

    public void RandomScene()
    {
        int randomIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings);

        while (randomIndex == GameManager.Instance.prevSceneIndexNum)
        {
            randomIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings);
        }

        GameManager.Instance.prevSceneIndexNum = randomIndex;
        StartCoroutine(FadeThenChange());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
