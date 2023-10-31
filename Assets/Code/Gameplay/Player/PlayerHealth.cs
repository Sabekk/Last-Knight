using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Animator))]
public class PlayerHealth : MonoBehaviour, IDamagable, ISerializable, ObjectPool.IPoolable {
	Animator animator;

	int maxHealth = 3;
	int health;

	public int Health {
		get { return health; }
		set { health = value; }
	}
	public bool IsAlive => Health > 0;
	public int MaxHealth => maxHealth;

	public ObjectPool.PoolObject Poolable { get; set; }

	private void Awake () {
		animator = GetComponent<Animator> ();
		SerializableMatcher.Instance.AddToCollection (gameObject, this);
	}
	private void OnDestroy () {
		SerializableMatcher.Instance.RemoveFromCollection (gameObject);
	}

	public void SetData (int health, Vector2 startingPosition) {
		this.health = health;
		transform.position = startingPosition;
		transform.SetParent (null);
		transform.localScale = Vector3.one * 3;
		Events.Gameplay.Player.OnGetHit.Invoke (Health);
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
		if (Health <= 0)
			Kill ();
		else
			animator.Play ("GetHit", 1);

		Events.Gameplay.Player.OnGetHit.Invoke (Health);

	}
	public void Kill () {
		animator.SetTrigger ("OnDeath");
		Events.Gameplay.Player.OnDeath.Invoke ();
	}

	public void LoadGame (PlayerSaveData saveData) {

	}

	public void SaveGame (ref PlayerSaveData saveData) {
		saveData.playerLives = Health;
		saveData.playerPosition = transform.position;
	}

	public void AssignPoolable (ObjectPool.PoolObject poolable) {
		Poolable = poolable;
	}
}
