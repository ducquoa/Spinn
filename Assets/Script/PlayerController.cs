using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject anchorSphere;
    [SerializeField] GameObject spinningSphere;
    [SerializeField] float spinRadius = 2f;
    [SerializeField] float spinSpeed = 100f;

    [SerializeField] LineRenderer ropeRenderer;
    [SerializeField] int ropePointsCount = 10;
    [SerializeField] CapsuleCollider ropeCollider;

    [SerializeField] GroundDetection[] anchorGroundDetections;

    

    bool isClockwise = true;
    

    void Start()
    {
        
        spinningSphere.transform.position = anchorSphere.transform.position + new Vector3(spinRadius, 0, 0);
        RopeRenderer();
    
    }

    private void RopeRenderer()
    {
        // Set the number of points in the Line Renderer
        ropeRenderer.positionCount = ropePointsCount;

        // Calculate the distance between the two spheres
        float distance = Vector3.Distance(anchorSphere.transform.position, spinningSphere.transform.position);

        // Calculate the position of each point on the rope
        for (int i = 0; i < ropePointsCount; i++)
        {
            float t = i / (ropePointsCount - 1);
            Vector3 pointPosition = Vector3.Lerp(anchorSphere.transform.position, spinningSphere.transform.position, t);
            ropeRenderer.SetPosition(i, pointPosition);
        }

       
    }

    void Update()
    {
        // Update the position and rotation of the Capsule Collider
        RopeCollider();

        // Update the position of each point on the rope
        for (int i = 0; i < ropePointsCount; i++)
        {
            float t = i / (ropePointsCount - 1);
            Vector3 pointPosition = Vector3.Lerp(anchorSphere.transform.position, spinningSphere.transform.position, t);
            ropeRenderer.SetPosition(i, pointPosition);
        }

        if (Input.GetMouseButtonDown(0))
        {
            SwitchRoles();
            SwitchRotationDirection();
        }

        foreach (var anchorGroundDetection in anchorGroundDetections)
        {
            if (anchorGroundDetection != null && anchorGroundDetection.IsAnchorOnGround())
            {
                if (isClockwise)
                {
                    spinningSphere.transform.RotateAround(anchorSphere.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
                }
                else
                {
                    spinningSphere.transform.RotateAround(anchorSphere.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
                }
            }
            else { StopSpinMotion(); }
        }
    }

    private void RopeCollider()
    {
        // Calculate the distance between the two spheres
        // float distance = Vector3.Distance(anchorSphere.transform.position, spinningSphere.transform.position);

        // Calculate the midpoint position of the rope
        //Vector3 midpoint = anchorSphere.transform.position + (spinningSphere.transform.position - anchorSphere.transform.position) / 2f;

        // Update the position and rotation of the Capsule Collider
        ropeCollider.transform.position = spinningSphere.transform.position;
        ropeCollider.transform.LookAt(anchorSphere.transform.position);
        ropeCollider.transform.rotation *= Quaternion.Euler(90f, 0f, 0f);

        // Set the size and center of the collider to cover the entire distance
        ropeCollider.height = 3.8f;
        ropeCollider.center = new Vector3(0f, 1.87f, 0f);            //Not optimized but will do for now


    }

    private void SwitchRotationDirection()
    {
        isClockwise = !isClockwise;
        spinSpeed *= -1f; // Reverse the spin speed to switch the rotation direction
    }

    private void SwitchRoles()
    {
        // Swap the references of the anchor and spinning spheres
        GameObject tempSphere = anchorSphere;
        anchorSphere = spinningSphere;
        spinningSphere = tempSphere;


    }
    public void StopSpinMotion()
    {
        spinSpeed = 0f;
    }

    public GameObject GetAnchorSphere()
    {
        return anchorSphere;
    }

    public GameObject GetSpinningSphere()
    {
        return spinningSphere;
    }

}
