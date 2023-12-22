using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class HUDUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _baseHpText;
        [SerializeField] private Image _treeHpImage;
        [SerializeField] private TMP_Text _coinsText;
        
        public void BaseHealthUpdate(int currentHp, float maxHp)
        {
            _baseHpText.text = currentHp.ToString()+"/"+maxHp;
            _treeHpImage.fillAmount = currentHp/maxHp;
        }
        
        public void CoinsCountUpdate(int coins)
        {
            _coinsText.text = coins.ToString();
        }
    }
}
