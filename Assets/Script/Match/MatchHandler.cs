using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// public enum BombType
// {
// 	// None = 0,
// 	// Horizontal,
// 	// Vertical,
// 	// Square,
// 	// Color,
// 	// Dye,
// 	// Cross
// }
using System;
using MTUnity;

public enum ReactionType
{
	None = 0,
	ElimType,
	GenElimentType,

}



public class MatchHandler :Singleton<MatchHandler>   {

	public CellCtrl _curCell = null;
	
		 
	public List<CellCtrl> _leftList  = new List<CellCtrl>();
	public List<CellCtrl> _rightList = new List<CellCtrl>();
	public List<CellCtrl> _upList    = new List<CellCtrl>();
	public List<CellCtrl> _downList  = new List<CellCtrl>();

	public bool _IsMatchLeftUp = false;
	public bool _IsMatchUpRight = false;
	public bool _IsMatchRightDown = false;
	public bool _IsMatchDownLeft = false;

	public CellCtrl _leftUpCell = null;
	public CellCtrl _upRightCell = null;
	public CellCtrl _rightDownCell = null;
	public CellCtrl _downLeftCell = null;

	public MatchReaction GetMatchReaction(CellCtrl curCell)
	{
		Reset();
		_curCell = curCell;
		CalculateMatch ();
		return CheckReactionType ();
	}
	
	void Reset()
	{
		_curCell = null;
		_leftList .Clear(); 
		_rightList.Clear(); 
		_upList   .Clear(); 
		_downList .Clear(); 
		
		_IsMatchLeftUp = false;
		_IsMatchUpRight = false;
		_IsMatchRightDown = false;
		_IsMatchDownLeft = false;
		
		_leftUpCell = null;
		_upRightCell = null;
		_rightDownCell = null;
		_downLeftCell = null;
		
		
	}

	void CalculateMatch()
	{
		var curLv = LevelCtrl.Current;
	
		curLv.CheckSameColorAndAdd(_curCell,0,-1,_leftList);
		curLv.CheckSameColorAndAdd(_curCell,0,1,_rightList);
		curLv.CheckSameColorAndAdd(_curCell,1,0,_upList);
		curLv.CheckSameColorAndAdd(_curCell,-1,0,_downList);

		var curRow = _curCell.Row;
		var curCol = _curCell.Col;

		_leftUpCell = curLv[curRow + 1,curCol - 1];
		_upRightCell = curLv[curRow + 1,curCol + 1];
		_rightDownCell = curLv[curRow -1,curCol + 1];
		_downLeftCell = curLv[curRow -1,curCol - 1];

		_IsMatchLeftUp    = curLv.IsSameNum(_curCell,_leftUpCell);
		_IsMatchUpRight   = curLv.IsSameNum(_curCell,_upRightCell);
		_IsMatchRightDown = curLv.IsSameNum(_curCell,_rightDownCell);
		_IsMatchDownLeft  = curLv.IsSameNum(_curCell,_downLeftCell);

	}

	MatchReaction CheckReactionType()
	{
		var bombConfList = GameConfig.Instance.BombConfList;
		for (int i = 0; i < bombConfList.Count; ++i) {
			var curReaction = CreateReaction (bombConfList [i]);
			if (curReaction != null && curReaction.ReactionType != ReactionType.None) {
				return curReaction;
			}
		}
		return null;
	}

	public MatchReaction CreateReaction(string curStr)
	{


		switch (curStr) 
		{
		case "D":
			return new DyeReaction (this);

		case "C":
			return new ColorReaction (this);
		case "F":
			return new FishReaction (this);

		case "S":
			return new SquareReaction (this);

		case "H":
			return new HorizonReaction (this);
		case "V":
			return new VerticalReaction (this);

		case "NV":
			return new HorizElimReaction (this);

		case "NH":
			return new VertiElimReaction (this);

		}
		return null;

			
	}




	public CellCtrl CurCell {
		get { 
			return _curCell;
		}
	}

}


