using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BusController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentBrakeForce;
    private float currentSteeringAngle;
    private bool isBraking;
    private float currentSpeed;
    

    [SerializeField] private float enginePower;
    [SerializeField] private float brakePower;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float maxSpeed;

    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;

    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearRightTransform;
    [SerializeField] private Transform rearLeftTransform;

    [SerializeField] private Rigidbody rigidbody;

    

    private void Update()
    {
        GetInput();
        PowerEngine();
        Steering();
        UpdateWheel(frontLeftCollider, frontLeftTransform);
        UpdateWheel(frontRightCollider, frontRightTransform);
        UpdateWheel(rearLeftCollider, rearLeftTransform);
        UpdateWheel(rearRightCollider, rearRightTransform);
    }

    private void GetInput() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void PowerEngine()
    {
        currentSpeed = Mathf.Sqrt(Mathf.Pow(rigidbody.velocity.sqrMagnitude, 1.3f));
        if (currentSpeed < maxSpeed) //checks if top speed has been reached
        {
            frontLeftCollider.motorTorque = verticalInput * enginePower;
            frontRightCollider.motorTorque = verticalInput * enginePower;
        }
        else
        {
            frontLeftCollider.motorTorque = currentSpeed;
            frontRightCollider.motorTorque = currentSpeed;
        }

        
        currentBrakeForce = isBraking ? brakePower : 0f; 
        Braking();
        
    }

    private void Braking()
    {
        frontLeftCollider.brakeTorque = currentBrakeForce;
        frontRightCollider.brakeTorque = currentBrakeForce;
        rearRightCollider.brakeTorque = currentBrakeForce;
        rearLeftCollider.brakeTorque = currentBrakeForce;
    }

    private void Steering()
    {
        currentSteeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftCollider.steerAngle = currentSteeringAngle;
        frontRightCollider.steerAngle = currentSteeringAngle;
    }

    private void UpdateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        transform.rotation = rotation;
        transform.position = position;
    }
    
}
