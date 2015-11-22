using UnityEngine;
using System.Collections;

using MTUnity.Actions;
using System.Collections.Generic;
using System;

public class LevelCtrl : MonoBehaviour {

	public static LevelCtrl Current = null;
	public enum SwapState
	{
		Default,
		Swiping,
		Updating
	}

	void Awake()
	{
		Current = this;
		_cellT = (GameObject)Resources.Load("Prefabs/Cell", typeof(GameObject));
		_unitT = (GameObject)Resources.Load ("Prefabs/Unit", typeof(GameObject));
		_cellHolder = new GameObject("CellHolder");
	}
	// Use this for initialization
	void Start () {
		Init();
	}

	GameObject _cellT;
	GameObject _unitT;

	public int MaxRow {
		get;
		set;
	}

	public int MaxCol {
		get;
		set;
	}
	public int ActiveMinRow{get;set;}
	public int ActiveMinCol{get;set;}
	public int ActiveMaxRow{get;set;}
	public int ActiveMaxCol{get;set;}

	public bool IsInBorder(int curRow,int curCol)
	{
		if(curRow >= ActiveMinRow && curRow < ActiveMaxRow 
			&& curCol >= ActiveMinCol && curCol <ActiveMaxCol)
		{
			return true;
		}
		return false;
	}
	GameObject _cellHolder;

	public bool IsSameColor (CellCtrl lCell, CellCtrl rCell)
	{
		if (lCell == null || rCell == null)
			return false;
		return lCell.IsMatchColor (rCell);
	}

	int LevelState {
		get;
		set;
	}

	public void CheckSameColorAndAdd (CellCtrl g, int offRow, int offCol, List<CellCtrl> curList)
	{
		int curRow = g.Row;
		int curCol = g.Col;
		
		
		while (true) {
			
			int nextRow = curRow + offRow;
			int nextCol = curCol + offCol;
			
			if(!IsInBorder(nextRow,nextCol))
			{
				return;
			}
			var nextCell = this[nextRow,nextCol];
			curRow = nextRow;
			curCol = nextCol;
			if(g.IsMatchColor(nextCell) && nextCell.Unit.bombType != BombType.Coloring)
			{
				curList.Add(nextCell);
				
			}else
			{
				return;
			}
		}
	}

