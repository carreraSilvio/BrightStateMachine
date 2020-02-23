using UnityEditor;

[CustomEditor(typeof(Command))]
public class CommandEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawPropertiesExcluding(serializedObject, "m_Script");

		serializedObject.ApplyModifiedProperties();
	}
}