using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBTarget : MonoBehaviour
{
    private Rigidbody m_RigidBody;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    public void Push()
    {
        m_RigidBody.AddForce(-transform.forward * 100.0f, ForceMode.Impulse);
    }
}
