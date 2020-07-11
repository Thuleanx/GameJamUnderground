
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnHit : MonoBehaviour
{
	Hitbox hitbox;	
	Rigidbody2D body;

	[SerializeField] bool hitMultipleTimes;
	[SerializeField] float forceMultiplier;

	HashSet<Hurtbox> previousHits = new HashSet<Hurtbox>();

	void Awake() {
		hitbox = GetComponentInChildren<Hitbox>();
		body = GetComponent<Rigidbody2D>();
	}

	void OnEnable()
	{
		previousHits.Clear();
	}

	void Update() {
		List<Hurtbox> hurtboxesHit = hitbox?.GetOverlappingHurtbox();

		foreach (Hurtbox hurtbox in hurtboxesHit) {
			if (hitMultipleTimes || !previousHits.Contains(hurtbox)) {
				hurtbox.knockBack = body.velocity.normalized * forceMultiplier;
				previousHits.Add(hurtbox);
			}
		}
	}
}
