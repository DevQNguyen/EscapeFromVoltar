using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        // Grab object tag name and assign to variable
        string tagString = other.gameObject.tag;
        
        switch (tagString)
        {
            case "Friendly":
                Debug.Log("Collided with a Friendly object!");
                break;
            case "Fuel":
                Debug.Log("You got a fuel pod!");
                break;
            case "Finish":
                Debug.Log("You have successfully finished the course!");
                break;
            default:
                Debug.Log("Collided with some other object!");
                break;
        }
    }
}



