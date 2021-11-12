using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject algae;
	[SerializeField] private GameObject fish;

	private int maxAlgaeCount=150;
	private int maxFishCount=20;

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
	private int getRandomFishLevel(int playerLevel)
	{
		if(playerLevel == 1) return 1;
		if(Random.value < 0.2f) return playerLevel;
		return Random.Range(1, getPlayerLevel());
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
				int fishLevel=getRandomFishLevel(getPlayerLevel());
				newFishHull.level = fishLevel;
				newFishHull.corrupt = Random.Range(0, 5*(fishLevel-1)+1) * 5;
			}
		}
	}

	private IEnumerator SpawnAlgae()
	{
		yield return new WaitForSeconds(3f);
    	while(true)
    	{
    		_SpawnAlgae();
    		yield return new WaitForSeconds(0.6f);
		}
	}
	private IEnumerator SpawnFish()
	{
		yield return new WaitForSeconds(5f);
    	while(true)
    	{
    		_SpawnFish();
    		yield return new WaitForSeconds(getPlayerLevel() == 1 ? 2f : 1f);
		}
	}
	public void Start () {
		for(int i=0;i<30;i++)
		{
			Instantiate(algae, getRandomPosition(2,2f), Quaternion.identity);
		}
		spawnAlgae=SpawnAlgae();
		spawnFish=SpawnFish();
		StartCoroutine(spawnAlgae);
		StartCoroutine(spawnFish);
	}
}
