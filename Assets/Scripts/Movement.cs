using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //                  STRUCTURE 
    // PARAMETERS - for tuning, typically in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables
    
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainThrustAudio;
    
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    
    Rigidbody rb;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {              
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainThrustAudio);
            }

            if (!mainThrustParticles.isPlaying)
            {
                mainThrustParticles.Play();
            }
        }

        else
        {
            audioSource.Stop();
            mainThrustParticles.Stop();
        }
    }
    
    void ProcessRotation()
    {     
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            
            if (!rightBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Play();
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            
            if (!leftBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Play();
            }
        }

        else
        {
            rightBoosterParticles.Stop();
            leftBoosterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation so the physics system can take over.
    }
}
