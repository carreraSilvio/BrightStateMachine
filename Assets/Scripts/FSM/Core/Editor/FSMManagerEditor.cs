using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FSMManager))]
public class FSMManagerEditor : Editor
{
    private void OnEnable()
    {
        var target = (FSMManager)base.target;

        if (target.state == null)
        {
            string projectRelativeFilePath = "Assets/" + typeof(StateData).Name + ".asset";

            //Try to load existing asset.
            StateData asset = (StateData)AssetDatabase.LoadAssetAtPath(projectRelativeFilePath, typeof(StateData));

            //If none exists, create a new one.
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<StateData>();
                AssetDatabase.CreateAsset(asset, projectRelativeFilePath);
            }

            target.state = asset;
        }
    }
}