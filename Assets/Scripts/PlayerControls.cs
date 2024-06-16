using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //The Header and the Tooltip is just for information it does not change anything in our code 
    [Header("Ship movement settings")]
    [Tooltip("The ship up and down speed")] [SerializeField] float controlSpeed = 1f;
    [Tooltip("The maximum left and right range")] [SerializeField] float xRange = 5f;
    [Tooltip("The maximum up and down range")] [SerializeField] float yRange = 5f;

    [Header("Ship rotation settings")]
    [SerializeField] float positionPitch = -2f;
    [SerializeField] float controlPitch = -15f;

    [SerializeField] float positionYaw = -5f;
    [SerializeField] float controlYaw = -10;

    [SerializeField] float controlRoll = 5;

    [Header("The ships lasers")]
    [SerializeField] GameObject[] lasers; 


    float xThrow;
    float yThrow;
    
    void Start()
    {
        controlSpeed = controlSpeed * 10;
    }

    void Update()
    {
        Moving();
        Rotating();
        Firing();

    }
    private void Rotating()
    {
        float pitch = transform.localPosition.y * positionPitch + yThrow * controlPitch;
        float yaw = transform.localPosition.x * positionYaw + xThrow * controlYaw;
        float roll = xThrow * controlRoll;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void Moving()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newxPos = transform.localPosition.x + xOffset;
        float clampedxPos = Mathf.Clamp(newxPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newyPos = transform.localPosition.y + yOffset;
        float clampedyPos = Mathf.Clamp(newyPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedxPos, clampedyPos, transform.localPosition.z);
    }

    void Firing()
    {
        if (Input.GetButton("Fire3"))
        {
            ActivateLasers(true);
        }
        else
        {
            ActivateLasers(false);
        }
    }

    

    void ActivateLasers(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
