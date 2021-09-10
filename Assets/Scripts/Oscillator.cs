using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    float movementFactor;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    void Oscillate()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283... ( 2 pi )
        float rawSignWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSignWave + 1f) / 2f; // recalculate to go from 0 to 1 (so it goes from start to end, and dont have to start at  the midpoint)

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
