using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
	protected bool isTurning;
	protected bool facingRight;
	protected Vector2 dir;

	private float _size;
	protected float speedMult;
	public int corrupt{get; private set;}

	private int _level;
	public int level{
		get{return _level;} 
		set{
			if(value > 0) _level = value;
			else _level = 1;
		}
	}
	public virtual int eatLevel{get;}
	public virtual bool isPlayer{get;}

	protected SpriteRenderer sprite;
	protected Animator anim;
	protected Rigidbody2D rigidbody2d;

	public float size
	{
		get{return _size;}
		protected set{
			_size = value;

			Vector2 scale= transform.localScale;
			scale.x = Mathf.Sign(scale.x) * _size;
			scale.y = _size;
			transform.localScale = scale;
		}
	}

	protected virtual void _start()
	{
		size = 1;
		corrupt = 0;
		speedMult = 5f;
		isTurning=false;
		facingRight=true;
		anim = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}
	public void Start()
	{
		_start();
	}
	protected virtual void _fixedUpdate()
	{
	}
	public void FixedUpdate()
	{
		_fixedUpdate();
	}
	protected virtual void _update()
	{
	}
	public void Update()
	{
		_update();
	}

	public virtual void eat(int _xp, int heavyMetal)
	{
		anim.SetTrigger("Eating");
		corrupt += heavyMetal;
	}

	protected void animControl()
	{
		//moving
		float horizMove = Mathf.Abs(dir.x);
		anim.SetFloat("move", horizMove);

		//rotating
		float rotateDeg = Mathf.Atan2(dir.y * dir.x / speedMult, speedMult) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(Vector3.forward * rotateDeg);

		//turning
		if(isTurning)
		{
			anim.SetTrigger("Turning");
			sprite.flipX=!facingRight;
		}
	}

}
