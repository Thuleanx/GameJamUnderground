

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class MobAnimationManager : MonoBehaviour
{
	public enum State {
		Idle = 0,
		Run = 1,
		Hit = 2,
		Die = 3
	}

	Animator anim;
	SpriteRenderer sprite;
	Rigidbody2D body;
	Seek seekTargetScript;

	public State currentState;

	[SerializeField] float soundFrequency = 2;
	float timeNextSound;

	void Awake() {
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		body = GetComponent<Rigidbody2D>();
		seekTargetScript = GetComponent<Seek>();
	}

	public void ResetState() {
		if (currentState != State.Die) {
			currentState = State.Idle;
			anim.SetInteger("AnimState", (int)currentState);
		}
	}

	public void TriggerHit() {
		if (currentState != State.Die) {
			currentState = State.Hit;
			AudioManager.Instance.Play("Growl");
			anim.SetInteger("AnimState", (int)currentState);
		}
	}

	public void TriggerDie() {
		currentState = State.Die;	
		anim.SetInteger("AnimState", (int) currentState);
		seekTargetScript.dying = true;
	}

	public void Death() {
		gameObject.SetActive(false);
	}

	void LateUpdate()
	{
		if (body.velocity != Vector2.zero) {
			sprite.flipX = Mathf.Sign(body.velocity.x) == -1; 
			if (currentState != State.Hit && currentState != State.Die) {
				currentState = State.Run;
			}
		} else if (currentState == State.Run) {
			currentState = State.Idle;
		}

		anim.SetInteger("AnimState", (int) currentState);
	}
}
