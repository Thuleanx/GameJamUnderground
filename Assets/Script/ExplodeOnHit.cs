
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class ExplodeOnHit : MonoBehaviour
{
	Hitbox hitbox;
	Animator anim;
	MoveLooped movement;
	Rigidbody2D body;
	

	[SerializeField] LayerMask wall;
	[SerializeField] int explodeState = 1;
	[SerializeField] string explodeSound;

	bool playedExplosion;

	void Awake()
	{
		hitbox = GetComponentInChildren<Hitbox>(); 
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
		movement = GetComponent<MoveLooped>();
	}

	void OnEnable() {
		playedExplosion = false;
		anim.SetInteger("AnimState", 0);
	}

	void Update() {
		if (hitbox.GetOverlappingHurtbox().Count > 0 || body.IsTouchingLayers(wall))
			Explode();
	}

	public void Explode() {
		OnCollisionEnter();
	}

	void OnCollisionEnter() {
		anim.SetInteger("AnimState", 1);
		if (movement != null)
			movement.stop  = true;
		if (!playedExplosion) {
			AudioManager.Instance.Play(explodeSound);
			playedExplosion = true;
		}
	}

	public void End() {
		gameObject.SetActive(false);
	}
}
