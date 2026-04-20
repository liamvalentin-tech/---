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

    public GameObject Crosshair;
    public Animator MoveLaser;

    [SerializeField] private FirstPersonMovement firstPersonMovement;
    void Update()
    {   
        firstPersonMovement.DetectAim(IsZooming, zoomFOV, fovTransitionTime);
        IsZooming = canZoom && Input.GetKey(ZoomButton);
        if (IsZooming)
        {
            Crosshair.SetActive(true);
            /*if (checkLerp == null)
            {
                firstPersonMovement.changingFOV = firstPersonMovement.StartCoroutine(LerpCamFOV(zoomFOV, fovTransitionTime));
            }
            else
            {
                firstPersonMovement.StopCoroutine(firstPersonMovement.changingFOV);
                firstPersonMovement.changingFOV = firstPersonMovement.StartCoroutine(LerpCamFOV(zoomFOV, fovTransitionTime));
            } */
        }
        else
        {
            Crosshair.SetActive(false);
            /*if (checkLerp == null)
            {
                firstPersonMovement.changingFOV = firstPersonMovement.StartCoroutine(LerpCamFOV(baseFOV, fovTransitionTime));
            }
            else
            {
                firstPersonMovement.StopCoroutine(firstPersonMovement.changingFOV);
                firstPersonMovement.changingFOV = firstPersonMovement.StartCoroutine(LerpCamFOV(baseFOV, fovTransitionTime));
            } */
        }
    }
    /*private IEnumerator LerpCamFOV(float newFOV, float transitionTime)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionTime)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newFOV, transitionTime * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }*/
    
}