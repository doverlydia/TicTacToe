using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MainMenuShowCase : MonoBehaviour
{
    [SerializeField] RectTransform logo;
    [SerializeField] RectTransform skinButton;
    [SerializeField] RectTransform playButton;
    private void Start()
    {
        logo.localScale = Vector2.zero;
        skinButton.localScale = Vector2.zero;
        playButton.localScale = Vector2.zero;
    }
    public void DoMainMenuSequence()
    {
        Sequence menuSequence = DOTween.Sequence();
        menuSequence
            .Append(logo.DOScale(1, 1.5f).SetEase(Ease.OutElastic))
            .InsertCallback(1, () =>
             {
                 StartCoroutine(SineRotationLoop(logo, 3));
             })
            .Insert(0.5f, skinButton.DOScale(1, 1.5f).SetEase(Ease.OutElastic))
            .Insert(1, playButton.DOScale(1, 1.5f).SetEase(Ease.OutElastic));
    }

    IEnumerator SineRotationLoop(RectTransform rect, int strength)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            float angle = Mathf.Sin(time) * strength;
            rect.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return null;
        }
    }
}
