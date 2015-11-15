using UnityEngine;
using System.Collections;

public static class MExtension
{
//	public static void TweenPosition(this Transform transform, float duration, Vector3 position)
//	{
//		Tweener tweener = transform.GetComponent<Tweener>() ?? transform.gameObject.AddComponent<Tweener>();
//		tweener.TweenPosition(duration, position);
//	}
	
	//Checks if an item is next to another one, either horizontally or vertically
	public static bool AreVerticalOrHorizontalNeighbors( this CellCtrl item1, CellCtrl item2)
	{
		return 
			(item1.Col == item2.Col || item1.Row == item2.Row) && 
				Mathf.Abs(item1.Col - item2.Col) <= 1 && 
				Mathf.Abs(item1.Row - item2.Row) <= 1;
	}


}
