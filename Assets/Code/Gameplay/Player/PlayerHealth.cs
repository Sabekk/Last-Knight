using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Animator))]
public class PlayerHealth : MonoBehaviour, IDamagable {
	Animator animator;

	int maxHealth = 3;
	int health;

	public int Health {
		get { return health; }
		set { health = value; }
	}
	public bool IsAlive => Health > 0;
	public int MaxHealth => maxHealth;

	private void Awake () {
		health = maxHealth;
		animator = GetComponent<Animator> ();
	}

	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.tag == "Trap")
			TakeDamage (1);
	}
	private void OnCollisionEnter2D (Collision2D collision) {
		if (collision.collider.tag == "Trap")
			TakeDamage (1);
	}

	public void TakeDamage (int damage) {
		Health -= damage;
		if (Health <= 0) {
			Kill ();
		} else {
			animator.Play ("GetHit", 1);
			Events.Gameplay.Player.OnGetHit.Invoke (Health, maxHealth);
		}

	}
	public void Kill () {
		animator.SetTrigger ("OnDeath");
		Events.Gameplay.Player.OnDeath.Invoke ();
	}
}
