using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFadeout : MonoBehaviour
{
    public Image img;
    //public string scene;
    public void OnButtonClicked()
    {
        StartFadeOut(2.0f, "Opening");
    }
    public void OnButtonClickedGameover()
    {
        StartFadeOut(1.0f, "FishGameMain");
    }
    public void OnButtonClickedClear()
    {
        StartFadeOut(2.0f, "StartScreen");
    }


    public void StartFadeOut(float time, string scene)
    {
        StartCoroutine(FadeOut_(time, scene));
    }

    IEnumerator FadeOut_(float time, string scene)
    {
        Color tempColor = img.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / time;
            img.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }
        img.color = tempColor;
        SceneManager.LoadScene(scene);
    }
}
