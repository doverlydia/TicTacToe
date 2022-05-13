using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PopUpUtils : MonoBehaviour
{
    public void PopIn()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f).SetEase(Ease.OutSine);
    }
    public void PopOut()
    {
        transform.DOScale(0, 0.2f).SetEase(Ease.InSine).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
