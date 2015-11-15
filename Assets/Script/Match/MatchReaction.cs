using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MatchReaction {


	protected int _horCount = 0;
	protected int _verCount = 0;

	protected bool _IsMatchLeftUp = false;
	protected bool _IsMatchRightUp = false;
	protected bool _IsMatchRightDown = false;
	protected bool _IsMatchLeftDown = false;

	public List<CellCtrl> _leftList  = new List<CellCtrl>();
	public List<CellCtrl> _rightList = new List<CellCtrl>();
	public List<CellCtrl> _upList    = new List<CellCtrl>();
	public List<CellCtrl> _downList  = new List<CellCtrl>();

	public List<CellCtrl> _finalList = new List<CellCtrl> ();

	public CellCtrl _curCell = null;
	public CellCtrl _leftUpCell = null;
	public CellCtrl _upRightCell = null;
	public CellCtrl _rightDownCell = null;
	public CellCtrl _downLeftCell = null;

	public MatchReaction(MatchHandler curHandler)
	{

		_leftList = curHandler._leftList;
		_rightList = curHandler._rightList;
		_upList = curHandler._upList;
		_downList = curHandler._downList;

		_curCell = curHandler._curCell;
		_leftUpCell = curHandler._leftUpCell;
		_upRightCell = curHandler._upRightCell;
		_rightDownCell = curHandler._rightDownCell;
		_downLeftCell = curHandler._downLeftCell;


		_horCount  = _leftList.Count + _rightList.Count + 1;
		_verCount  = _upList.Count + _downList.Count + 1;

		_IsMatchLeftUp    = curHandler._IsMatchLeftUp;
		_IsMatchRightUp   = curHandler._IsMatchUpRight;
		_IsMatchRightDown = curHandler._IsMatchRightDown;
		_IsMatchLeftDown  = curHandler._IsMatchDownLeft;

		CheckReaction ();
	}

	public abstract void CheckReaction();
	

	protected ReactionType _matchReactionType = ReactionType.None;
	public ReactionType ReactionType {
		get { 
			return _matchReactionType;
		}

		
	}


	protected UnitType _elemType = UnitType.None;
	public UnitType GenElemType {
		get {
			return _elemType;
		}

	}

	protected int _subType = -1;
	public int SubType{
		get{ 
			return _subType;
		}
	}



}
