using TMPro;
using UnityEngine;

namespace UISystem
{
    public class HUDUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _baseHpText;
        [SerializeField] private TMP_Text _coinsText;
        
        public void BaseHealthUpdate(int currentHp)
        {
            _baseHpText.text = currentHp.ToString();
        }
        
        public void CoinsCountUpdate(int coins)
        {
            _coinsText.text = coins.ToString();
        }
    }
}
