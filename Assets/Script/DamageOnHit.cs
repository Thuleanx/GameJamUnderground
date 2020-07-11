
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
	Hitbox hitbox;	
	Status status;

	[SerializeField] int damage;
	[SerializeField] bool hitMultipleTimes;
	[SerializeField] int hitHeal = 0;

	HashSet<Hurtbox> previousHits = new HashSet<Hurtbox>();

	void Awake() {
		hitbox = GetComponentInChildren<Hitbox>();
		status = GetComponent<Status>();
	}

	void OnEnable() {
		previousHits.Clear();
	}

	void Update() {
		List<Hurtbox> hurtboxesHit = hitbox?.GetOverlappingHurtbox();

		foreach (Hurtbox hurtbox in hurtboxesHit) if (hurtbox.CanBeHit()) {
			if (hitMultipleTimes || !previousHits.Contains(hurtbox)) {
				hurtbox.RegisterHit(damage);

				if (hurtbox.transform.parent.tag == "Enemy") {
					HealthDisplay.EnemyInstance.Attach(hurtbox.GetComponentInParent<Status>());
				}

				if (status != null && hitHeal > 0) {
					status.CurrentHealth = status.CurrentHealth + hitHeal; 
				}

				previousHits.Add(hurtbox);
			}
		}
	}
}
