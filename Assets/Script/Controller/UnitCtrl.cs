using UnityEngine;
using System.Collections;
using MTUnity.Actions;

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

public enum UnitMoveState
{
	Default,
	Droping
}

public enum UnitElimState
{
	Default,
	Eliming
}

public class UnitCtrl : MonoBehaviour
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

	public UnitMoveState _moveState{ set; get; }
	public UnitElimState _elimState{ set; get;}

	public void Elim ()
	{
		if (_elimState == UnitElimState.Eliming)
			return;
		_elimState = UnitElimState.Eliming;
		StartCoroutine (Eliming ());

	}

	IEnumerator Eliming()
	{
		var duration = Constants.UNIT_ELIM_TIME;
		var shink = new MTScaleTo (duration, 0.1f);
		var fade = new MTFadeOut (duration);
		var spawn = new MTSpawn (shink, fade);
		gameObject.RunActions (spawn, new MTDestroy ());
		yield return new WaitForSeconds (duration);
		Cell.ElimDone ();
	}
	
	public CellCtrl Cell { get; set; }
	
	public int Row{
		get{return Cell.Row;}
	}

	public int Col{
		get{return Cell.Col;}
	}
	
	public bool IsEqual (UnitCtrl unit)
	{
		return unit != null && unit.unitType == unitType;
	}

	public void Init (UnitData curData)
	{
		_unitType = curData.type;
		_bombType = curData.bombType;
		_unitColor = curData.color;
		_moveState = UnitMoveState.Default;
		_elimState = UnitElimState.Default; 
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
