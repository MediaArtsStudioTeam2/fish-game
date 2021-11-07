using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherFish : Fish
{
	public static int count = 0;
	public override int eatLevel{get{return level * 2 + 1;}}
	public override bool isPlayer{get{return false;}}

	private void fishAI()
	{
		//wandering
		Vector2 position = rigidbody2d.position;
		position += dir * Time.fixedDeltaTime;
		rigidbody2d.MovePosition(position);
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
		size = 1 + (level - 0.5f) * 0.3f;
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
