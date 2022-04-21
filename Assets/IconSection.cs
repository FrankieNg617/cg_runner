using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSection : MonoBehaviour
{
    private void Awake()
    {
        GridLayoutGroup layout = GetComponent<GridLayoutGroup>();
        if (Screen.width >= 1500)
        {
            layout.padding.left = 100;
            layout.padding.right = 50;
            layout.padding.top = 70;
            layout.cellSize = new Vector2(100, 100);
            layout.spacing = new Vector2(100, 60);
        }
        else
        {
            layout.padding.left = 100;
            layout.padding.right = 0;
            layout.padding.top = 0;
            layout.cellSize = new Vector2(80, 80);
            layout.spacing = new Vector2(30, 60);
        }
    }
}
