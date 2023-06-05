using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovementForIsland : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float distance = 100f;

    private Vector3[] waypoints;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        // Calculate the waypoints of the square path
        waypoints = new Vector3[4];
        waypoints[0] = transform.position + Vector3.forward * distance;
        waypoints[1] = waypoints[0] + Vector3.right * distance;
        waypoints[2] = waypoints[1] - Vector3.forward * distance;
        waypoints[3] = transform.position;
    }

    private void Update()
    {
        // Move towards the current waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if reached the current waypoint
        if (transform.position == targetPosition)
        {
            // Move to the next waypoint
            currentWaypointIndex++;

            // Wrap around to the first waypoint if reached the last waypoint
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = 0;
        }
    }
}
