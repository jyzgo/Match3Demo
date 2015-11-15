using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HorizontalResult : BombResult {

	public HorizontalResult(CellCtrl curModel):base(curModel)
	{}

	protected override void CaulcElimList ()
	{
		float hzDelay = GameConfig.Instance.GetTime (TimeConf.hvOffsetDelay);
		BombCellsWithDir (-1, 0, hzDelay);
		BombCellsWithDir ( 1, 0, hzDelay);

	}

}


public class VerticalResult : BombResult {

	public VerticalResult(CellCtrl curModel):base(curModel)
	{}

	protected override void CaulcElimList ()
	{
		float hzDelay = GameConfig.Instance.GetTime (TimeConf.hvOffsetDelay);
		BombCellsWithDir ( 0, 1, hzDelay);
		BombCellsWithDir ( 0, -1, hzDelay);
		
	}
}

public class CrossResult : BombResult {

	public CrossResult(CellCtrl curModel):base(curModel)
	{}

	protected override void CaulcElimList ()
	{
		float hzDelay = GameConfig.Instance.GetTime (TimeConf.hvOffsetDelay);
		BombCellsWithDir ( 0, 1,  hzDelay);
		BombCellsWithDir ( 0, -1, hzDelay);
		BombCellsWithDir (-1, 0,  hzDelay);
		BombCellsWithDir ( 1, 0, hzDelay);
		
	}
}

public class SquareResult : BombResult {

	public SquareResult(CellCtrl curModel):base(curModel)
	{}

	protected override void CaulcElimList ()
	{
		float sqDelay = GameConfig.Instance.GetTime (TimeConf.squareDelay);
		BombCellsWithDir(0,-1,sqDelay,1);
		BombCellsWithDir(1,-1,sqDelay,1);
		BombCellsWithDir(1,0,sqDelay,1);
		BombCellsWithDir(1,1,sqDelay,1);
		BombCellsWithDir(0,1,sqDelay,1);
		BombCellsWithDir(-1,1,sqDelay,1);
		BombCellsWithDir(-1,0,sqDelay,1);
		BombCellsWithDir(-1,-1,sqDelay,1);
	}
}

public class FishOneResult:FishResult
{
	public FishOneResult(CellCtrl curModel):base(curModel)
	{
	}

	protected override void CaulcElimList()
	{
		FindMvpCells ();
		if (_mvpCellList.Count > 0) {
			float fishDely = GameConfig.Instance.GetTime(TimeConf.fishDelay);
			var tarRow = _mvpCellList[0].Row;
			var tarCol = _mvpCellList[0].Col;
			var curInfo = new BombInfo(_triggerModel,fishDely,tarRow,tarCol,BombCmd.Elim);
			_elimInfoList.Add (curInfo);
		}
	}
}

public class FishMutipleResult:FishResult
{
	int _fishNum = 0;
	public FishMutipleResult(CellCtrl curModel,int num):base(curModel)
	{
		_fishNum = num;
	}
	
	protected override void CaulcElimList()
	{
		FindMvpCells ();
		if (_mvpCellList.Count > 0) {
			float fishDely = GameConfig.Instance.GetTime(TimeConf.fishDelay);
			float combinTime = GameConfig.Instance.GetTime(TimeConf.combinTriggerDelay);
			for(int i = 0 ;i < _mvpCellList.Count && i < _fishNum;i ++)
			{
				var tarRow = _mvpCellList[i].Row;
				var tarCol = _mvpCellList[i].Col;
				var curInfo = new BombInfo(_triggerModel,fishDely + i * combinTime,tarRow,tarCol,BombCmd.Elim);
				_elimInfoList.Add (curInfo);
			}
		}
	}
}


public class ColorResult : BombResult {

	UnitColor _elimColor;
	public ColorResult(CellCtrl curModel,UnitColor curColor):base(curModel)
	{
		_elimColor = curColor;
	}

	protected override void CaulcElimList ()
	{
		float colorDelay = GameConfig.Instance.GetTime (TimeConf.colorDelay);
		var curLvCtrl = LevelCtrl.Current;
		for (int curRow = _minRow; curRow < _maxRow; curRow++) {
			for(int curCol = _minCol; curCol < _maxCol ; curCol++)
			{
				CellCtrl curCell = curLvCtrl[curRow,curCol];
				if(curCell!= null && curCell != _triggerModel && curCell.IsEliminateable && curCell.MatchEliminateColor(_elimColor))
				{
					var curInfo = new BombInfo(_triggerModel,colorDelay,curRow,curCol,BombCmd.Elim);
					_elimInfoList.Add(curInfo);
				}

			}
		}
	}


}

public class DyeResult : BombResult {

	UnitColor _dyeColor;
	public DyeResult(CellCtrl curModel,UnitColor curColor):base(curModel)
	{
		_dyeColor = curColor;
	}

	protected override void CaulcElimList ()
	{
		float dyeDelay = GameConfig.Instance.GetTime (TimeConf.dyeDelay);
		var dyeList = new List<BombInfo> ();
		var curLvCtrl = LevelCtrl.Current;
		for (int curRow = _minRow; curRow < _maxRow; curRow++) {
			for(int curCol = _minCol; curCol < _maxCol ; curCol++)
			{
				CellCtrl curCell = curLvCtrl[curRow,curCol];
				if(curCell!= null && curCell != _triggerModel && curCell.IsEliminateable && !curCell.MatchEliminateColor(_dyeColor))
				{
					var curInfo = new BombInfo(_triggerModel,dyeDelay,curRow,curCol,BombCmd.Dye);
					curInfo._formColor = _dyeColor;
					dyeList.Add(curInfo);
				}
				
			}
		}

	}
}
