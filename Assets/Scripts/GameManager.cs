using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator FadeAnimator;

    void Start()
    {
        DontDestroyOnLoad(FadeAnimator.gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        FadeAnimator.SetBool("FadeIn", true);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        FadeAnimator.SetBool("FadeOut", true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
