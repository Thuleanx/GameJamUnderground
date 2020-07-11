using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveLooped : MonoBehaviour
{
	Rigidbody2D body;

	[SerializeField] public float speed;
	[SerializeField] public float accelToTopSpeed = 3;
	[HideInInspector] public bool stop;

	Vector2 accel;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}

	void OnEnable() {
		stop = false;
		accel = Vector2.zero;
	}

	void Update()
	{
		if (!stop) {
			// body.velocity = speed * (Vector2) transform.right;	
			body.velocity = Vector2.SmoothDamp((Vector2) body.velocity, speed * (Vector2) transform.right, ref accel, accelToTopSpeed);
		} else {
			body.velocity = Vector2.zero;
		}
	}
}
