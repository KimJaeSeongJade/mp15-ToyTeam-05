using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton")][Tooltip("true면 씬 전환 시 파괴")]
    [SerializeField] private bool _destroyOnLoad;
    
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 싱글톤 설정 함수. Awake에서 반드시 호출할 것.
    /// </summary>
    protected void SetSingleton()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DestroyOnLoad();
        }
    }

    private void DestroyOnLoad()
    {
        if (_destroyOnLoad) return;
        DontDestroyOnLoad(gameObject);
    }
}
