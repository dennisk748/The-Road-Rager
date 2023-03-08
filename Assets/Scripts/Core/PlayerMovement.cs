using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isFlat = true;
    private Rigidbody m_rb; 

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;

        if(isFlat)
            tilt = Quaternion.Euler(90, 0 ,0) * tilt;

        m_rb.AddForce(tilt);

    }
}
