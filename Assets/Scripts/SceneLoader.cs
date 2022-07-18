using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour {
    public static SceneLoader instance;

    
    [SerializeField] private CanvasGroup sceneLoaderCanvasGroup;
    private string loadSceneName;


    private void Init() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Awake() {
        Init();
    }
    
    public void LoadScene(string sceneName) {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        this.loadSceneName = sceneName;
        StartCoroutine(LoadingProgress(this.loadSceneName));
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode) {
        if (scene.name == this.loadSceneName) {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    IEnumerator LoadingProgress(string sceneName) {
        yield return StartCoroutine(Fade(true));
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;

        while(!asyncOp.isDone) {
            yield return null;

            if (asyncOp.progress >= 0.9f) {        
                yield return new WaitForSeconds(0.5f);
                asyncOp.allowSceneActivation = true;
                yield break;
            }
        }
    }

    IEnumerator Fade(bool isFadeIn) {
        float timer = 0;

        while (timer <= 1) {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            this.sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if (!isFadeIn) {
            gameObject.SetActive(false);
        }
    }
}