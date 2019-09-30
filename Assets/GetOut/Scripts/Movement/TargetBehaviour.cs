using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(null);
        
        Vector3 targetForward = Camera.main.transform.forward;
        targetForward.y = 0;
        transform.forward = targetForward;
    }
}
