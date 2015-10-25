using UnityEngine;
using System.Collections;

public class LevelConfig  {
	
	public int MaxRow{ set; get;}
	public int MaxCol{ set; get;}

	public int ActiveRow{ set; get;}
	public int ActiveCol{ set; get;}


	public void InitLevelConfig(int lvId)
	{


		UpdateBySubId (0);

	}

	public bool UpdateBySubId(int subId)
	{
		MaxRow = 9;
		MaxCol = 9;
		
		ActiveRow = 9;
		ActiveCol = 9;
		return true;
	}



}
