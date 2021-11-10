using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherFish : Fish
{
	public static int count = 0;
	public override int eatLevel{get{return level * 2 + 1;}}
	public override bool isPlayer{get{return false;}}

	private bool isTurning;
	private bool isChasing;
	private bool isEndChasing;

	private GameObject targetFeed;
	private	Rigidbody2D targetFeed_r;

	private IEnumerator detectWall;
	private IEnumerator detectFeeds;

	private void endChase()
	{
		isChasing=false;
		isEndChasing=true;
		StopCoroutine(detectFeeds);
		detectFeeds = DetectFeeds();
		StartCoroutine(detectFeeds);
	}
	private void fishChaseAI(Vector2 myPos)
	{
		if(targetFeed == null || targetFeed_r == null)
		{
			endChase();
			return;
		}
		Vector2 targetPos = targetFeed_r.position;
		Vector2 targetDir = targetPos - myPos;
		targetDir = targetDir.normalized;
		if(!inAoV(targetDir))
		{
			endChase();
			return;
		}
		float yAcc = targetDir.y / Mathf.Abs(targetDir.x) * speedMult;
		dir.y = dir.y * 0.8f + yAcc * 0.2f;
	}
	private void fishAI()
	{
		//wandering
		Vector2 position = rigidbody2d.position;

		//turning
		if(isTurning)
		{
			dir.x += MyUtils.boolToSign(facingRight) * 0.5f;
			if(Mathf.Abs(dir.x) > speedMult)
			{
				dir.x = Mathf.Sign(dir.x) * speedMult;
				isTurning=false;
			}
		}

		//chasing
		if(isChasing) fishChaseAI(position);

		//reset y-dir
		if(isEndChasing)
		{
			dir.y *= 0.95f;
			if(Mathf.Abs(dir.y) < 0.1f)
			{
				dir.y = 0;
				isEndChasing=false;
			}
		}

		position += Vector2.ClampMagnitude(dir, speedMult) * Time.fixedDeltaTime;
		rigidbody2d.MovePosition(position);

		if(position.x < Consts.leftBorder || position.x > Consts.rightBorder) Destroy(gameObject);
    }
    private bool OutOfBorder(string tag)
    {
    	if(tag != "Border") return false;
    	int minCorrupt = level * 10;
    	int maxCorrupt = 10 + level * 20;
    	Debug.LogFormat("Corrupt : {0}, min : {1}, max : {2}",corrupt, minCorrupt, maxCorrupt);
    	float chanceBase = (corrupt - minCorrupt) / (float)(maxCorrupt - minCorrupt);
    	float chance = chanceBase < 0.05f ? 0.05f : Mathf.Sqrt(chanceBase);
    	Debug.LogFormat("OoB Chance : {0}",chance);
    	return Random.value < chance;
    }
    private IEnumerator DetectWall() //detect obstacles for each 0.5 secs
    {
    	while(true)
    	{
			Vector2 position = rigidbody2d.position;
			Vector2 lookDirection = dir.normalized;
			RaycastHit2D hit = Physics2D.Raycast(position, lookDirection, Consts.detectWallRange, Consts.ObstacleLayer);
			if(hit.collider != null)
			{
				if(OutOfBorder(hit.collider.tag)) yield break;
				turn();
				isTurning=true;
				facingRight=(MyUtils.sign(dir.x) != 1);
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
	private bool inAoV(Vector2 targetDir)
	{
		Vector2 front = new Vector2(Mathf.Sign(dir.x), 0);
		return Vector2.Dot (front, targetDir.normalized ) > Mathf.Cos(45f * Mathf.Deg2Rad);
	}
	private bool findFeed()
	{
		Vector2 position = rigidbody2d.position;
		Collider2D[] results = Physics2D.OverlapCircleAll(rigidbody2d.position, Consts.chaseRange, Consts.FishLayer);
//		Debug.Log(results[0].tag);

		float minDist=10000000f;
		Collider2D target=null;
		foreach (Collider2D result in results)
		{
			bool isValidFeed=false;
			if(result.tag == "FishObject")
			{
				Fish obj = result.GetComponent<Fish>();
				if(obj == this) continue; //if result is self, skip
				if(eatLevel <= obj.eatLevel) continue;
				isValidFeed = true;
			}
			else if(result.tag == "Feed")
			{
				isValidFeed = true;
			}
			if(isValidFeed)
			{
				GameObject res = result.gameObject;
				Vector2 res_pos = res.GetComponent<Rigidbody2D>().position;
				float dist = Vector2.Distance(res_pos, position);
				if( !inAoV(res_pos - position) ) continue;
				if(minDist > dist)
				{
					minDist = dist;
					target = result;
				}
			}
		}
		if(target != null)
		{
			targetFeed = target.gameObject;
			targetFeed_r = target.GetComponent<Rigidbody2D>();
			return true;
		}
		else
		{
			targetFeed = null;
			targetFeed_r = null;
			return false;
		}
	}
	private IEnumerator DetectFeeds()
	{
    	while(true)
    	{
    		yield return new WaitForSeconds(2f);
    		bool isFeedFound = findFeed();
    		if(isFeedFound)
    		{
    			isChasing=true;
    			yield break;
    		}
		}
	}
    private Color getFishColor()
    {
    	const float m=Consts.overlayHeavyMetal;
    	float bright = 1.0f;
    	float deep = 1.0f;
    	if(corrupt < (int)m)
    	{
    		bright = (m-corrupt) / m;
    		deep = 1.0f;
    	}
    	else
    	{
    		bright = 0f;
    		deep = 1.0f - 0.1f * Mathf.Log((float)corrupt/m, 2f);
    	}
    	return new Color(deep, bright, bright, 1.0f);
    }

    public void Awake()
	{
		count++;
	}
    protected override void _start()
	{
		base._start();
		facingRight=MyUtils.randomBool();
		speedMult = 3f;
		dir = new Vector2(speedMult, 0f);
		if(!facingRight)
		{
			dir.x *= -1;
			sprite.flipX=true;
		}
		isTurning=false;
		isChasing=false;
		isEndChasing=false;

		targetFeed = null;
		targetFeed_r = null;

		detectWall = DetectWall();
		detectFeeds = DetectFeeds();
		StartCoroutine(detectWall);
		StartCoroutine(detectFeeds);
	}
	protected override void _fixedUpdate()
	{
		fishAI();
		animControl();
	}
	public override void eat(int _xp, int heavyMetal)
	{
		base.eat(_xp, heavyMetal);
		sprite.color = getFishColor();
	}
	
	public void OnDestroy()
	{
		count--;
	}
}
