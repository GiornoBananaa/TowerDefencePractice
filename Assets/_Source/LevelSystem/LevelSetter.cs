using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    [field: SerializeField] public float LevelDuration { get; private set; }
    [field: SerializeField] public GameObject[] EnemiesPrefabs { get; private set; }
}

public class LevelSetter
{
    public Action<LevelData> OnLevelChange;

    [SerializeField] private LevelData[] _levelDatas;
    private int _level;

    public void NextLevel()
    {
        _level++;
        OnLevelChange?.Invoke(_levelDatas[_level]);
    }
}
