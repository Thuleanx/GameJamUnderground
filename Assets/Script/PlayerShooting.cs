
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Firable), typeof(InputManager))]
public class PlayerShooting : MonoBehaviour
{
	InputManager input;
	Firable fireTrigger;
	AnimationManager animManager;

	void Awake() {
		input = GetComponent<InputManager>();
		fireTrigger = GetComponent<Firable>();
		animManager = GetComponent<AnimationManager>();
	}

	void Update()
	{
		if (input.FireInputDirection != Vector2.zero && fireTrigger.canShoot && (animManager == null ? true : animManager.currentState != AnimationManager.State.Hit)) {
			// Priority on the horizontal
			if (input.FireInputDirection.x != 0)
				fireTrigger.OnFireDirection(Vector2.right * input.FireInputDirection.x);
			else // honestly just input.FireInputDirection would work
				fireTrigger.OnFireDirection(Vector2.up * input.FireInputDirection.y);
			animManager?.TriggerAttack();
		}
	}
}
