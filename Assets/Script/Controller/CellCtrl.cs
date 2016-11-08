using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CellCtrl :MonoBehaviour
{

	public int Row { get; private set; }
	public int Col { get; private set; }


	public int DirRow { get; private set;}
	public int DirCol { get; private set;}

	public int SideDirRow { get; private set;}
	public int SideDirCol { get; private set;}

	public void SetDir(int row,int col)
	{
		Debug.Assert((Mathf.Abs (row) + Mathf.Abs(col)) != 0,"Shouldn't all be zero");
		DirRow = row;
		DirCol = col;

	}

	public bool isGenCell = false;

	public bool IsDropHere()
	{
				
		if(isGenCell)
		{
			return true;
		}
		//get up steam cell, check if them stable

		var preCellRow = Row + DirRow;
		var preCellCol = Col + DirCol;
		var mainDrop = IsCellDropable (preCellRow, preCellCol);

		if (mainDrop == true) {
			return true;
		}

		int sideRow1 = Row;
		int sideCol1 = Col;

		int sideRow2 = Row;
		int sideCol2 = Col;
		if (DirRow == 0) {
			sideRow1 += 1;
			sideRow2 += -1;
		} else if (DirCol == 0) {
			sideCol1 += 1;
			sideCol2 += -1;
		}else
		{
			Debug.Assert(true,"Shouldn't happened");
		}

		bool isSideStable = true;
		if (IsCellDropable (sideRow1, sideCol1)) {
			SideDirRow = sideRow1;
			SideDirCol = sideCol1;
		} else if (IsCellDropable (sideRow2, sideCol2)) 
		{
			SideDirRow = sideRow2;
			SideDirCol = sideCol2;
		} else {
			isSideStable = false;
		}

		return isSideStable;
	}

	bool IsCellDropable(int row,int col)
	{
		bool curCellExist = LevelCtrl.Current.IsInBorder (row, col);
		bool isDropAble = false;
		if (curCellExist) {
			isDropAble = LevelCtrl.Current[row,col].IsDropHere();
		}
		return isDropAble;
	}
	


	public void SwapUnit(CellCtrl curCell)
	{
		UnitCtrl temp = Unit;
		Unit = curCell.Unit;
		curCell.Unit = temp;
	}
	public UnitCtrl Unit { get; set; }
	
	public bool IsEmpty { get { return Unit == null; } }

	public bool IsBombable {
		get
		{
			if(Unit == null)
				return false;
			return true;
		}

	}

	public void Elim ()
	{
		if (Unit != null)
			Unit.Elim ();
	}

	public void ElimDone ()
	{
		Debug.Log("Elim done");
		LevelCtrl.Current.CheckDrop ();
	}

	public bool MatchEliminateColor (UnitColor _elimColor)
	{
		if (!IsBombable)
			return false;
		if (Unit.unitNum == _elimColor)
			return true;
		return false;
	}

    internal bool Droping()
    {
        return false;
    }

    public bool IsEliminateable {
		get
		{
			return IsBombable;
		}

	}
	
	public void Init(int row, int col)
	{
		Row = row;
		Col = col;
	}
	
	public void SetItem(UnitCtrl item)
	{
		Unit = item;
		Unit.Cell = this;
	}
	
	public bool IsSameNum(CellCtrl oth)
	{
		if (IsEmpty || oth.IsEmpty)
			return false;
		if (Unit.unitNum == oth.Unit.unitNum)
			return true;
		return false;
	}
	
	public void Clear()
	{
		Unit = null;
	}
}
