using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace EnemySystem
{
    [CreateAssetMenu(fileName = "new EnemyData", menuName = "SO/EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        [SerializeField] private EnemyTypes EnemyType;
        [SerializeField] private float Speed;
        [SerializeField] private int Attack;
        [SerializeField] private float AttackCooldown;
        [SerializeField] private int Hp;
        [SerializeField] private int Coins;
        [SerializeField] private GameObject Prefab;
    }

    [CustomEditor(typeof(EnemyDataSO))]
    [CanEditMultipleObjects]
    public class EnemyDataCustomEditor : Editor
    {
        private SerializedProperty EnemyType;
        private SerializedProperty Speed;
        private SerializedProperty Attack;
        private SerializedProperty AttackCooldown;
        private SerializedProperty Hp;
        private SerializedProperty Coins; 
        private SerializedProperty Prefab;

        private void OnEnable()
        {
            EnemyType = serializedObject.FindProperty("EnemyType");
            Speed = serializedObject.FindProperty("Speed");
            Attack = serializedObject.FindProperty("Attack");
            AttackCooldown = serializedObject.FindProperty("AttackCooldown");
            Hp = serializedObject.FindProperty("Hp");
            Coins = serializedObject.FindProperty("Coins");
            Prefab = serializedObject.FindProperty("Prefab");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();
            GUILayout.Space(8);
            if (GUILayout.Button("Apply data"))
            {
                Enemy enemy = Prefab.objectReferenceValue.GetComponent<Enemy>();
                SerializedObject serialized = new SerializedObject(enemy);
                serialized.Update();
                serialized.FindProperty("<EnemyType>k__BackingField").enumValueIndex = EnemyType.enumValueIndex;
                serialized.FindProperty("<Speed>k__BackingField").floatValue = Speed.floatValue;
                serialized.FindProperty("<Attack>k__BackingField").intValue = Attack.intValue;
                serialized.FindProperty("<AttackCooldown>k__BackingField").floatValue = AttackCooldown.floatValue;
                serialized.FindProperty("<Hp>k__BackingField").intValue = Hp.intValue;
                serialized.FindProperty("<Coins>k__BackingField").intValue = Coins.intValue;
                serialized.ApplyModifiedProperties();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}