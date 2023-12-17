using System;
using UnityEngine;

namespace TowerSystem
{
    [Serializable]
    public class BerserkTowerData: TowerData
    {
        [field: SerializeField] public int HP { get; private set; }
    }
}