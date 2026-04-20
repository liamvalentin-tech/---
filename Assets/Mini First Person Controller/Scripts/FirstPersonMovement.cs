using System;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public GameObject StaminaBar;
    public GameObject StaminaBarBackground;
    public float Stamina = 500f;
    public GameObject ZoomIn;    
    
    [SerializeField]
    [Range(1f, 130f)]
    private float baseFOV = 60f;

    [SerializeField]
    [Range(1f, 130f)]
    private float sprintFOV = 90f;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    [Range(0f, 10f)]
    private float fovTransitionTime;

    private Coroutine changingFOV;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void StaminaBarThing()
    {
        if (IsRunning == true)
        {
            ZoomIn.SetActive(false);
            Stamina -= 80f * Time.deltaTime;
        }
        else
        {
            ZoomIn.SetActive(true);
            Stamina += 40f * Time.deltaTime;
        }
        StaminaBar.transform.localScale = new Vector3(Stamina / 500f, 1, 1);
        StaminaBarBackground.transform.localScale = new Vector3(Stamina / 500f, 1, 1);
        if (Stamina <= 0)
        {
            canRun = false;
            StaminaBarBackground.SetActive(true);
            StartCoroutine(DelaySeconds(3f));
        }
    }
    

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);
            StaminaBarThing();
            DetectSprint();
        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }
    
    public IEnumerator DelaySeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StaminaBarBackground.SetActive(false);
        canRun = true;
    }
    private void DetectSprint()
    {
        if (IsRunning)
        {
            IsRunning = true;
            if (changingFOV == null)
            {
                changingFOV = StartCoroutine(LerpCamFOV(sprintFOV, fovTransitionTime));
            }
            else
            {
                StopCoroutine(changingFOV);
                changingFOV = StartCoroutine(LerpCamFOV(sprintFOV, fovTransitionTime));
            }
        }
        else
        {
            IsRunning = false;
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
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newFOV, transitionTime * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}