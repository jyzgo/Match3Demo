using UnityEngine;
using System.Collections;
using System;

public class LevelConfig  {
	
	public int MaxRow{ set; get;}
	public int MaxCol{ set; get;}

	public int ActiveRow{ set; get;}
	public int ActiveCol{ set; get;}

	public LevelConfig (JSONObject json)
	{
		throw new NotImplementedException ();
	}

	public void InitLevelConfig(int lvId)
	{


		UpdateBySubId (0);

	}

	public int id
	{ set; get;}

	public bool UpdateBySubId(int subId)
	{
		MaxRow = 9;
		MaxCol = 9;
		
		ActiveRow = 9;
		ActiveCol = 9;
		return true;
	}



}
