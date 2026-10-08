using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private UnityEvent _onSoundChanged;
    
    public UnityEvent OnSoundChanged => _onSoundChanged;

    private void Awake() => SetSingleton();

    public void SetMute()
    {
        Debug.Log("SetMute");
        OnSoundChanged?.Invoke();
    }
}
