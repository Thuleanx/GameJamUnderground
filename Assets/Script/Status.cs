
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
	[SerializeField] int maxHealth;
	public int MaxHealth { get { return maxHealth; } }
	int currentHealth;
	public int CurrentHealth { 
		get { return currentHealth; }
		set {
			currentHealth = Mathf.Clamp(value, 0, maxHealth);
		} }

	void OnEnable() {
		currentHealth = maxHealth;
	}

	public void Damage(int dmg) {
		currentHealth -= dmg;
	}

	void Update() {
		if (currentHealth <= 0 && gameObject.tag == "Player") {
			GameMaster.Instance.Reset();
		}
	}
}
