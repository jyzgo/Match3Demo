using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BombCmd{
	None =0,
	Elim,
	Dye
}

public struct BombInfo{
	public float _delayTime;
	public int _row;
	public int _col;
	public BombCmd _bombCmd;

	public UnitType _formType;
	public UnitColor _formColor;
	public int _subType ;
	public Cell _triggerModel;
	public BombInfo(Cell triggerModel, float delay,int row,int col,BombCmd bombCmd = BombCmd.None,UnitType curType = UnitType.None,int curSubType = 0,UnitColor curColor = UnitColor.None)
	{
		_triggerModel = triggerModel;
		_row = row;
		_col = col;
		_delayTime = delay;
		_bombCmd = bombCmd;


		_formType = curType;
		_subType = curSubType;
		_formColor = curColor;
	}

}

public abstract class BombResult  {

	protected List<BombInfo> _elimInfoList;
	public List<BombInfo> elimInfoList{
		get{return _elimInfoList ;}
	}

	protected Cell _triggerModel;


	protected int _curRow;
	protected int _curCol;

	protected int _maxRow;
	protected int _minRow;

	protected int _maxCol;
	protected int _minCol;

	public BombResult(Cell curModel)
	{
		_triggerModel = curModel;

		_elimInfoList = new List<BombInfo>();

		_minCol = LevelController.Current.ActiveMinCol;
		_maxCol = LevelController.Current.ActiveMaxRow;

		_minRow = LevelController.Current.ActiveMinRow;
		_maxRow = LevelController.Current.ActiveMaxRow;

		 _curRow = _triggerModel.Row;
		 _curCol = _triggerModel.Col;
		 
		CaulcElimList ();
	}

	protected virtual void CaulcElimList(){}

	protected void BombCellsWithDir(int offsetRow,int offsetCol,float delayTime = 0f,int distance = 100) 
	{
		int curRow = _curRow + offsetRow;
		int curCol = _curCol + offsetCol;
		 
		int curDistance = 0;
		while (IsInBorder(curRow,curCol) && curDistance < distance) 
		{

			BombInfo curInfo = new BombInfo(_triggerModel,delayTime *curDistance,curRow,curCol,BombCmd.Elim);
			_elimInfoList.Add(curInfo);
			curRow += offsetRow;
			curCol += offsetCol;
			curDistance +=1;
			
		}
	}

	bool IsInBorder(int curRow,int curCol)
	{
		return LevelController.Current.IsInBorder (curRow, curCol);
	}

}

public  abstract class FishResult : BombResult {
	
	public FishResult(Cell curModel):base(curModel)
	{}

	protected virtual void CaulcElimList()
	{}

	protected List<Cell> _mvpCellList= new List<Cell>();
	protected void FindMvpCells()
	{
		var curGrid = LevelController.Current;
		for (int curRow = _minRow ; curRow < _maxRow; ++curRow) 
		{
			for (int curCol = _minCol; curCol < _maxCol; ++curCol) 
			{
				Cell curCell = curGrid[curRow,curCol];
				if (curCell!= null && curCell.IsBombable) 
				{
					_mvpCellList.Add(curCell);
				}	
			}
		}
		
		if (_mvpCellList.Count > 0 ) 
		{
			_mvpCellList.Sort();
			
		}
	}
}
