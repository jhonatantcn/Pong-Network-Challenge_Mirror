using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // loadingScreen is a prefab to load next scene
    public GameObject loadingScreen;

    // Start is called before the first frame update
    public void LoadScene(string sceneName)
    {
        Instantiate(loadingScreen);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Start is called before the first frame update
    private IEnumerator LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);

                loadingScreen.GetComponentInChildren<Slider>().value = progress; //Progresso da barra de carregamento
                yield return null;
            }
        }
}
