
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(Animator), typeof(SpriteRenderer))]
public class AnimationManager : MonoBehaviour
{
	public enum State {
		Idle = 0,
		Run = 1,
		Attack = 2,
		Hit = 3
	}

	InputManager input;
	Animator anim;
	SpriteRenderer sprite;

	public State currentState;

	void Awake() {
		input = GetComponent<InputManager>();
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
	}

	public void ResetState() {
		currentState = State.Idle;
		anim.SetInteger("AnimState", (int) currentState);
	}

	public void TriggerAttack() {
		currentState = State.Attack;
		anim.SetInteger("AnimState", (int) currentState);
	}

	public void TriggerHit() {
		currentState = State.Hit;
		anim.SetInteger("AnimState", (int) currentState);
		AudioManager.Instance.Play("Hurt");
	}

	void LateUpdate()
	{
		if (currentState != State.Hit && currentState != State.Attack) {
			if (input.InputDirection == Vector2.zero && input.FireInputDirection == Vector2.zero) {
				currentState = State.Idle;
			} else {
				currentState = State.Run;
			}
		}
		if (input.FireInputDirection != Vector2.zero)
		{
			sprite.flipX = Mathf.Sign(input.FireInputDirection.x) != 1f;
		}
		else if (input.InputDirection != Vector2.zero)
		{
			sprite.flipX = Mathf.Sign(input.InputDirection.x) != 1f;
		}
		anim.SetInteger("AnimState", (int) currentState);
	}
}
