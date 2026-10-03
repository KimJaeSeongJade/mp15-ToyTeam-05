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
            _ready.color = new Color(11f, 207f, 98f, 255f);
        }
        else
        {
            _ready.color = new Color(255f,140f,85f,255f);
        }
    }
}
