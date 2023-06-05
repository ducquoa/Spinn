using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CylinderSoftBody : MonoBehaviour
{
    Rigidbody rootRb;
    

    [Header("Bones")]
    public GameObject root = null;
    public GameObject x = null;
    public GameObject x2 = null;
    [Header("Spring Joint Settings")]
    [Tooltip("Strength of spring")]
    public float Spring = 100f;
    [Tooltip("Higher the value the faster the spring oscillation stops")]
    public float Damper = 0.2f;
    [Header("Other Settings")]
    public Softbody.ColliderShape Shape = Softbody.ColliderShape.Box;
    public float ColliderSize = 0.002f;
    public float RigidbodyMass = 1f;


    private void Start()
    {
        
        // Set the root's Rigidbody to kinematic
        rootRb = root.GetComponent<Rigidbody>();
        if (rootRb != null)
        {
            rootRb.isKinematic = true;
        }

             
        Softbody.Init(Shape, ColliderSize, RigidbodyMass, Spring, Damper, RigidbodyConstraints.FreezeRotation);        
        Softbody.AddCollider(ref x);    // Skip root bone to keep the whole object in place
        Softbody.AddCollider(ref x2);

        Softbody.AddSpring(ref x, ref root);
        Softbody.AddSpring(ref x2, ref root);

    }


}
