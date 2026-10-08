using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _muteToggle;
    
    private float _prevSoundVolume;

    private void Start() => BindSoundEvents();
    private void OnDisable() => UnBindSoundEvents();

    private void BindSoundEvents()
    {
        // 이벤트 구독
        AudioController.Instance.OnSoundChanged.AddListener(OnMuteSound);
    }
    
    private void UnBindSoundEvents()
    {
        // 이벤트 해지
        AudioController.Instance.OnSoundChanged.RemoveListener(OnMuteSound);
    }
    
    private void OnMuteSound()
    {
        Debug.Log("OnMuteSound");
        if (_muteToggle.isOn)
        {
            _prevSoundVolume = _volumeSlider.value;
            _volumeSlider.value = 0;
        }
        else
        {
            _volumeSlider.value = _prevSoundVolume;
        }
    }
}
