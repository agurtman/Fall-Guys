using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject text;
    [SerializeField] Slider bar;

    List<string> dropText = new List<string>() { "Menu", "Level 1", "Level 2", "Level 3" };

    void Start()
    {
        dropdown.AddOptions(dropText);
    }

    public void StartGame()
    {
        ui.SetActive(true);
        StartCoroutine(waitScene(1));
    }

    public void LoadDropLevel(int index)
    {
        ui.SetActive(true);
        StartCoroutine(waitScene(index));
    }

    IEnumerator waitScene(int index)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(index);
        loadScene.allowSceneActivation = false;
        while (!loadScene.isDone)
        {
            bar.value = loadScene.progress;
            if (loadScene.progress >= .9f)
            {
                text.SetActive(true);
                if (Input.anyKeyDown)
                {
                    loadScene.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
