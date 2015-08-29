using UnityEngine;
using System.Collections;


public static class ActionExtension  {

	public static CLAction runAction(this MonoBehaviour target,CLAction action)
	{
		ActionManager.instance.addAction(action,target,false);
		return action;
	}

	public static CLAction stopAction(this MonoBehaviour target,CLAction action)
	{
		ActionManager.instance.removeAction(target,action);
		return action;
	}

}

