using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

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

	void UpdateActiveArea ()
	{
	}

	void InitCells()
	{
		for (int row = 0; row <LevelMaxRow; row ++) {
			for(int col = 0 ; col < LevelMaxCol ; col ++)
			{
				GameObject cell = (GameObject)Instantiate(_cellT,new Vector3(col,row,Zorders.CellZorder),Quaternion.identity);

				cell.transform.SetParent(_cellHolder.transform);
				if((row + col +1) % 2 == 0){
					cell.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
				}

				GameObject unit = (GameObject)Instantiate(_unitT,new Vector3(col,row,Zorders.UnitZorder),Quaternion.identity);
			}
		}
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
