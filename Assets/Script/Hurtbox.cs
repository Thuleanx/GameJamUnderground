
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
	// Attached to a status
	BoxCollider2D box;	
	public Collider2D Bounds {
		get { return box; }
	}
	Status status;
	AnimationManager animManager;
	MobAnimationManager mobAnimManger;
	NoticeWhenShot noticeListener;

	[HideInInspector] public Vector2 knockBack;
	[SerializeField] float hurtboxDecayRate = 1f;

	void Awake() {
		box = GetComponent<BoxCollider2D>();
		status = GetComponentInParent<Status>();
		animManager = GetComponentInParent<AnimationManager>();
		mobAnimManger = GetComponentInParent<MobAnimationManager>();
		noticeListener = GetComponentInParent<NoticeWhenShot>();
	}

	public void RegisterHit(int damage) {
		status?.Damage(damage);
		animManager?.TriggerHit();
		noticeListener?.Detection();
		mobAnimManger?.TriggerHit();

		if (status != null && status.CurrentHealth <= 0)
			mobAnimManger?.TriggerDie();
	}

	public bool CanBeHit() {
		return animManager == null ? true : animManager.currentState != AnimationManager.State.Hit;
	}

	void Update() {
		knockBack = new Vector2(
			Mathf.Lerp(knockBack.x, 0, hurtboxDecayRate),
			Mathf.Lerp(knockBack.y, 0, hurtboxDecayRate)
		);

		if (knockBack.sqrMagnitude < 0.001f)
			knockBack = Vector2.zero;
	}
}
