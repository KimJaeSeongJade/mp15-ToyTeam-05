using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private float playTime;
    
    public float PlayTime { get => playTime; set => playTime = value; }
    
    [SerializeField] private TextMeshProUGUI timeText;

    void Update()
    {
        // 일시정지 상태를 반영하여 시간 누적
        playTime -= Mathf.FloorToInt(Time.deltaTime) * Time.deltaTime; 
        timeText.text = playTime.ToString();
    }
}
