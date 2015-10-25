using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
	public string type;
	
	public Cell Cell { get; private set; }
	
	public void SetCell(Cell cell)
	{
		Cell = cell;
	}
	
	public bool IsEqual(Unit unit)
	{
		return unit != null && unit.type == type;
	}
}
