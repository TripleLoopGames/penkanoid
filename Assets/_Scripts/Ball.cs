using System;
using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    private Rigidbody2D ownRigidbody;
    private float lastSpeed = 0;

	// Use this for initialization
	void Start ()
	{
        Vector2 randomForce = new Vector2(1, 0.6f);
        ownRigidbody = GetComponent<Rigidbody2D>();
        ownRigidbody.AddForce(randomForce * 5, ForceMode2D.Impulse);
    }

    void Update()
    {
        float currentSpeed = ownRigidbody.velocity.magnitude;
        if (currentSpeed != lastSpeed)
        {
            Debug.Log(currentSpeed);
        }
        lastSpeed = currentSpeed;
    }

}
