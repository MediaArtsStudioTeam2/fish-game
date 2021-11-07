using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour
{
	// Update is called once per frame
	private Vector2 dir;
	protected Rigidbody2D rigidbody2d;

	public bool isHeavyMetal;
	private SpriteRenderer feedSprite;

	public static int count = 0;

	private bool randomBool()
	{
		return Random.value > 0.5;
	}

	public void Awake()
	{
		count++;
	}
	public void Start()
	{
		dir = new Vector2(randomBool() ? 1f : -1f, Random.Range(-0.2f, 0.2f));
		dir.Normalize();
		dir *= 0.5f;
		rigidbody2d = GetComponent<Rigidbody2D>();
		feedSprite = GetComponent<SpriteRenderer>();

		isHeavyMetal = randomBool();
		if(isHeavyMetal)
		{
			Color c = new Color(0.95f, 0.33f, 0.3f, 1.0f);
			feedSprite.color = c;
		}
		else
		{
			Color c = new Color(0.4f, 0.5f, 0.3f, 1.0f);
			feedSprite.color = c;
		}
	}
	public void FixedUpdate()
	{
		Vector2 position = rigidbody2d.position;
		position += dir * Time.fixedDeltaTime;
		rigidbody2d.MovePosition(position);

		if(position.x < Consts.leftBorder || position.x > Consts.rightBorder) Destroy(gameObject);
		if(position.y > 0.0) dir.y = -Mathf.Abs(dir.y);
		if(position.y < Consts.downBorder) dir.y = Mathf.Abs(dir.y);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		Fish predator = other.GetComponent<Fish>();

		if (predator != null)
		{
			predator.eat(1, (isHeavyMetal ? 1 : 0) ) ;
			Destroy(gameObject);
		}
	}
	public void OnDestroy()
	{
		count--;
	}
}
