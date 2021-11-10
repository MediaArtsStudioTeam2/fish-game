using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
//	protected bool isTurning;
	public bool facingRight;
	protected Vector2 dir;

	private float _size;
	protected float speedMult;

	private int _corrupt;
	public int corrupt{
		get{return _corrupt;} 
		set{
			if(value > 0) _corrupt = value;
			else _corrupt = 0;
		}
	}

	private int _level=1;
	public int level{
		get{return _level;} 
		set{
			if(value > 0) _level = value;
			else _level = 1;
		}
	}
	public virtual int eatLevel{get{return 2;}}
	public virtual bool isPlayer{get{return false;}}

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

	protected float getSize()
	{
		return 0.7f + (eatLevel - 1) * 0.15f;
	}
	protected virtual void _start()
	{
		size = getSize();
		corrupt = 0;
		speedMult = isPlayer ? 4f : 3f;
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
/*		if(isTurning)
		{
			anim.SetTrigger("Turning");
			sprite.flipX=!facingRight;
		}*/
	}
	protected void turn()
	{
		//turning
		anim.SetTrigger("Turning");
		sprite.flipX=facingRight;
	}

}
