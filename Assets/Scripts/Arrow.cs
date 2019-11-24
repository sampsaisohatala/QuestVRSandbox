﻿using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float m_Speed = 2000f;
    public Transform m_Tip = null;

    Rigidbody m_Rigidbody = null;
    bool m_IsStoppped = false;
    Vector3 m_LastPositon = Vector3.zero;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        transform.localPosition = new Vector3(0, 0, 0.425f);
        transform.localEulerAngles = Vector3.zero;
        Debug.Log("AA: pos: " + transform.localPosition + " / rot: " + transform.localEulerAngles);
    }

    void FixedUpdate()
    {
        if (m_IsStoppped)        
            return;

        if (m_Rigidbody.velocity != Vector3.zero)
             m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));

        if (Physics.Linecast(m_LastPositon, m_Tip.position))
        {
            Stop();
        }

        m_LastPositon = m_Tip.position;
    }

    void Stop()
    {
        m_IsStoppped = true;
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
    }

    public void Fire(float pullValue)
    {
        m_IsStoppped = false;
        transform.parent = null;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(transform.forward * (pullValue * m_Speed));

        Destroy(gameObject, 5f);
    }
}