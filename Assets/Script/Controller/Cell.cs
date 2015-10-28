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
	
	public bool IsMatched(Cell cell)
	{
		return !IsEmpty && !cell.IsEmpty && Unit.IsEqual(cell.Unit);
	}
	
	public void Clear()
	{
		Unit = null;
	}
}
