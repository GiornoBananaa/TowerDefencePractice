using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace TowerSystem
{
    [CreateAssetMenu(fileName = "new TowerData", menuName = "SO/TowerData")]
    public class TowerDataSO : ScriptableObject
    {
        [field: SerializeField] public TowerType TowerType { get; private set; }
        [field: SerializeField] public int Attack { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
    
    [CustomEditor(typeof(TowerDataSO))]
    [CanEditMultipleObjects]
    public class TowerDataCustomEditor : Editor
    {
        private SerializedProperty TowerType;
        private SerializedProperty Attack;
        private SerializedProperty AttackCooldown;
        private SerializedProperty AttackRange;
        private SerializedProperty BulletSpeed;
        private SerializedProperty Price; 
        private SerializedProperty Prefab;

        private void OnEnable()
        {
            TowerType = serializedObject.FindProperty("<TowerType>k__BackingField");
            Attack = serializedObject.FindProperty("<Attack>k__BackingField");
            AttackCooldown = serializedObject.FindProperty("<AttackCooldown>k__BackingField");
            AttackRange = serializedObject.FindProperty("<AttackRange>k__BackingField");
            BulletSpeed = serializedObject.FindProperty("<BulletSpeed>k__BackingField");
            Price = serializedObject.FindProperty("<Price>k__BackingField");
            Prefab = serializedObject.FindProperty("<Prefab>k__BackingField");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();
            GUILayout.Space(8);
            if (GUILayout.Button("Apply data"))
            {
                Tower tower = Prefab.objectReferenceValue.GetComponent<Tower>();
                SerializedObject serialized = new SerializedObject(tower);
                serialized.Update();
                serialized.FindProperty("<TowerType>k__BackingField").enumValueIndex = TowerType.enumValueIndex;
                serialized.FindProperty("<Attack>k__BackingField").intValue = Attack.intValue;
                serialized.FindProperty("<AttackCooldown>k__BackingField").floatValue = AttackCooldown.floatValue;
                serialized.FindProperty("<AttackRange>k__BackingField").floatValue = AttackRange.floatValue;
                serialized.FindProperty("<BulletSpeed>k__BackingField").floatValue = BulletSpeed.floatValue;
                serialized.FindProperty("<Price>k__BackingField").intValue = Price.intValue;
                serialized.ApplyModifiedProperties();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
