using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surfaceOrient : MonoBehaviour
{
    public Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //void OnCollisionEnter(Collision other)
    //{
    //    print("Points colliding: " + other.contacts.Length);
    //    print("First normal of the point that collide: " + other.contacts[0].normal);
    //    m_Rigidbody.MoveRotation(Quaternion.Euler(other.contacts[0].normal));
    //    transform.rotation = Quaternion.FromToRotation(transform.up, other.contacts[0].normal);
    //}
}
