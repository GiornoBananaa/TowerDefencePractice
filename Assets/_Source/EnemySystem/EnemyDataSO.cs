using EnemySystem;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyData", menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField][OnChangedCall("OnSerializedPropertyChange")] private EnemyTypes EnemyType;
    [SerializeField][OnChangedCall("OnSerializedPropertyChange")] private float Speed;
    [SerializeField][OnChangedCall("OnSerializedPropertyChange")] private int Attack;
    [SerializeField][OnChangedCall("OnSerializedPropertyChange")] private int AttackCooldown;
    [SerializeField][OnChangedCall("OnSerializedPropertyChange")] private int Hp;
    [SerializeField][OnChangedCall("OnSerializedPropertyChange")] private int Coins;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private bool ApplyChanges;

    public void OnSerializedPropertyChange()
    {
        if (!ApplyChanges) return;

        SerializedObject serialized = new SerializedObject(Prefab.GetComponent<Enemy>());
        serialized.Update();
        serialized.FindProperty("<EnemyType>k__BackingField").enumValueIndex = (int)EnemyType;
        serialized.FindProperty("<Speed>k__BackingField").floatValue = Speed;
        serialized.FindProperty("<Attack>k__BackingField").intValue = Attack;
        serialized.FindProperty("<AttackCooldown>k__BackingField").intValue = AttackCooldown;
        serialized.FindProperty("<Hp>k__BackingField").intValue = Hp;
        serialized.FindProperty("<Coins>k__BackingField").intValue = Coins;
        serialized.ApplyModifiedProperties();
    }
}