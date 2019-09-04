using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private Image ProgressBar;
    [SerializeField]
    private Text ProgressInDigit;
    // Start is called before the first frame update
    void Start()
    {
        ProgressBar.fillAmount = 0;
        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene()
    {
        AsyncOperation LoadLevel = SceneManager.LoadSceneAsync("Game");
        while(!LoadLevel.isDone)
        {
            float Progress = Mathf.Clamp01(LoadLevel.progress / .9f);
            ProgressBar.fillAmount = Progress;
            ProgressInDigit.text = Progress * 100f + "%";
            Debug.Log(Progress);
            yield return null;
        }  
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
