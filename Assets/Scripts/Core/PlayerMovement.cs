using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float m_horizontalInput;
    private float m_steerAngle;
    private float m_verticalInput;

    [SerializeField] private float m_motorForce;
    [SerializeField] private float m_maxSteeringAngle;

    [SerializeField] private WheelCollider m_frontLeftWheelCollider;
    [SerializeField] private WheelCollider m_rearLeftWheelCollider;
    [SerializeField] private WheelCollider m_frontRightWheelCollider;
    [SerializeField] private WheelCollider m_rearRightWheelCollider;

    [SerializeField] private Transform m_frontLeftWheelTransform;
    [SerializeField] private Transform m_rearLeftWheelTransform;
    [SerializeField] private Transform m_frontRightWheelTransfrom;
    [SerializeField] private Transform m_rearRightWheelTransform;

    private float speed = 10.0f;

    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheel();

        //Vector3 dir = Vector3.zero;

        //// we assume that device is held parallel to the ground
        //// and Home button is in the right hand

        //// remap device acceleration axis to game coordinates:
        ////  1) XY plane of the device is mapped onto XZ plane
        ////  2) rotated 90 degrees around Y axis
        //dir.x = Input.acceleration.x;
        //dir.z = 0.05f;//Input.acceleration.x

        //// clamp acceleration vector to unit sphere
        //if (dir.sqrMagnitude > 1)
        //    dir.Normalize();

        //// Make it move 10 meters per second instead of 10 meters per frame...
        //dir *= Time.deltaTime;

        //// Move object
        //transform.Translate(dir * speed);
    }
    
    private void GetInput()
    {
        m_verticalInput = Input.acceleration.y * 2.0f;
        m_horizontalInput = Input.acceleration.x * 2.0f;
    }

    private void HandleMotor()
    {
        m_frontLeftWheelCollider.motorTorque = m_verticalInput * m_motorForce;
        m_frontRightWheelCollider.motorTorque = m_verticalInput * m_motorForce;
    }

    private void HandleSteering()
    {
        m_steerAngle = m_maxSteeringAngle * m_horizontalInput;
        m_frontLeftWheelCollider.steerAngle = m_steerAngle;
        m_frontRightWheelCollider.steerAngle = m_steerAngle;
    }

    private void UpdateWheel()
    {
        UpdateSingleWheel(m_frontLeftWheelCollider, m_frontLeftWheelTransform);
        UpdateSingleWheel(m_frontRightWheelCollider, m_frontRightWheelTransfrom);
        UpdateSingleWheel(m_rearLeftWheelCollider, m_rearLeftWheelTransform);
        UpdateSingleWheel(m_rearRightWheelCollider, m_rearRightWheelTransform);
    }

    private void UpdateSingleWheel( WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
