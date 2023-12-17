using TMPro;
using UnityEngine;

namespace UISystem
{
    public class TowerStatBlock: MonoBehaviour
    {
        [field: SerializeField] public TMP_Text StatName { get; private set; }
        [field: SerializeField] public TMP_Text StatValue { get; private set; }
        [SerializeField] public TMP_Text _upgradedValue;

        public void EnableUpgradedValueView(string upgradedValue)
        {
            _upgradedValue.text = upgradedValue;
            _upgradedValue.gameObject.SetActive(true);
        }
        
        public void DisableUpgradedValueView()
        {
            _upgradedValue.gameObject.SetActive(false);
        }
    }
}