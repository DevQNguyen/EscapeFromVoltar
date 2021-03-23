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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {       
        // If in transition sequence, return
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with a Friendly object!");
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
        Debug.Log("Play crash sound!");
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
        Debug.Log("Play Success sound!");
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



