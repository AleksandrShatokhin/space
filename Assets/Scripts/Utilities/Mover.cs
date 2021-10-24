using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed = 25.0f;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		rb.velocity = transform.forward * speed;
	}


    private void Update()
    {
		
	}
}
