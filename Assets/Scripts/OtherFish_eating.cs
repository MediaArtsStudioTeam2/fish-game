using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherFish_eating : MonoBehaviour
{
	GameObject parentHull;
	OtherFish parent;

	private void setParent()
	{
		if(parent == null)
		{
			parentHull = transform.parent.gameObject;
			if(parentHull != null)
			{
				parent = parentHull.GetComponent<OtherFish>();
			}
		}
		
	}
	public void OnTriggerEnter2D(Collider2D other)
	{
		Fish predator = other.GetComponent<Fish>();
		setParent();

		if(predator != null)
		{
			if(predator.eatLevel > parent.eatLevel)
			{
				predator.eat(parent.level * 2, parent.corrupt) ;
				Destroy(parentHull);
			}
			else if(predator.isPlayer)
			{
				parent.eat(0, predator.corrupt) ;
				Destroy(other.gameObject);
				Debug.Log("Player was Eaten");

				GameObject FadeOutManager = GameObject.Find("FadeOut_Gameover");
				FadeOut FadeOutScript = FadeOutManager.GetComponent<FadeOut>();
				FadeOutScript.StartFadeOut(4.0f, "Ending_GameOver");
			}
		}
	}
}
