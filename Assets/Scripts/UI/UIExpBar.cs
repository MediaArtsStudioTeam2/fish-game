using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExpBar : UIBar
{
	public static UIExpBar instance { get; private set; }
	void Awake()
	{
		instance = this;
	}
	protected override void preStart()
	{
		SetHoriz(true);
	}
	protected override void reset()
	{
		SetValue(0f);
	}
}
