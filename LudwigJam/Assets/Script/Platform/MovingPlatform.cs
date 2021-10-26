using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 speed = new Vector3(0, 0, 0);
    public int speedDivider = 1;
    public float distanceMax = -1;

    private Vector3 StartPosition;

    private void Start()
    {
        StartPosition = gameObject.transform.position;
    }

    private void Update()
    {
        Move();

        if (distanceMax >= 0)
            speed = DistanceFromStart() > distanceMax ? -speed : speed;

    }

    private void Move()
    {
        gameObject.transform.position += ((speed / speedDivider) * Time.deltaTime);
    }

    private float DistanceFromStart()
    {
        return Vector3.Distance(transform.position, StartPosition);
    }
}
