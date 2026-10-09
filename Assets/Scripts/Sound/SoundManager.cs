using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum BGMType
{
    Title,Game
}

public enum SFXType
{
    // SFX 사운드 넣어야함
    // Cutting : Cutting
    // Pan : Pan
    // Pot : Pot
    // Dish : 사운드 32
    // Desk : 사운드 21
    // Counter : 사운드 45
    // Throw : 사운드 47?
    // Result : 사운드 18
    Cutting=0, Pan, Pot, Dish, Desk, Counter, Result, GameStart
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BGM, SFX;
    public AudioClip[] BGMArr,  SFXArr;

    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BGMPlay(BGMType type)
    {
        int index = (int)type;

        BGM.Stop();

        if (index >= 0 && index < BGMArr.Length)
        {
            BGM.clip = BGMArr[index];
            BGM.loop = true;
            BGM.Play();
        }
    }


    public void SFXPlay(SFXType type)
    {
        int index = (int)type;

        if (index >= 0 && index < SFXArr.Length)
        {
            Debug.Log(index);
            Debug.Log(SFXArr[index]);
            SFX.PlayOneShot(SFXArr[index]);
        }
    }
    

    public void BGMStop(BGMType type)
    {
        int index = (int)type;
        BGM.Stop();
    }
}
