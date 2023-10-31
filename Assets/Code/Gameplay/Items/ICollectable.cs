using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
	bool Collected { get; }
	public void Initialize ();
	public void Collect ();
}
