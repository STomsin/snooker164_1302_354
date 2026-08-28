using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private float waitSeconds = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitSeconds > 0f)
            waitSeconds -= Time.deltaTime;
        else
            StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync("Scene01");

        while (!oper.isDone)
        {
            slider.value = oper.progress / 0.9f;
            yield return null;
        }
    }
}