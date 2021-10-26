using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingRock : MonoBehaviour
{
    public float fallingSpeedMin, fallingSpeedMax, rotationSpeedMin, rotationSpeedMax;
    private float fallingSpeed, rotationSpeed;
    private Vector3 rotationAngle;

    private bool isCollidingWithPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        fallingSpeed = Random.Range(fallingSpeedMin, fallingSpeedMax);
        fallingSpeed /= 10;

        rotationAngle = new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isCollidingWithPlayer)
        {
            ApplyGravity(fallingSpeed);
            ApplyAngle(rotationAngle, rotationSpeed);
        }
        
    }

    void ApplyGravity(float fallingSpeed)
    {
        transform.position -= new Vector3(0, fallingSpeed, 0);
    }

    void ApplyAngle(Vector3 rotationAngle, float rotationSpeed)
    {
        transform.Rotate(rotationAngle, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
        }
    }
}