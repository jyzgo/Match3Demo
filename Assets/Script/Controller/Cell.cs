using UnityEngine;
using System.Collections;

public class Cell :MonoBehaviour
{
	public int Row { get; private set; }
	public int Col { get; private set; }

	public void SwapUnit(Cell curCell)
	{
		Unit temp = Unit;
		Unit = curCell.Unit;
		curCell.Unit = temp;
	}
	public Unit Unit { get; set; }
	
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
	
	public void SetItem(Unit item)
	{
		Unit = item;
		Unit.Cell = this;
	}
	
	public bool IsMatchColor(Cell oth)
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
