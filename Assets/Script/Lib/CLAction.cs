using UnityEngine;
using System.Collections;

public abstract class CLAction{

	protected bool _isInit = false;


	bool _isDone = false;
	public bool IsDone
	{
		set{

			_isDone = value;
		}
		get{
			return _isDone;
		}
	}

	MonoBehaviour _target;
	public MonoBehaviour target
	{
		get{
			return _target;
		}
		set{
			_target = value;
		}
	}

	public abstract void excute();





}
