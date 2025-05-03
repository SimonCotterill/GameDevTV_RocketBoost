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
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ThrustPressed();
        Rotation();
    }

    private void ThrustPressed()
    {
        if (thrust.IsPressed())
        {
            ThrustProcess();
        }

        else
        {
            audioSource.Stop();
            mainThrustParticles.Stop();
        }
    }

    private void Rotation()
    {

        float rotationInput = rotation.ReadValue<float>();
        
        if (rotationInput == 1)
        {
            RightTurnProcess();
        }
        else if (rotationInput == -1)
        {
            LeftTurnProcess();
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

    private void ThrustProcess()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineAudio);
        }
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    private void RightTurnProcess()
    {
        RotationAction(1);
        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    private void LeftTurnProcess()
    {
        RotationAction(-1);
        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }
}
