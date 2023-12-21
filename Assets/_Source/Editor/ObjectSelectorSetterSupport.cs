using TowerSystem;
using UISystem;
using UnityEditor;
using UnityEngine;

namespace PlayerSystem
{
    [CustomEditor(typeof(ObjectsSelector))]
    [CanEditMultipleObjects]
    public class ObjectSelectorSetterSupport : Editor
    {
        private SerializedProperty _branches;

        private void OnEnable()
        {
            _branches = serializedObject.FindProperty("_branches");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();

            if(GUILayout.Button("SetBranches"))
            {
                _branches.arraySize = 0;
                foreach(var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
                {
                    if(gameObj.name == "TowerCells")
                    {
                        TowerCell[] towerCells = gameObj.GetComponentsInChildren<TowerCell>();
                        GameObject branch = gameObj.transform.parent.gameObject;
                        ObjectOutlineControl branchOutline = branch.GetComponent<ObjectOutlineControl>();
                        _branches.arraySize++;
                        _branches.GetArrayElementAtIndex(_branches.arraySize - 1).boxedValue =  new Branch(branch,towerCells,branchOutline);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}