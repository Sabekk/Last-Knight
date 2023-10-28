using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {
	public int Health { get; set; }
	public int MaxHealth { get; }
	public bool IsAlive { get; }
	public void TakeDamage (int damage);
	public void Kill ();
}
