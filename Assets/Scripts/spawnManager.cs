using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
	public GameObject player;
	public GameObject algae;
	public GameObject fish;

	private int maxAlgaeCount=120;
	private int maxFishCount=20;

	private Vector2 getPlayerPosition()
	{
		if(player.gameObject != null)
		{
			PlayerFish comp = player.GetComponent<PlayerFish>();
			return comp.getPosition();
		}
		else return new Vector2();
	}
	private int getPlayerLevel()
	{
		if(player.gameObject != null)
		{
			PlayerFish comp = player.GetComponent<PlayerFish>();
			return comp.level;
		}
		else return 1;
	}

	private Vector3 getRandomPosition(int layer)
	{
		Vector2 playerPos = getPlayerPosition();
		while(true)
		{
			float x=Random.Range(Consts.leftBorder, Consts.rightBorder);
			float y=Random.Range(Consts.downBorder, Consts.upBorder);

			//for avoiding spawn feeds and enemies near the player
			Vector2 xy=new Vector2(x,y);
			if(Vector2.Distance(playerPos, xy) > Consts.distFromPlayer) return new Vector3(x, y, (float)layer);
		}
	}
	private void SpawnAlgae()
	{
		for(int i=0;i<2;i++)
		{
			if(Feed.count < maxAlgaeCount)
			{
				Instantiate(algae, getRandomPosition(2), Quaternion.identity);
			}
		}
	}
	private void SpawnFish()
	{
		GameObject newFish;
		if(OtherFish.count < maxFishCount)
		{
			newFish = Instantiate(fish, getRandomPosition(1), Quaternion.identity);
			OtherFish newFishHull = newFish.GetComponent<OtherFish>();
			if(newFishHull != null)
			{
//				newFishHull.level = 1;
				newFishHull.level = Random.Range(1, getPlayerLevel()+1);
			}
		}
	}
	public void Start () {
		for(int i=0;i<10;i++)
		{
			Instantiate(algae, getRandomPosition(2), Quaternion.identity);
		}
		InvokeRepeating("SpawnAlgae", 3, 1);
		InvokeRepeating("SpawnFish", 5, 2);
	}
}
