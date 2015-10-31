using UnityEngine;
using System.Collections;



public class DyeReaction : MatchReaction {

	public DyeReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
//		Debug.Log ("Dye reaction");
		if ((_verCount >=5  && _horCount >= 2) || (_horCount >= 5 && _verCount >= 2)) {
			_matchReactionType = ReactionType.GenElimentType;
			_elemType = UnitType.Bomb;
			_subType = (int)BombType.Coloring;
			_finalList.AddRange (_upList);
			_finalList.AddRange (_downList);
			_finalList.AddRange (_rightList);
			_finalList.AddRange (_leftList);
		}
	}
}

public class ColorReaction : MatchReaction {

	public ColorReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
//		Debug.Log ("Color reaction");
		if ((_verCount >=5  && _horCount == 1) || (_horCount >= 5 && _verCount == 1)) {
			_matchReactionType = ReactionType.GenElimentType;
			_elemType = UnitType.Bomb;
			_subType = (int)BombType.Color;
			_finalList.AddRange (_upList);
			_finalList.AddRange (_downList);
			_finalList.AddRange (_rightList);
			_finalList.AddRange (_leftList);
		}
	}
}

public class FishReaction : MatchReaction {

	public FishReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
		if (_leftList.Count > 0 && _upList.Count > 0 && _IsMatchLeftUp) {
			//fish candy
//			Debug.Log("fish1 candy");
			_finalList.Add (_leftUpCell);
			SetBombType();

		} else if (_upList.Count > 0 && _rightList.Count > 0 && _IsMatchRightUp) {
			//fish candy
			// Debug.Log("fish2 candy");
			_finalList.Add (_upRightCell);
			SetBombType();

		} else if (_rightList.Count > 0 && _downList.Count > 0 && _IsMatchRightDown) {
			//fish candy
			// Debug.Log("fish3 candy");
			_finalList.Add (_rightDownCell);
			SetBombType();

		} else if (_downList.Count > 0 && _leftList.Count > 0 && _IsMatchLeftDown) {
			//fish candy
			// Debug.Log("fish4 candy");
			_finalList.Add (_downLeftCell);
			SetBombType();

		}
	}

	void SetBombType()
	{
		_matchReactionType = ReactionType.GenElimentType;
		_elemType = UnitType.Bomb;
		_finalList.AddRange (_upList);
		_finalList.AddRange (_downList);
		_finalList.AddRange (_rightList);
		_finalList.AddRange (_leftList);
	}
}

public class SquareReaction : MatchReaction {

	public SquareReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
		// Debug.Log ("Color reaction");
		if (_horCount >= 3 && _horCount < 5 && _verCount >= 3 && _verCount < 5) {
			_matchReactionType = ReactionType.GenElimentType;
			_elemType = UnitType.Bomb;
			_subType = (int)BombType.Square;

			_finalList.AddRange (_upList);
			_finalList.AddRange (_downList);
			_finalList.AddRange (_rightList);
			_finalList.AddRange (_leftList);
		}
	}
}

public class HorizonReaction : MatchReaction {

	public HorizonReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
		// Debug.Log ("Horizon reaction");
		if (_verCount == 4 && _horCount < 3) {
			_matchReactionType = ReactionType.GenElimentType;
			_elemType = UnitType.Bomb;
			_subType = (int)BombType.Horizontal;
			_finalList.AddRange (_upList);
			_finalList.AddRange (_downList);
		}
	}
}

public class VerticalReaction : MatchReaction {

	public VerticalReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
		// Debug.Log ("Vertical reaction");
		if (_horCount == 4 && _verCount < 3) {
			_matchReactionType = ReactionType.GenElimentType;
			_elemType = UnitType.Bomb;
			_subType = (int)BombType.Vertical;
			_finalList.AddRange (_rightList);
			_finalList.AddRange (_leftList);
		}
	}
}

public class VertiElimReaction : MatchReaction {

	public VertiElimReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
		// Debug.Log ("Vertical Elim reaction");
		if (_verCount == 3 && _horCount < 3) {
			_matchReactionType = ReactionType.ElimType;
			_finalList.AddRange (_upList);
			_finalList.AddRange (_downList);
		}
	}
}

public class HorizElimReaction : MatchReaction {

	public HorizElimReaction(MatchHandler curHandler):base(curHandler)
	{}

	public override void CheckReaction ()
	{
		// Debug.Log ("Horizontal Elim reaction");
		if (_verCount < 3 && _horCount == 3) {
			_matchReactionType = ReactionType.ElimType;
			_finalList.AddRange (_rightList);
			_finalList.AddRange (_leftList);
		}
	}
}

