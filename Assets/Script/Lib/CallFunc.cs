using UnityEngine;
using System.Collections;
using System;

public class CallFunc : ActionInterval {

	Action _curAction;
	public CallFunc(Action curAction)
	{
		Duration = 0f;
		_curAction = curAction;
	}

	public override void excute()
	{
		if (!_isInit) 
		{
			_isInit = true;
			init();
		}


		_curAction ();
		IsDone = true;
	}

	void init()
	{
	}


}
