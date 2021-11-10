using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoCheck : MonoBehaviour
{
	public VideoPlayer vid;
	public SpriteRenderer sp;
	public string scene;
	// Start is called before the first frame update
	void Start()
	{
		sp = this.GetComponent<SpriteRenderer>();
		vid = this.GetComponent<VideoPlayer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (vid.frame >= 5 && vid.frame <= 10)
		{
			sp.color = new Color(1, 1, 1);
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			StartFadeOut(0.5f);
		}
		if (vid.frame >= (long)vid.frameCount - 5)
		{
			SceneManager.LoadScene(scene);
		}
	}
	private void StartFadeOut(float time)
    {
        StartCoroutine(Fadeout_(time));
    }

	private IEnumerator Fadeout_(float time)
	{
		float col = sp.color.r;
		while (col > 0f)
		{
			col -= Time.deltaTime / time;
			if(col <= 0f) col = 0f;
			sp.color = new Color(col, col, col);
			yield return null;
			
		}
		SceneManager.LoadScene(scene);
	}
}
