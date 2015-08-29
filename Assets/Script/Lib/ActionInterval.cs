using UnityEngine;
using System.Collections;

public abstract class ActionInterval : FiniteTimeAction {

	protected float _duration;


	public abstract override void excute ();

}
