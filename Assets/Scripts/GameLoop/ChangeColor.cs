using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Button button;
    private bool isClick = false;
    
    private void ColorChange()
    {
        ColorBlock colors = button.colors;

        isClick = !isClick;
        
        colors.normalColor = isClick ? colors.highlightedColor : colors.normalColor;
    }
}
