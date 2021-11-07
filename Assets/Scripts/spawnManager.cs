using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
	public GameObject algae;
	public GameObject fish;

	private int maxAlgaeCount=120;
//	private int maxFishCount=20;

	private Vector3 getRandomPosition()
	{
		float x=Random.Range(Consts.leftBorder, Consts.rightBorder);
		float y=Random.Range(Consts.downBorder, Consts.upBorder);
		return new Vector3(x,y,0f);
	}
	private void SpawnAlgae()
	{
		Debug.Log(Feed.count);
		for(int i=0;i<3;i++)
		{
			if(Feed.count < maxAlgaeCount)
			{
				Instantiate(algae, getRandomPosition(), Quaternion.identity);
			}
		}
	}
	public void Start () {
		for(int i=0;i<10;i++)
		{
			Instantiate(algae, getRandomPosition(), Quaternion.identity);
		}
		InvokeRepeating("SpawnAlgae", 3, 1);
//		InvokeRepeating("SpawnFish", 10, 3);
	}
}
