using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
	protected bool isTurning;
	protected Vector2 dir;

	private float _size;
	public int corrupt{get; private set;}

	protected Animator anim;
	protected Rigidbody2D rigidbody2d;

	public static int count = 0;

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

	protected int sign(float a)
	{
		if(a > 0f) return 1;
		else if(a < 0f) return -1;
		else return 0;
	}

	protected virtual void _start()
	{
		size = 1;
		isTurning=false;
		anim = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	public void Start()
	{
		_start();
	}
	protected virtual void _move()
	{
	}
	public void FixedUpdate()
	{
		_move();
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
		float rotateDeg = Mathf.Atan2(dir.y * dir.x / 5f, 5f) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(Vector3.forward * rotateDeg);

		//turning
		if(isTurning)
		{
			anim.SetTrigger("Turning");
		}
	}

	public void turn()
	{
		Vector2 scale= transform.localScale;
		scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);
		transform.localScale = scale;
	}
}
