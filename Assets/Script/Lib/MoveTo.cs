using UnityEngine;
using System.Collections;

public class MoveTo : ActionInterval {


	Vector3 _tarPos;
	Vector3 _curPos;

	float _startTime;
	float _speed;
	float _distance;
	public MoveTo(Vector3 tarPos,float d)
	{
		Duration = d;
		_tarPos = tarPos;
		_duration = d;
	}

	public override void excute()
	{
		if (!_isInit) 
		{
			_isInit = true;
			init();
		}

		updatePos();

	}

	void init()
	{
		_startTime = Time.time;
		_curPos = target.transform.position;
		_distance = Vector3.Distance(_curPos,  _tarPos);
		if (_duration != 0) 
		{
			_speed = _distance /_duration;
		}
		
	}

	void updatePos()
	{
		if (_duration == 0) 
		{
			target.transform.position = _tarPos;

		}
		else
		{
			float distCovered = (Time.time - _startTime) * _speed;
	        float fracJourney = distCovered / _distance;
	        target.transform.position = Vector3.Lerp(_curPos, _tarPos, fracJourney);
		}

		if (target.transform.position == _tarPos) 
		{
			IsDone = true;
		}
	}
}
