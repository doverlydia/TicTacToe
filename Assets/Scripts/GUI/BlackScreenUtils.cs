using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlackScreenUtils : MonoBehaviour
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.canvasRenderer.SetAlpha(0);
    }
    public void FadeBlackScreen(float value)
    {
        image.CrossFadeAlpha(value, 0.2f, false);
    }
}
