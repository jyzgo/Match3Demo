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

	public Cell _curCell = null;
	
		 
	public List<Cell> _leftList  = new List<Cell>();
	public List<Cell> _rightList = new List<Cell>();
	public List<Cell> _upList    = new List<Cell>();
	public List<Cell> _downList  = new List<Cell>();

	public bool _IsMatchLeftUp = false;
	public bool _IsMatchUpRight = false;
	public bool _IsMatchRightDown = false;
	public bool _IsMatchDownLeft = false;

	public Cell _leftUpCell = null;
	public Cell _upRightCell = null;
	public Cell _rightDownCell = null;
	public Cell _downLeftCell = null;

	public MatchReaction GetMatchReaction(Cell curCell)
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
		var curLv = LevelController.Current;
	
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

		_IsMatchLeftUp    = curLv.IsSameColor(_curCell,_leftUpCell);
		_IsMatchUpRight   = curLv.IsSameColor(_curCell,_upRightCell);
		_IsMatchRightDown = curLv.IsSameColor(_curCell,_rightDownCell);
		_IsMatchDownLeft  = curLv.IsSameColor(_curCell,_downLeftCell);

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




	public Cell CurCell {
		get { 
			return _curCell;
		}
	}

}


