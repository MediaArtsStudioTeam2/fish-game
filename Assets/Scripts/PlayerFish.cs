using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFish : Fish
{
	private int prevDx; //for left-right flipping

	//score-variable
	public int maxCorrupt { get; private set; }
	public int exp { get; private set; }
	public int maxExp { get; private set; }
	public override int eatLevel { get { return level * 2; } }
	public override bool isPlayer { get { return true; } }


	public bool isOver;
	int maxLevel = 6;

	public GameObject otherFish;

	private void playerControl()
	{
		if (!isOver)
		{
			//keyboard control
			Vector2 position = rigidbody2d.position;
			float dx = Input.GetAxis("Horizontal");
			float dy = Input.GetAxis("Vertical");

			//if the fish goes out of the water, stop controlling and jump.
			bool isJumping = (position.y >= 0);
			if (isJumping) dir += new Vector2(0.0f, -0.075f);
			else dir = new Vector2(dx * speedMult, dy * 0.6f *speedMult);

			position += dir * Time.fixedDeltaTime;
			rigidbody2d.MovePosition(position);

			//turning
			//		isTurning=false;
			if (!isJumping)
			{
				if (MyUtils.sign(dx) * prevDx < 0) turn();//isTurning=true;
				if (dx > 0) prevDx = 1;
				else if (dx < 0) prevDx = -1;
				facingRight = (prevDx == 1);
			}
		}
	}
	private void levelUP()
	{
		//player level up
		level++;
		maxExp = (level * level * level * 5 + level * 55) / 3;
		maxCorrupt = 100 + level * 40;
		size = getSize();
		speedMult += 1;
		UILevelTxt.instance.SetValue(level);
		UIHMMaxBar.instance.SetValue(maxCorrupt / 300f);
		if (level >= maxLevel)
        {
			
			GameObject FadeOutManager = GameObject.Find("FadeOut_Clear");
			FadeOut FadeOutScript = FadeOutManager.GetComponent<FadeOut>();
			FadeOutScript.StartFadeOut(4.0f, "Ending_Clear");
		}
	}
	private void addExp(int _xp)
	{
		//when player eat feed, player's exp up
		exp += _xp;
		if (exp >= maxExp)
		{
			exp -= maxExp;
			levelUP();
		}
		UIExpBar.instance.SetValue(exp / (float)maxExp);
//		Debug.LogFormat("XP : {0}, HM : {1}, Level : {2}", exp, corrupt, level);
	}

	protected override void _start()
	{
		base._start();
		isOver = false;
		prevDx = 1;
		exp = 0; maxCorrupt = 100; maxExp = 20; level = 1;
	}
	protected override void _fixedUpdate()
	{
		playerControl();
		animControl();
	}
	public override void eat(int _xp, int heavyMetal)
	{
		SoundManagerScript.PlaySound("eatFoodSound");
		base.eat(_xp, heavyMetal);
		addExp(_xp);
		UIHMBar.instance.SetValue((corrupt / 300f) * 0.96f + 0.04f);
		if (corrupt >= maxCorrupt)
		{
			isOver = true;
			StartCoroutine(OverCorrupt());
		}
	}

	//for avoiding spawn feeds and enemies near the player
	public Vector2 getPosition()
	{
		Vector3 position = transform.position;
		return new Vector2(position.x, position.y);
	}

	IEnumerator OverCorrupt()
    {
		GameObject redRect = GameObject.Find("RedRect");
		SpriteRenderer sr = redRect.GetComponent<SpriteRenderer>();
		Color tempColor = sr.color;
		float time = 3.0f;
		while (tempColor.a < 0.5f)
		{
			tempColor.a += Time.deltaTime / time;
			sr.color = tempColor;

			if (tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}
		sr.color = tempColor;

		//spawn big fish
		GameObject spawner = GameObject.Find("spawnManager");
		GameObject newFish;
		OtherFish newFishHull;
		if (rigidbody2d.position.x + 10.0f > Consts.rightBorder) {
			newFish = Instantiate(otherFish, rigidbody2d.position + new Vector2(-7.0f, 0), Quaternion.identity);
			newFishHull = newFish.GetComponent<OtherFish>();
			newFishHull.facingRight = true;
		}
        else
        {
			newFish = Instantiate(otherFish, rigidbody2d.position + new Vector2(7.0f, 0), Quaternion.identity);
			newFishHull = newFish.GetComponent<OtherFish>();
			newFishHull.facingRight = false;
		}
		
		newFishHull.level = level + 2;
		//newFishHull.size = 1 + (newFishHull.level - 0.5f) * 0.3f;
	}
}