using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using ActionExt;

public class Sequence : ActionInterval {
	List<ActionInterval> _mList;

	public Sequence(List<ActionInterval> curList)
	{
		_mList = curList;

		for (int i = 0; i < _mList.Count; ++i) {
			Duration += _mList [i].Duration;
		}
		
	}
		
	public override void excute()
	{
		int actionCount = 0;
		for (int i = 0 ; i < _mList.Count; i ++) 
		{
			if (!_mList[i].IsDone) 
			{
				_mList[i].target = target;
				_mList[i].excute();
				break;
			}
			actionCount++;
		}
		if (actionCount == _mList.Count) 
		{
			IsDone = true;
		}


	}
}
