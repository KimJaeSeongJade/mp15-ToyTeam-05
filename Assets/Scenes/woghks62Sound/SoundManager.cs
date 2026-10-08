using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    Cutting=0, Pan, Pot, Dish, Desk, Counter, Result,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BGM, SFX;
    public AudioClip[] BGMArr,  SFXArr;

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

    public void BGMPlay(int numb)
    {
        if (BGM.isPlaying)
        {
            BGM.Stop();
        }

        if (numb >= 0 && numb < BGMArr.Length)
        {
            BGM.clip = BGMArr[numb];
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
    
    
}
