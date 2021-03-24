
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


    #region Methods

    void ProcessThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MainEngineThrusting();
        }
        else
        {
            StopMainEngineThrusting();
        }
    }

    void ProcessRotationInput()
    {
        // Press 'A' to rotate left/counterclockwise
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        // Press 'D' to rotate right/clockwise
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }


    void MainEngineThrusting()
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

    private void StopMainEngineThrusting()
    {
        rocketAudio.Stop();
        mainEngineParticles.Stop();
    }


    private void RotateLeft()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            // Play left thruster particles
            rightThrusterParticles.Play();
        }
        ApplyRotation(rotationMultiplier);
    }

    private void RotateRight()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            // Play right thruster particles
            leftThrusterParticles.Play();
        }
        ApplyRotation(-rotationMultiplier);
    }

    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
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

    #endregion

}


