using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Firable : MonoBehaviour
{
	// Tweakables
	[SerializeField] GameObject source;
	[SerializeField] string projectileTag;
	// [SerializeField] float fireRatePerSecond;
	[SerializeField] string firingSound;

	// float timeNextFire; // default 0

	[HideInInspector] public bool canShoot = true;

	public bool OnFireDirection(Vector2 dir) {
		Assert.IsNotNull(source);
		Assert.IsNotNull(projectileTag);
		// Assert.IsTrue(fireRatePerSecond > 0);

		if (canShoot) {
			// Fire a projectile and rotate it to face dir
			GameObject projectile = ObjectPool.Instance.Instantiate(
				projectileTag,
				source.transform.position,
				Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x))
			);

			canShoot = false;
			if (firingSound.Length > 0) {
				AudioManager.Instance.Play(firingSound);
			}

			return true;
		}

		return false;
	}

	public void ReloadShot() {
		canShoot = true;
	}

	// Returns whether fire goes off
	public bool OnFire(Vector2 targetInWorldSpace) {
		Vector2 dir = targetInWorldSpace - (Vector2)source.transform.position;
		return OnFireDirection(dir);
	}
}
