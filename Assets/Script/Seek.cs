using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Seek : MonoBehaviour
{
	Rigidbody2D body;
	Hurtbox hurtbox;
	GameObject lightCue;

	[SerializeField] float speed;

	[HideInInspector] public Vector2 seekTarget;
	bool seeking = false;
	[HideInInspector]
	public bool dying = false;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		hurtbox = GetComponentInChildren<Hurtbox>();
		lightCue = GameObject.FindGameObjectWithTag("FollowLight");
	}

	public void SetTarget(Vector2 target) 
	{
		seekTarget = target;
		seeking = true;
		if (lightCue != null)
			lightCue.transform.position = target;
	}

	void Update()
	{
		body.velocity = Vector2.zero;
		if (seeking && !dying) {
			body.velocity = (seekTarget - (Vector2)transform.position);
			float len2 = body.velocity.sqrMagnitude;
			if (len2 > speed * speed * Time.deltaTime)
				body.velocity = (seekTarget - (Vector2)transform.position).normalized * speed;
			else seeking = false;
		}
		if (hurtbox != null) {
			body.velocity += hurtbox.knockBack;
		}
	}
}