	SwapState _state = SwapState.Default;
	CellCtrl _activeCell  = null;
	CellCtrl _passiveCell = null;
	void Update()
	{
		switch (_state) {
		case (SwapState.Default):
		{
			if (Input.GetMouseButton (0)) {
				var hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider != null) {
					_activeCell = hit.collider.GetComponent<CellCtrl> ();
					_state = SwapState.Swiping;
				}
			}
			break;
		}
		case(SwapState.Swiping):
		{
			if (Input.GetMouseButton (0)) {
				var hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider != null && _activeCell.gameObject != hit.collider.gameObject) {
					_passiveCell = hit.collider.GetComponent<CellCtrl> ();
					if (!_activeCell.AreVerticalOrHorizontalNeighbors(_passiveCell))
					{
						_state = SwapState.Default;
						Restore();
					}
					else
					{
						_state = SwapState.Updating;

						StartCoroutine(Swaping());
					}
				}
			}
			break;


		}
		}

	}
	

	IEnumerator Swaping()
	{
		var activePosition = new Vector3 (_activeCell.transform.position.x, _activeCell.transform.position.y, _activeCell.Unit.transform.position.z);
		var passivePostion = new Vector3 (_passiveCell.transform.position.x, _passiveCell.transform.position.y, _passiveCell.Unit.transform.position.z);

		// swap model and play action
		_activeCell.Unit.RunActions (new MTMoveTo(Constants.SWAP_TIME,passivePostion));
		_passiveCell.Unit.RunActions (new MTMoveTo(Constants.SWAP_TIME,activePosition));
		_activeCell.SwapUnit (_passiveCell);

		yield return new WaitForSeconds (Constants.SWAP_TIME);

		//check is match or not
		var actReact = MatchHandler.Instance.GetMatchReaction (_activeCell);
		var pasReact = MatchHandler.Instance.GetMatchReaction (_passiveCell);

		if (actReact != null || pasReact != null) {
			//yes, is able to swap
			TryElim(actReact);
			TryElim(pasReact);

		} else {
			//no, unable to swap,swap back them
			_activeCell.Unit.RunActions (new MTMoveTo(Constants.SWAP_TIME,passivePostion));
			_passiveCell.Unit.RunActions (new MTMoveTo(Constants.SWAP_TIME,activePosition));
			_activeCell.SwapUnit (_passiveCell);
			yield return new WaitForSeconds(Constants.SWAP_TIME);
		}
		Restore ();

	}

	void TryElim(MatchReaction curReact)
	{
		if (curReact == null)
			return;

		var elimList = curReact._finalList;
		curReact._curCell.Elim ();
		for (int i = 0; i <elimList.Count; ++i) {
			var curCell = elimList[i];
			curCell.Elim();
		}

	}

	void Restore()
	{
		_state = SwapState.Default;
		_activeCell = null;
		_passiveCell = null;
	}

	void UpdateActiveArea ()
	{
	}

	public CellCtrl this [int row, int col] {
		get {
			if(row < 0 || col < 0)
				return null;
			if(row >= _rowList.Count )
				return null;
			var curColList = _rowList[row];
			if(curColList != null && col < curColList.Count)
				return curColList[col];
			return null;
		}
	}

	List<List<CellCtrl>> _rowList;


	void InitCells()
	{
		_rowList = new List<List<CellCtrl>> ();
		for (int row = 0; row <MaxRow; row ++) {
			var colList = new List<CellCtrl>();
			_rowList.Add(colList);
			for(int col = 0 ; col < MaxCol ; col ++)
			{

				GameObject cell = (GameObject)Instantiate(_cellT,new Vector3(col,row,Zorders.CellZorder),Quaternion.identity);
				var curCellSc = cell.gameObject.GetComponent<CellCtrl>();
				curCellSc.Init(row,col);
				colList.Add(curCellSc);
				curCellSc.SetDir(0,1); 


				if(row == MaxRow - 1)
				{
					curCellSc.isGenCell = true;
				}

				cell.transform.SetParent(_cellHolder.transform);
				if((row + col +1) % 2 == 0){
					cell.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
				}

				GameObject unit = (GameObject)Instantiate(_unitT,new Vector3(col,row,Zorders.UnitZorder),Quaternion.identity);
				var curUnitSc = unit.GetComponent<UnitCtrl>();
				curUnitSc.Init(GetUnitData(row,col));
				curUnitSc.Cell = curCellSc;
				curCellSc.Unit = curUnitSc;
			}
		}
	}

	public bool CheckDrop()
	{
		bool isDrop = false;
		for (int row = 0; row <_rowList.Count; row ++) 
		{
			var colList = _rowList[row];
			for(int col = 0 ; col < colList.Count ; col ++)
			{
				
			}
		}
		return isDrop;
	}

	UnitData GetUnitData(int row, int col)
	{
		var curColor = (UnitColor)UnityEngine.Random.Range (1, System.Enum.GetValues (typeof(UnitColor)).Length-1);
		var curType = UnitType.Brick;// (UnitType)Random.Range (1, System.Enum.GetValues (typeof(UnitType)).Length);
		var curBombType = BombType.None;

		UnitData curData = new UnitData (curColor, curType, curBombType);
		return curData;
	}
	
	void Init()
	{
		MaxRow = Constants.MAX_ROWS;
		MaxCol = Constants.MAX_COLS;

		ActiveMinRow = 0;
		ActiveMaxRow = MaxRow;
		ActiveMinCol = 0;
		ActiveMaxCol = MaxCol;


		
		LevelState = 0;

		InitCells ();

		UpdateActiveArea();


	}

}
