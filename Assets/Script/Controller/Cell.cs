using UnityEngine;
using System.Collections;

public class Cell :MonoBehaviour
{
	public int Row { get; private set; }
	public int Column { get; private set; }
	
	public Unit Unit { get; private set; }
	
	public bool IsEmpty { get { return Unit == null; } }
	
	public Cell(int row, int col)
	{
		Row = row;
		Column = col;
	}
	
	public void SetItem(Unit item)
	{
		Unit = item;
		Unit.SetCell(this);
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
