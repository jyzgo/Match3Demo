using UnityEngine;
using System.Collections;

public struct UnitData
{
	public 	UnitColor color;
	public 	UnitType type;
	public 	BombType bombType;

	public UnitData (UnitColor color,
	UnitType type,
	BombType bombType)
	{
		this.color = color;
		this.type = type;
		this.bombType = bombType;
	}
}

public class Unit : MonoBehaviour
{


	UnitType _unitType;
	public UnitType unitType
	{
		get{return _unitType;}
	}

	BombType _bombType;
	public BombType bombType
	{
		get{return _bombType;}
	}

	UnitColor _unitColor;
	public UnitColor unitColor
	{
		get{return _unitColor;}
	}
	
	public Cell Cell { get; set; }
	
	public int Row{
		get{return Cell.Row;}
	}

	public int Col{
		get{return Cell.Col;}
	}
	
	public bool IsEqual (Unit unit)
	{
		return unit != null && unit.unitType == unitType;
	}

	public void Init (UnitData curData)
	{
		_unitType = curData.type;
		_bombType = curData.bombType;
		_unitColor = curData.color;
		UpdateCell ();
	}


	public void UpdateCell(float delayTime = 0f)
	{
		StartCoroutine(DoUpdateCell(delayTime));

	}


	IEnumerator DoUpdateCell(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		string spritePath = _unitType.ToString() +_bombType.ToString()+ _unitColor.ToString();

		Sprite newSprite = Resources.Load("Sprite/Cells/"+spritePath,typeof(Sprite)) as Sprite;
		GetComponent<SpriteRenderer>().sprite = newSprite;

	}
}
