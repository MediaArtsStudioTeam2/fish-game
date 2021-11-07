using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHMBar : UIBar
{
	public static UIHMBar instance { get; private set; }
	void Awake()
	{
		instance = this;
	}
    protected override void preStart()
    {
        SetHoriz(false);
    }
    protected override void reset()
    {
        SetValue(0f);
    }
}