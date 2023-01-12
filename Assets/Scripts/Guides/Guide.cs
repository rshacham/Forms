using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Guide : MonoBehaviour
{
    [SerializeField] private GameObject beforeMsg;
    [SerializeField] private GameObject afterMsg;
    
    protected bool conditionToNextGuide;

    protected virtual void Update()
    {
        if (conditionToNextGuide)
        {
            beforeMsg.SetActive(false);
            afterMsg.SetActive(true);
        }
    }
}
