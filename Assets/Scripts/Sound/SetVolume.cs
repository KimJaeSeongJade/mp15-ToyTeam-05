using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        if (gameObject.name == "MasterSlider")
        {
            GetComponent<Slider>().value = SoundManager.Instance.masterVolume;
        }

        if (gameObject.name == "BgmSlider")
        {
            GetComponent<Slider>().value = SoundManager.Instance.bgmVolume;
        }

        if (gameObject.name == "SfxSlider")
        {
            GetComponent<Slider>().value = SoundManager.Instance.sfxVolume;
        }
    }

    public void SetLevel(float sliderVal)
    {
        float volume = Mathf.Log10(sliderVal) * 20;

        if (gameObject.name == "MasterSlider")
        {
            _audioMixer.SetFloat("Master", volume);
            SoundManager.Instance.masterVolume = sliderVal;
        }

        if (gameObject.name == "BgmSlider")
        {
            _audioMixer.SetFloat("BGM", volume);
            SoundManager.Instance.bgmVolume = sliderVal;
        }

        if (gameObject.name == "SfxSlider")
        {
            _audioMixer.SetFloat("SFX", volume);
            SoundManager.Instance.sfxVolume = sliderVal;
        }
    }
}
