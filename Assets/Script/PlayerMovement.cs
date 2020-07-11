using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D body;
	InputManager input;
	Hurtbox hurtbox;

	// Tweakables
	[SerializeField] float speed;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
		input = GetComponent<InputManager>();
		hurtbox = GetComponentInChildren<Hurtbox>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 projSpeed = input.InputDirection * speed;
		body.velocity = projSpeed + (hurtbox == null ? Vector2.zero : hurtbox.knockBack);
	}
}
