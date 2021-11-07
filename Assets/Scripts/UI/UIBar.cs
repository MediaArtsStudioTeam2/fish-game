using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
//	public static UIBar instance { get; private set; }
	public Image mask;
	private bool isHorizontal;
	float originalSize;
	
	protected void SetHoriz(bool isHoriz)
	{
		isHorizontal=isHoriz;
	}
	protected virtual void preStart()
	{
//		SetHoriz(true);
	}
	protected virtual void reset()
	{
//		SetValue(0f);
	}
	void Start()
	{
		preStart();
		if(isHorizontal) originalSize = mask.rectTransform.rect.width;
		else originalSize = mask.rectTransform.rect.height;
		reset();
	}
	public void SetValue(float value)
	{
		Debug.LogFormat("isHoriz : {0}, origSize : {1}, value : {2}",isHorizontal, originalSize, value);
		float size = originalSize * value;
		if(isHorizontal) mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
		else mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
	}
}