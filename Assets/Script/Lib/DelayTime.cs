using UnityEngine;
using System.Collections;

public class DelayTime : ActionInterval {

	float _delayTime;
	public DelayTime(float delay)
	{
		Duration = delay;
		_delayTime = delay;
	}

	public override void excute()
	{
		if (!_isInit) 
		{
			_isInit = true;
			init();
		}
		updateTime ();

	}

	float _startTime = 0f;
	void init()
	{
		_startTime = Time.time;
	}

	void updateTime()
	{
		if (Time.time >= _startTime + _delayTime) {
			IsDone = true;
		}
	}

}
