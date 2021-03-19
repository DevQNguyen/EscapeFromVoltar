using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRB;
    AudioSource rocketAudio;
    [SerializeField] float thrustMultiplier = 1000f;
    [SerializeField] float rotationMultiplier = 45f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Assign reference variable to rigid body of this object
        rocketRB = GetComponent<Rigidbody>();
        // Grab Audio Source comoponent
        rocketAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrustInput();
        ProcessRotationInput();
    }

    void ProcessThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Add relative force to rocket
            rocketRB.AddRelativeForce(Vector3.up * thrustMultiplier * Time.deltaTime);
            
            // If clip is NOT playing
            if (!rocketAudio.isPlaying)
            {
                // Play rocket boost sound
                rocketAudio.Play();
            }
        }
        else
        {
            rocketAudio.Stop();
        }
    }

    void ProcessRotationInput()
    {
        // Allow only one input at a time
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("'A' key pressed - Rotate Left!");
            ApplyRotation(rotationMultiplier);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("'D' key pressed - Rotate Right!");
            ApplyRotation(-rotationMultiplier);
            
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // Freezing rotation so we can manually rotate
        rocketRB.freezeRotation = true;
        // Manually apply rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // Unfreeze rotation to allow physics to take over again
        rocketRB.freezeRotation = false;
    }
}
