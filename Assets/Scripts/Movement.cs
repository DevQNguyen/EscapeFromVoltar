
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters
    [SerializeField] float thrustMultiplier = 1000f;
    [SerializeField] float rotationMultiplier = 45f;
    [SerializeField] AudioClip engineThrustClip;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    
    // Cache gameObjects
    Rigidbody rocketRB;
    AudioSource rocketAudio;

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
                rocketAudio.PlayOneShot(engineThrustClip);
            }
            
            // If particles system NOT playing
            if (!mainEngineParticles.isPlaying)
            {
                // Play main engine particles
                mainEngineParticles.Play();
            }

        }
        else
        {
            rocketAudio.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ProcessRotationInput()
    {
        // Allow only one input at a time
        if (Input.GetKey(KeyCode.A))
        {
            if (!rightThrusterParticles.isPlaying)
            {
                // Play left thruster particles
                rightThrusterParticles.Play();
            }

            //Debug.Log("'A' key pressed - Rotate Left!");
            ApplyRotation(rotationMultiplier);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!leftThrusterParticles.isPlaying)
            {
                // Play right thruster particles
                leftThrusterParticles.Play();
            }

            //Debug.Log("'D' key pressed - Rotate Right!");
            ApplyRotation(-rotationMultiplier);
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
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


    public void StopThrusterParticles()
    {
        mainEngineParticles.Stop();
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
}
