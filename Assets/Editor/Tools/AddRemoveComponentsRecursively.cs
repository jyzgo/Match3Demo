
using UnityEngine;
using System.Collections;


using UnityEditor;

/// <summary>
/// 对于选定GameObject，进行指定component的批量添加
/// </summary>
public class AddRemoveComponentsRecursively : ScriptableWizard
{
	public string componentType = null;
	
	/// <summary>
	/// 当没有任何GameObject被选中的时候，将菜单disable（注意，这个函数名可以随意取）
	/// </summary>
	/// <returns></returns>
	[MenuItem("GameObject/Add or remove components recursively...", true)]
	static bool CreateWindowDisabled()
	{
		return Selection.activeTransform;
	}
	
	/// <summary>
	/// 创建编辑窗口（注意，这个函数名可以随意取）
	/// </summary>
	[MenuItem("GameObject/Add or remove components recursively...")]
	static void CreateWindow()
	{
		// 定制窗口标题和按钮，其中第二个参数是Create按钮，第三个则属于other按钮
		// 如果不想使用other按钮，则可调用DisplayWizard的两参数版本
		ScriptableWizard.DisplayWizard<AddRemoveComponentsRecursively>(
			"Add or remove components recursivly",
			"Add", "Remove");
	}
	
	/// <summary>
	/// 窗口创建或窗口内容更改时调用
	/// </summary>
	void OnWizardUpdate()
	{
		helpString = "Note: Duplicates are not created";
		
		if (string.IsNullOrEmpty(componentType))
		{
			errorString = "Please enter component class name";
			isValid = false;
		}
		else
		{
			errorString = "";
			isValid = true;
		}
	}
	
	/// <summary>
	/// 点击Add按钮（即Create按钮）调用
	/// </summary>
	void OnWizardCreate()
	{
		int c = 0;
		Transform[] ts = Selection.GetTransforms(SelectionMode.Deep);
		foreach (Transform t in ts)
		{
			if (t.gameObject.GetComponent(componentType) == null)
			{
				if (UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(t.gameObject, "Assets/Editor/Tools/AddRemoveComponentsRecursively.cs (68,9)", componentType) == null)
//				if(t.gameObject.AddComponent<typeof(componentType)>())
				{
					Debug.LogWarning("Component of type " + componentType + " does not exist");
					return;
				}
				c++;
			}
		}
		Debug.Log("Added " + c + " components of type " + componentType);
	}
	
	/// <summary>
	/// 点击Remove（即other按钮）调用
	/// </summary>
	void OnWizardOtherButton()
	{
		int c = 0;
		Transform[] ts = Selection.GetTransforms(SelectionMode.Deep);
		foreach (Transform t in ts)
		{
			if (t.GetComponent(componentType) != null)
			{
				DestroyImmediate(t.GetComponent(componentType));
				c++;
			}
		}
		Debug.Log("Removed " + c + " components of type " + componentType);
		Close();
	}
}