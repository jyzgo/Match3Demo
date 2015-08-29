using UnityEngine;
using System.Collections;

public abstract class FiniteTimeAction : CLAction {

	public float Duration {
		set;
		get;
	}

	public abstract override void excute();
}
