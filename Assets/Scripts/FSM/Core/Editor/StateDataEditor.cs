using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateData))]
public class StateDataEditor : Editor
{
	private Color originalBackgroundColor;
	private SerializedProperty arrayProp;
	private new StateData target;

	private const string deleteTitle = "Confirm Deletion";
	private const string deletePrompt = "Are you sure you would like to delete this command?";
	private const string deleteText = "Delete";
	private const string cancelText = "Cancel";

	/// <summary>
	/// Cache some references
	/// </summary>
	private void OnEnable()
	{
		originalBackgroundColor = GUI.backgroundColor;

		target = (StateData)base.target;
		arrayProp = serializedObject.FindProperty("commands");
	}

	/// <summary>
	/// Clear references
	/// </summary>
	private void OnDisable()
	{
		target = null;
		arrayProp = null;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		//QuizData GUI
		DrawPropertiesExcluding(serializedObject, "questions");
		serializedObject.ApplyModifiedProperties();

		//Question GUI
		DrawQuestions(arrayProp);
		DrawAddButtons(arrayProp);
	}

	private void DrawAddButtons(SerializedProperty arrayProp)
	{
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();

		DrawAddButton<Command>();
		DrawAddButton<PlayAnimCommand>();

		EditorGUILayout.EndHorizontal();
	}

	private void DrawAddButton<T>( ) where T : Command
	{
		var buttonText = typeof(T).Name;
		if (GUILayout.Button(buttonText))
		{
			int newPos = arrayProp.arraySize;
			arrayProp.InsertArrayElementAtIndex(newPos);
			serializedObject.ApplyModifiedProperties();

			T newQuestion = ScriptableObject.CreateInstance<T>();
			newQuestion.name = "Q" + arrayProp.arraySize;
			target.commands[newPos] = newQuestion;

			AssetDatabase.AddObjectToAsset(newQuestion, base.target);
			AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(base.target));
		}
	}

	private void DrawQuestions(SerializedProperty arrayProp)
	{
		EditorGUILayout.LabelField("Commands: " + arrayProp.arraySize);

		for (int i = 0; i < arrayProp.arraySize; ++i)
		{
			EditorGUILayout.Space();

			SerializedProperty itemProp = arrayProp.GetArrayElementAtIndex(i);
			Command item = (Command)itemProp.objectReferenceValue;

			string label = "Question " + (i + 1) + " (" + item.GetType() + ")";
			itemProp.isExpanded = GUILayout.Toggle(itemProp.isExpanded, label, EditorStyles.foldout);

			if (itemProp.isExpanded)
			{
				Editor drawer = Editor.CreateEditor(item);

				EditorGUI.indentLevel += 2;

				drawer.OnInspectorGUI();

				if (DrawDeleteButton(itemProp, i))
				{
					if (--i > 0) continue;
					else return;
				}

				EditorGUI.indentLevel -= 2;
				EditorGUILayout.Space();
			}
		}
	}

	private bool DrawDeleteButton(SerializedProperty itemProp, int index)
	{
		EditorGUILayout.BeginHorizontal();
		GUI.backgroundColor = Color.red;
		GUILayout.FlexibleSpace();

		bool removed = false;

		if (GUILayout.Button(deleteText, GUILayout.MaxWidth(120)))
		{
			EditorApplication.Beep();
			if (EditorUtility.DisplayDialog(deleteTitle, deletePrompt, deleteText, cancelText))
			{
				UnityEngine.Object assetObject = itemProp.objectReferenceValue;

				//Remove item from the array
				if (itemProp.objectReferenceValue != null)
					arrayProp.DeleteArrayElementAtIndex(index);
				arrayProp.DeleteArrayElementAtIndex(index);

				//Rename remaining assets
				for (int i = 0; i < arrayProp.arraySize; ++i)
					arrayProp.GetArrayElementAtIndex(i).objectReferenceValue.name = "Q" + (i + 1);

				serializedObject.ApplyModifiedProperties();

				//Destroy the asset
				DestroyImmediate(assetObject, true);
				AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(base.target));

				removed = true;
			}
		}

		GUI.backgroundColor = originalBackgroundColor;
		EditorGUILayout.EndHorizontal();

		return removed;
	}
}