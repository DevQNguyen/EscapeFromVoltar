using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float delayInSeconds = 1f;

    void OnCollisionEnter(Collision other)
    {
        // Grab object tag name and assign to variable
        string tagString = other.gameObject.tag;
        
        switch (tagString)
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
        // [ToDo] add SFX
        // [ToDo] add particle effect

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
        // [ToDo] add SFX
        // [ToDo] add particle effect

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



