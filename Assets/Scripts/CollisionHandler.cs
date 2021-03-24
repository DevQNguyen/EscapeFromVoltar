using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float delayInSeconds = 1.5f;
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip explosionClip;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;

    // Cache 
    AudioSource audioSource;

    // State
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("Press 'L' to load next scene.");
        Debug.Log("Press 'C' to toggle collsion disabled/enabled.");
    }

    void Update()
    {
        CheatKeyInputs();
    }

    void OnCollisionEnter(Collision other)
    {       
        // If in transition sequence, return
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log($"Collided with: {other.gameObject.name}!");
                break;
            case "Finish":
                SuccessSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }


    #region Methods

    void CheatKeyInputs()
    {
        // If 'L' key pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Next scene loading...");
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Collisions disabled/enabled...");
            // Toggle collision disabled/enabled
            collisionDisabled = !collisionDisabled;
        }
    }

    /// <summary>
    /// Reload scene when player collides with untagged object. 
    /// </summary>
    /// <param name="objTagName"></param>
    void CrashSequence()
    {
        isTransitioning = true;
        // Play explosion clip
        audioSource.PlayOneShot(explosionClip, 0.2f);
        
        // Play particle effect
        explosionParticles.Play();

        // Disable Movement control
        GetComponent<Movement>().enabled = false;

        // Play crash sound clip
        Invoke("ReloadScene", delayInSeconds);
    }

    /// <summary>
    /// Load next level if player successfully lands on landingpad.
    /// </summary>
    void SuccessSequence()
    {
        isTransitioning = true;
        // Play success clip
        audioSource.PlayOneShot(successClip);

        // Play particle effect
        successParticles.Play();

        // Disable controls
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", delayInSeconds);
    }

    /// <summary>
    /// Load the next scene if player successfully finish current scene.
    /// </summary>
    void LoadNextScene()
    {
        // Cache active scene index
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Increment index to next scene index
        int nextSceneIndex = currentSceneIndex + 1;
        
        // Check if there are enough build scenes to load next scene
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // set nextSceneIndex to zero (first scene index)
            nextSceneIndex = 0;
        }

        // Load nextScene
        SceneManager.LoadScene(nextSceneIndex);

    }

    /// <summary>
    /// Reloads current scene.
    /// </summary>
    void ReloadScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    #endregion 
}



