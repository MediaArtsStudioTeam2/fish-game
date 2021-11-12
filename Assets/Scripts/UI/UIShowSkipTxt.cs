using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowSkipTxt : MonoBehaviour
{
    public static UIShowSkipTxt instance { get; private set; }
    [SerializeField] private Text txt;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
//        Show(1.0f);
    }
    public void Show(float time)
    {
        StartCoroutine(Show_(time));
    }
    private IEnumerator Show_(float time)
    {
        Color tempColor = txt.color;
        while (tempColor.a < 0.7f)
        {
            tempColor.a += Time.deltaTime / time;
            txt.color = tempColor;

            if (tempColor.a >= 0.7f) tempColor.a = 0.7f;

            yield return null;
        }
        txt.color = tempColor;
    }
}
