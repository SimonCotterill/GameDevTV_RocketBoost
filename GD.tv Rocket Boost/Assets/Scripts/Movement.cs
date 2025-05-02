using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 40f;
    [SerializeField] float rotationStrength = 40f;

    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem mainThrustParticles;

    [SerializeField] AudioClip mainEngineAudio;

    AudioSource audioSource;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //when object is enabled, enable the thrust method to occur ?
        thrust.Enable();
        rotation.Enable();
    }

    private void Update()
    {
        if (thrust.IsPressed())
        {
            //UnityEngine.Debug.Log("pressing space");
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineAudio);        
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

    private void FixedUpdate()
    {
        Thrust();
        Rotation();

    }


    private void Thrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);     
        }
    }


    private void Rotation()
    {

        float rotationInput = rotation.ReadValue<float>();
        
        if (rotationInput == 1)
        {
            RotationAction(1);
            if (!rightThrustParticles.isPlaying)
            {
                leftThrustParticles.Stop();
                rightThrustParticles.Play();
            }
        }
        else if (rotationInput == -1)
        {
            RotationAction(-1);
            if (!leftThrustParticles.isPlaying)
            {
                rightThrustParticles.Stop();
                leftThrustParticles.Play();
            }
        }
        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }

    }

    private void RotationAction(float direction)
    {
        rb.freezeRotation = true;
        transform.Rotate(direction * Vector3.forward * rotationStrength * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
