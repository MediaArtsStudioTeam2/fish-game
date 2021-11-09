using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHMMaxBar : UIBar
{
	public static UIHMMaxBar instance { get; private set; }
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
        SetValue(140f / 300f);
    }
}
