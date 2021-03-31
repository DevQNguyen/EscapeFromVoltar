using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField][Range(0, 1f)] float movementFactor;
    [SerializeField] Vector3 movementVector;
    Vector3 startPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Cache starting transform position
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate and cache offset position
        Vector3 offsetPos = movementFactor * movementVector;
        Debug.Log($"Offset: {offsetPos}");
        
        // Move transform to new position by adding offset value
        transform.position = startPos + offsetPos;
        Debug.Log($"End Position: {transform.position}");
        
    }
}



