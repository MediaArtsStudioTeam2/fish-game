using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFish : Fish
{
	private int prevDx; //for left-right flipping

	//score-variable
	public int maxCorrupt{get; private set;}
	public int exp{get; private set;}
	public int maxExp{get; private set;}
	public override int eatLevel{get{return level * 2;}}
	public override bool isPlayer{get{return true;}}

	private void playerControl()
	{
		//keyboard control
		Vector2 position = rigidbody2d.position;
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");

		//if the fish goes out of the water, stop controlling and jump.
		bool isJumping=(position.y >= 0);
		if(isJumping) dir += new Vector2(0.0f, -0.075f);
		else dir = new Vector2(dx * 5f, dy * 3f);

		position += dir * Time.fixedDeltaTime;
		rigidbody2d.MovePosition(position);

		//turning
		isTurning=false;
		if(!isJumping)
		{
			if( MyUtils.sign(dx) * prevDx < 0 ) isTurning=true;
			if(dx> 0) prevDx=1;
			else if(dx < 0) prevDx=-1;
			facingRight = (prevDx == 1);
		}
    }
	private void levelUP()
	{
		//player level up
		level++;
		maxExp = ( level*level*level*5 + level*55 ) / 3;
		maxCorrupt = 100 + level * 40;
		size = 1 + (level - 1) * 0.3f;
	}
	private void addExp(int _xp)
	{
		//when player eat feed, player's exp up
		exp += _xp;
		if(exp > maxExp)
		{
			exp -= maxExp;
			levelUP();
		}
//		Debug.LogFormat("XP : {0}, HM : {1}, Level : {2}", exp, corrupt, level);
	}

	protected override void _start()
	{
		base._start();
		prevDx=1;
		exp=0; maxCorrupt = 100; maxExp=20; level=1;
	}
	protected override void _fixedUpdate()
	{
		playerControl();
		animControl();
	}
	public override void eat(int _xp, int heavyMetal)
	{
		base.eat(_xp, heavyMetal);
		addExp(_xp);
	}

	//for avoiding spawn feeds and enemies near the player
	public Vector2 getPosition()
	{
		Vector3 position = transform.position;
		return new Vector2(position.x, position.y);
	}
}