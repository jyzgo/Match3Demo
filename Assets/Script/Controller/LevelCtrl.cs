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
        var mat = MatchHandler.Instance;
        var conf = GameConfig.Instance;
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
    

	public bool IsSameNum (CellCtrl lCell, CellCtrl rCell)
	{
		if (lCell == null || rCell == null)
			return false;
		return lCell.IsSameNum (rCell);
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
			if(g.IsSameNum(nextCell) && nextCell.Unit.bombType != BombType.Coloring)
			{
				curList.Add(nextCell);
				
			}else
			{
				return;
			}
		}
	}


    bool Droping()
	{
        bool isAnyCellDroping = false;
        for(int row = 0; row < _rowList.Count; row++)
        {
            var colList = _rowList[row];
            for(int col  = 0; col <colList.Count; col++)
            {
                var curCell = colList[col];
               if(curCell.Droping())
                {
                    isAnyCellDroping = true;
                }
            }

        }

        return isAnyCellDroping;

	}


	SwapState _state = SwapState.Default;
	CellCtrl _activeCell  = null;
    float _lastPressTime = 0f;
    const float PRESS_INTERVAL = 0.3f;
	void Update()
	{
        if(Droping())
        {
            return;
        }

		switch (_state)
        {
            case (SwapState.Default):
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        if (hit.collider != null)
                        {
                            _activeCell = hit.collider.GetComponent<CellCtrl>();
                            _state = SwapState.Swiping;
                            _lastPressTime = Time.time;
                        }
                    }
                    break;
                }
            case (SwapState.Swiping):
                {
                    if (_lastPressTime + PRESS_INTERVAL < Time.time)
                    {
                        _state = SwapState.Default;
                        _activeCell = null;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        if (hit.collider != null && _activeCell!=null && _activeCell.gameObject == hit.collider.gameObject)
                        {
                            Debug.Log("double click");
                            _activeCell.Elim();
                        }
                    }
                    break;


                }
            case (SwapState.Updating):
                {
                    Droping();
                    break;
                }
        }

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
       // _state = SwapState.Updating;
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
