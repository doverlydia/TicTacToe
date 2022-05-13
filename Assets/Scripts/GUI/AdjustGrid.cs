using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustGrid : MonoBehaviour
{
    [SerializeField] RectTransform boardRectTransform;
    private void Awake()
    {
        float width = boardRectTransform.rect.width;
        float height = boardRectTransform.rect.height;
        Vector2 newSize = new Vector2(width / 3, height / 3);
        GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
