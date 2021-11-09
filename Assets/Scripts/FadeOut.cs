using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
	//public string scene;
	public void OnButtonClicked()
    {
		StartFadeOut(2.0f, "Opening");
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
		SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		Color tempColor = sr.color;
		while (tempColor.a < 1f)
		{
			tempColor.a += Time.deltaTime / time;
			sr.color = tempColor;

			if (tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}
        sr.color = tempColor;
		SceneManager.LoadScene(scene);
	}
}
