using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiRot : MonoBehaviour
{
    private Transform _cam;

    private void Awake() => Get();

    private void LateUpdate()
    {
        Rot();
    }

    private void Get()
    {
        _cam = Camera.main.transform;
    }

    private void Rot()
    {
        transform.forward = _cam.forward;
    }
}