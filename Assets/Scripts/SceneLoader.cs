using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour {
    
    public static SceneLoader instance;
    

    [SerializeField] private CanvasGroup sceneLoaderCanvasGroup;
    [SerializeField] private Image progressBar;

    private string loadSceneName;
    private AsyncOperation asyncOp;


    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName) {
        this.loadSceneName = sceneName;
        this.asyncOp = SceneManager.LoadSceneAsync(this.loadSceneName);
        SceneManager.sceneLoaded += LoadSceneEnd;

        StartCoroutine(LoadingProgress());
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.name == this.loadSceneName) {
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    IEnumerator LoadingProgress() {
        if (this.asyncOp.progress < 0.9f) {
            this.asyncOp.allowSceneActivation = false;  // HOLD 
            yield return StartCoroutine(Fade(true));
        }

        if (this.asyncOp.progress == 0.9f) {
            //yield return StartCoroutine(Fade(false));
            this.asyncOp.allowSceneActivation = true;
        }
    }

    IEnumerator Fade(bool isFadeIn) {
        float timer = 0;

        while (timer <= 1) {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            this.sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

    }
    

}