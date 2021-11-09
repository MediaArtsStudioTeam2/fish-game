using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
	public GameObject player;
	public GameObject algae;
	public GameObject fish;

	private int maxAlgaeCount=120;
	private int maxFishCount=15;

	private IEnumerator spawnAlgae;
	private IEnumerator spawnFish;

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

	private Vector3 getRandomPosition(int layer, float distFromPlayer=2f)
	{
		Vector2 playerPos = getPlayerPosition();
		while(true)
		{
			float x=Random.Range(Consts.leftBorder, Consts.rightBorder);
			float y=Random.Range(Consts.downBorder + 2f, Consts.upBorder - 2f);

			//for avoiding spawn feeds and enemies near the player
			Vector2 xy=new Vector2(x,y);
			if(Vector2.Distance(playerPos, xy) > distFromPlayer) return new Vector3(x, y, (float)layer);
		}
	}
	private void _SpawnAlgae()
	{
		for(int i=0;i<5;i++)
		{
			if(Feed.count < maxAlgaeCount)
			{
				Instantiate(algae, getRandomPosition(2, 2f), Quaternion.identity);
			}
		}
	}
	private void _SpawnFish()
	{
		GameObject newFish;
		if(OtherFish.count < maxFishCount)
		{
			newFish = Instantiate(fish, getRandomPosition(1, 10f), Quaternion.identity);
			OtherFish newFishHull = newFish.GetComponent<OtherFish>();
			if(newFishHull != null)
			{
				int fishLevel=Random.Range(1, getPlayerLevel()+1);
				newFishHull.level = fishLevel;
				newFishHull.corrupt = Random.Range(0, 5*(fishLevel-1)+1) * 4;
			}
		}
	}

	private IEnumerator SpawnAlgae()
	{
		yield return new WaitForSeconds(3f);
    	while(true)
    	{
    		_SpawnAlgae();
    		yield return new WaitForSeconds(1f);
		}
	}
	private IEnumerator SpawnFish()
	{
		yield return new WaitForSeconds(5f);
    	while(true)
    	{
    		_SpawnFish();
    		yield return new WaitForSeconds(2f);
		}
	}
	public void Start () {
		for(int i=0;i<20;i++)
		{
			Instantiate(algae, getRandomPosition(2,2f), Quaternion.identity);
		}
		spawnAlgae=SpawnAlgae();
		spawnFish=SpawnFish();
		StartCoroutine(spawnAlgae);
		StartCoroutine(spawnFish);
	}
}
