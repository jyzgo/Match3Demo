using UnityEngine;
using System.Collections;

using MTUnity.Actions;

public class LevelController : MonoBehaviour {

	public enum SwapState
	{
		Default,
		Swiping,
		Updating
	}

	void Awake()
	{
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

	int LevelMaxRow {
		get;
		set;
	}

	int LevelMaxCol {
		get;
		set;
	}

	GameObject _cellHolder;

	int LevelState {
		get;
		set;
	}

	SwapState _state = SwapState.Default;
	Cell _activeCell  = null;
	Cell _passiveCell = null;
	void Update()
	{
		switch (_state) {
		case (SwapState.Default):
		{
			if (Input.GetMouseButton (0)) {
				var hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider != null) {
					_activeCell = hit.collider.GetComponent<Cell> ();
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
					_passiveCell = hit.collider.GetComponent<Cell> ();
					if (!_activeCell.AreVerticalOrHorizontalNeighbors(_passiveCell))
					{
						_state = SwapState.Default;
						Restore();
					}
					else
					{
						_state = SwapState.Updating;

						Swaping();
					}
				}
			}
			break;


		}
		}

	}


	void Swaping()
	{
		var activeUnit = _activeCell.Unit;
		var passiveUnit = _passiveCell.Unit;

		activeUnit.RunActions (MTMoveTo.Create (Constants.SWAP_TIME, passiveUnit));
		passiveUnit.RunActions (MTMoveTo.Create (Constants.SWAP_TIME, activeUnit));
		StartCoroutine (Restore ());
	}

	IEnumerator Restore()
	{
		yield return new WaitForSeconds (Constants.SWAP_TIME);
		_state = SwapState.Default;
		_activeCell.SwapUnit (_passiveCell);
		_activeCell = null;
		_passiveCell = null;
	}

	void UpdateActiveArea ()
	{
	}

	void InitCells()
	{
		for (int row = 0; row <LevelMaxRow; row ++) {
			for(int col = 0 ; col < LevelMaxCol ; col ++)
			{
				GameObject cell = (GameObject)Instantiate(_cellT,new Vector3(col,row,Zorders.CellZorder),Quaternion.identity);
				var curCellSc = cell.gameObject.GetComponent<Cell>();
				curCellSc.Init(row,col);

				cell.transform.SetParent(_cellHolder.transform);
				if((row + col +1) % 2 == 0){
					cell.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
				}

				GameObject unit = (GameObject)Instantiate(_unitT,new Vector3(col,row,Zorders.UnitZorder),Quaternion.identity);
				var curUnitSc = unit.GetComponent<Unit>();
				curUnitSc.Init(GetUnitData(row,col));
				curUnitSc.Cell = curCellSc;
				curCellSc.Unit = curUnitSc;
			}
		}
	}

	UnitData GetUnitData(int row, int col)
	{
		var curColor = (UnitColor)Random.Range (1, System.Enum.GetValues (typeof(UnitColor)).Length-1);
		var curType = UnitType.Brick;// (UnitType)Random.Range (1, System.Enum.GetValues (typeof(UnitType)).Length);
		var curBombType = BombType.None;

		UnitData curData = new UnitData (curColor, curType, curBombType);
		return curData;
	}
	
	void Init()
	{
		LevelMaxRow = Constants.MAX_ROWS;
		LevelMaxCol = Constants.MAX_COLS;


		
		LevelState = 0;

		InitCells ();

		UpdateActiveArea();


	}

}
