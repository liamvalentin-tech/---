using System;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public KeyCode ZoomButton = KeyCode.Mouse1;
    public bool IsZooming { get; private set; }
    public bool canZoom = true;

    [SerializeField]
    [Range(1f, 130f)]
    private float baseFOV = 60f;

    [SerializeField]
    [Range(1f, 130f)]
    private float zoomFOV = 30f;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    [Range(0f, 10f)]
    private float fovTransitionTime;

    private Coroutine changingFOV;

    void Start()
    {
        IsZooming = canZoom && Input.GetKey(ZoomButton);
        if (IsZooming)
        {
            IsZooming = true;
            if (changingFOV == null)
            {
                changingFOV = StartCoroutine(LerpCamFOV(zoomFOV, fovTransitionTime));
            }
            else
            {
                StopCoroutine(changingFOV);
                changingFOV = StartCoroutine(LerpCamFOV(zoomFOV, fovTransitionTime));
            }
        }
        else
        {
            IsZooming = false;
            if (changingFOV == null)
            {
                changingFOV = StartCoroutine(LerpCamFOV(baseFOV, fovTransitionTime));
            }
            else
            {
                StopCoroutine(changingFOV);
                changingFOV = StartCoroutine(LerpCamFOV(baseFOV, fovTransitionTime));
            }
        }
    }
    private IEnumerator LerpCamFOV(float newFOV, float transitionTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newFOV, transitionTime * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}