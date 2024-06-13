using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineEvent : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;

    private bool isPlay = false;

    private void Start()
    {
        for (int i = 0; i < lights.Length; i++) 
            lights[i].GetComponent<SplashLight>().enabled = false;
        isPlay = false;
    }

    private void Update()
    {
        if(isPlay)
        {
            for (int i = 0; i < lights.Length; i++)
                lights[i].GetComponent<SplashLight>().enabled = true;
            isPlay = false;
        }
    }

    public void PlayOn()
    {
        isPlay = true;
    }

    public void LoadScene()
    {
        FadeManager.Instance.Alpah1();
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("01_MainScene");
    }
}
