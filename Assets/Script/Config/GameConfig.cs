using UnityEngine;
using System.Collections.Generic;
using MTUnity;
using System;

public enum TimeConf{
	hvOffsetDelay = 0,// 水平横竖炸弹延迟,每一个增加一个delay
	squareDelay,//方炸弹延迟
	colorDelay,//花炸弹延迟
	colorOffsetDelay,//花炸弹offset 
	fishDelay,//鱼游动时间
	dyeDelay,//染色炸弹引爆延迟
	dyeOffsetDelay,//多个染色的时候延迟染色
	combinTriggerDelay,//水平横竖方炸弹触发延迟五花炸弹和横竖合成后 炸弹依次引爆的延迟时间
	specialDelay,//特殊炸弹合成间隔 例如 彩色炸弹和横竖炸弹合成，单个合成有一定的延迟时间和顺序，然后依次把对应炸弹变成特殊炸弹
}

public enum CellWeightConf{
	AdditionWeight=0,
	Obstacle,
	RequireWeight,
	BombWeight,
	FishWeight,
	BottleWeight,
	CookieWeight,
	ChocolateWeight,
	GiftWeight,
	BearBobbleDirectionWeight
}

public enum NumConf{
	activeFishNum = 0,
}

public class GameConfig : Singleton<GameConfig>
{
	
	public const int DEFAULT_ZONE = 1;
	public const int MAX_ZONE = 2;
	public const int DEFAULT_LEVEL = 1;
	public const int MAX_LEVEL = 16;
	
	public const int MAX_VISIBLE_ROWS = 9;
	public const int MAX_VISIBLE_COLS = 9;
	
	public const float SwapTime = 0.15f;
	
	private Dictionary<int, LevelConfig> _levelsMap;
	Dictionary<string,float> _timeMap;
	Dictionary<string,int> _numMap;
	Dictionary<string,int> _cellWeight;

	
	protected GameConfig ()
	{
		_levelsMap = new Dictionary<int, LevelConfig> ();
		_timeMap   = new Dictionary<string,float>();
		_numMap = new Dictionary<string,int> ();
		_cellWeight = new Dictionary<string,int > ();
		
	}
	
	public int GetNum(NumConf curConf)
	{
		int rtn = 0;
		_numMap.TryGetValue (curConf.ToString (), out rtn);
		return rtn;
	}
	
	public float GetTime(TimeConf curConf)
	{
		float rtn = 0f;
		_timeMap.TryGetValue (curConf.ToString (), out rtn);
		return rtn;
	}
	
	public int GetWeight(CellWeightConf curConf)
	{
		int rtn = 0;
		_cellWeight.TryGetValue(curConf.ToString(),out rtn);
		return rtn;
	}
	
	public void ParseConfig (JSONObject data)
	{
		var bombConf = data.GetField("bombConf").list;
		for (int i  = 0; i <bombConf.Count; ++i) 
		{
			string curString = bombConf[i].ToString();
			_bombConfList.Add(curString);
		}
		
		var timeConf = data.GetField ("timeConf");
		
		for (int i = 0; i < Enum.GetValues(typeof(TimeConf)).Length; ++i) 
		{
			var curKeyStr = ((TimeConf)i).ToString();
			float delayTime = timeConf.GetField(curKeyStr).f;
			_timeMap.Add(curKeyStr,delayTime);
		}
		
		var numConf = data.GetField("numConf");
		for (int i = 0; i <Enum.GetValues(typeof(NumConf)).Length; ++i) {
			var curKeyStr = ((NumConf)i).ToString();
			int num = (int)numConf.GetField(curKeyStr).n;
			_numMap.Add(curKeyStr,num);
		}
		
		var cellWeightConf = data.GetField ("cellWeightConf");
		for (int i = 0; i <Enum.GetValues(typeof(CellWeightConf)).Length; ++i) {
			var curKeyStr = ((CellWeightConf)i).ToString();
			int num = (int)cellWeightConf.GetField(curKeyStr).n;
			_cellWeight.Add(curKeyStr,num);
		}
		
		
		
	}
	
	
	
	List<string> _bombConfList = new List<string>();
	public List<string> BombConfList
	{
		get{
			return _bombConfList;
		}
	}
	
	public void ParseLevels (JSONObject data)
	{
		List<JSONObject> levels = data.GetField ("levels").list;
		foreach (JSONObject json in levels) {
			LevelConfig conf = new LevelConfig (json);
			AddLevel (conf.id, conf);
		}
	}
	
	public LevelConfig GetLevel (int level)
	{
		if (_levelsMap.ContainsKey (level)) {
			return _levelsMap [level];
		}
		return null;
	}
	
	public void AddLevel (int level, LevelConfig conf)
	{
		_levelsMap.Add (level, conf);
	}
	
	public void RemoveLevel (int level)
	{
		_levelsMap.Remove (level);
	}
	
}