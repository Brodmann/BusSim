using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentBrakeForce;
    private float currentSteeringAngle;
    private bool isBraking;

    [SerializeField] private float enginePower;
    [SerializeField] private float brakePower;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;

    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    private void FixedUpdate()
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
        frontLeftCollider.motorTorque = verticalInput * enginePower;
        frontRightCollider.motorTorque = verticalInput * enginePower;
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
