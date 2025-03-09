using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [Header("Level Type")]
    [SerializeField] private LevelType levelType;

    [Header("Transition Animation")]
    [SerializeField] private GameObject transitionCanvas;
    [SerializeField] private Animator transitionAnimator;

    [Header("Player's Position Reference")]
    [SerializeField] private Transform playerPos;
    public void ChangeScene(string targetSceneName)
    {
        if(levelType == LevelType.IsoLevel)
        {
            SceneHandler.playerPosition = playerPos.position;
        }
        SceneHandler.destinationSceneName = targetSceneName;
        StartCoroutine(StartLoadingScreen());
    }
    private void Awake()
    {
        SceneHandler.currentSceneName = SceneManager.GetActiveScene().name;
    }
    private void Start()
    {
        if (levelType == LevelType.Loading)
        {
            StartCoroutine(StartChangeScene());
        }
        else if(levelType == LevelType.IsoLevel)
        {
            SceneHandler.playerPosition = playerPos.position;
            StartCoroutine(TransitionOut());
        }
        else
        {
            StartCoroutine(TransitionOut());
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && levelType == LevelType.BossLevel)
        {
            SceneHandler.destinationSceneName = "Level1_ISO";
            StartCoroutine(StartLoadingScreen());
        }
    }
    private IEnumerator StartLoadingScreen()
    {
        transitionCanvas.SetActive(true);
        transitionAnimator.SetInteger("Transition", 0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LoadingScreen");
    }
    private IEnumerator StartChangeScene()
    {
        AsyncOperation destinationScene = SceneManager.LoadSceneAsync(SceneHandler.destinationSceneName);
        destinationScene.allowSceneActivation = false;
        while(destinationScene.progress < 0.9f)
        {
            yield return null;
        }
        transitionCanvas.SetActive(true);
        transitionAnimator.SetInteger("Transition", 0);
        yield return new WaitForSeconds(1f);
        destinationScene.allowSceneActivation = true;
    }
    private IEnumerator TransitionOut()
    {
        transitionAnimator.SetInteger("Transition", 1);
        yield return new WaitForSeconds(1f);
        transitionCanvas.SetActive(false);
    }
}
