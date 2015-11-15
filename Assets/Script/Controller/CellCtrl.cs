using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellCtrl :MonoBehaviour
{
	public int Row { get; private set; }
	public int Col { get; private set; }

	public int DirRow { get; set; }
	public int DirCol { get; set; }

	bool _isDroping = false;
	public bool IsStable()
	{
		if(_isDroping)
		{
			return false;
		}
		return true;
	}

	public bool isGenCell = false;

	public CellCtrl PreCell = null;

	public List<CellCtrl> AlternteCellList = new List<CellCtrl>();

	public bool TryGetElment()
	{
		//
		if(Unit != null)
		{
			return false;
		}
		return true;

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
	}

	public bool MatchEliminateColor (UnitColor _elimColor)
	{
		if (!IsBombable)
			return false;
		if (Unit.unitColor == _elimColor)
			return true;
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
	
	public bool IsMatchColor(CellCtrl oth)
	{
		if (IsEmpty || oth.IsEmpty)
			return false;
		if (Unit.unitColor == oth.Unit.unitColor)
			return true;
		return false;
	}
	
	public void Clear()
	{
		Unit = null;
	}
}
