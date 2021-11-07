using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFish : Fish
{
	private int prevDx;

	public int level{get; private set;}
	public int maxCorrupt{get; private set;}
	public int exp{get; private set;}
	public int maxExp{get; private set;}

	private void playerControl()
	{
		//player moving

		Vector2 position = rigidbody2d.position;
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");

		bool isJumping=(position.y >= 0);

		if(isJumping) dir += new Vector2(0.0f, -0.075f);
		else dir = new Vector2(dx * 5f, dy * 3f);
		position += dir * Time.fixedDeltaTime;
		rigidbody2d.MovePosition(position);

		//turning
		isTurning=false;
		if(!isJumping)
		{
			if( sign(dx) * prevDx < 0 ) isTurning=true;
			if(dx> 0) prevDx=1;
			else if(dx < 0) prevDx=-1;
		}
    }
	private void levelUP()
	{
		level++;
		maxExp = ( level*level*level*5 + level*55 ) / 3;
		maxCorrupt = 100 + level * 40;
		size = 1 + (level - 1) * 0.3f;
	}
	private void addExp(int _xp)
	{
		exp += _xp;
		if(exp > maxExp)
		{
			exp -= maxExp;
			levelUP();
		}
		Debug.LogFormat("XP : {0}, HM : {1}, Level : {2}", exp, corrupt, level);
	}

	protected override void _start()
	{
		base._start();
		prevDx=1;
		exp=0; maxCorrupt = 100; maxExp=20; level=1;
	}
	protected override void _move()
	{
		playerControl();
		animControl();
	}
	protected override void _update()
	{
//		animControl();
	}
	public override void eat(int _xp, int heavyMetal)
	{
		base.eat(_xp, heavyMetal);
		addExp(_xp);
		Debug.LogFormat("XP : {0}, HM : {1}", exp, corrupt);
	}
}