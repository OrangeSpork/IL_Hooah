﻿using System.Collections;
using AIChara;
using UnityEngine;

public class HijackLook : MonoBehaviour
{
    private ChaControl chaControl;
    private EyeLookController lookAtController;
    private Transform originalTransform;

    private void Awake()
    {
        StartCoroutine("FindTarget");
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        StopCoroutine("FindTarget");
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            chaControl = GetComponentInParent<ChaControl>();

            if (chaControl != null)
            {
                lookAtController = chaControl.eyeLookCtrl;
                if (lookAtController != null)
                {
                    if (originalTransform == null) originalTransform = lookAtController.target;

                    lookAtController.target = enabled ? transform : Camera.main.transform;
                }
            }
            else
            {
                if (originalTransform != null && lookAtController != null) lookAtController.target = originalTransform;
                lookAtController = null;
                originalTransform = null;
            }
        }
    }
}