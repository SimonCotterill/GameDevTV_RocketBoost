using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 40f;
    [SerializeField] float rotationStrength = 40f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }
        else if (rotationInput == -1)
        {
            RotationAction(-1);
        }

    }

    private void RotationAction(float direction)
    {
        rb.freezeRotation = true;
        transform.Rotate(direction * Vector3.forward * rotationStrength * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
