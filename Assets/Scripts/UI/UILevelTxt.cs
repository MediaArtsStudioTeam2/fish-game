using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelTxt : MonoBehaviour
{
    public static UILevelTxt instance { get; private set; }
    public Text level;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SetValue(1);
    }
    public void SetValue(int value)
    {
        if(value > 0) level.text = value.ToString();
    }
}
