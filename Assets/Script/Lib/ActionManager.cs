using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ActionManager : MonoBehaviour {


	Dictionary<MonoBehaviour,HashSet<CLAction>> _targetDict;


	public void addAction(CLAction action,MonoBehaviour target,bool pause)
	{
		if (!_targetDict.ContainsKey(target)) 
		{
			_targetDict[target] = new HashSet<CLAction>();
		}
		action.target = target;
		_targetDict[target].Add(action);
	}

	public void removeAction(MonoBehaviour target, CLAction action)
	{
		if (action == null || target == null) 
		{
			return;
		}

		if (_targetDict.ContainsKey(target)) 
		{
			_targetDict[target].Remove(action);
			if (_targetDict[target].Count == 0) 
			{
				_targetDict.Remove(target);
			}
		}


	}
		
	void Awake()
	{
		init();
	}

	public void init()
	{
		_targetDict = new Dictionary<MonoBehaviour,HashSet<CLAction>>();
	}
	
	static ActionManager _instance;

	static public ActionManager instance
	{
		get
		{
			if (_instance == null) 
			{
				GameObject obj = new GameObject("ActionManager");
				obj.AddComponent<ActionManager>();
				_instance = obj.GetComponent<ActionManager> ();
			}
			return _instance;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (_targetDict == null || _targetDict.Count == 0) 
		{
			return;
		}

		var toRemove = _targetDict.Where (pair => pair.Value.Count == 0 || 
												  pair.Key == null|| pair.Key.gameObject == null)
								    .Select (pair => pair.Key)
									.ToList ();
		

		for (int i = 0 ; i < toRemove.Count; i++) 
		{
			_targetDict.Remove (toRemove[i]);
		}


		var dictEnumer = _targetDict.GetEnumerator();
		while (dictEnumer.MoveNext()) 
		{
			var curHash = dictEnumer.Current.Value;
			var curActionEnumer = curHash.GetEnumerator();
			while (curActionEnumer.MoveNext()) 
			{
				curActionEnumer.Current.excute();
			}
			curHash.RemoveWhere (x => x.IsDone == true);

		}

			
	}


}
