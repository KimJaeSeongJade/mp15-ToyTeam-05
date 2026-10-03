using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    private bool isChoose = false;
    
    // onClick
    public void OnButtonClick(Image _ready)
    {
        isChoose = !isChoose;

        if (isChoose)
        {
            _ready.color = new Color(11, 207, 98);
        }
        else
        {
            _ready.color = new Color(255, 140, 85);
        }
    }
}
