using UnityEngine;

public class Oscillator : MonoBehaviour
{
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

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
        // Protect against division by 0 NaN error
        if (period <= Mathf.Epsilon) { return; }
   
        // ex. 10 sec / 2 sec = 5 cycles. Value gets larger over time
        float cycles = Time.time / period;
        
        // Convert pi to radian
        const float tau = Mathf.PI * 2;

        // Calculate raw Sin wave over time, cycles between -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);

        // Convert sin wave to move from 0 to 1 to 0
        movementFactor = (rawSinWave + 1f) / 2f;

        // Calculate and cache offset position
        Vector3 offsetPos = movementFactor * movementVector;

        // Move transform to new position by adding offset value
        transform.position = startPos + offsetPos;
    }
}




