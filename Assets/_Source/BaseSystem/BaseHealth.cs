using Core;
using TMPro;
using UnityEngine;

namespace BaseSystem
{
    public class BaseHealth : MonoBehaviour
    {
        [SerializeField] private TMP_Text hpText;
        private Game _game;
        private int _maxHp;
        private int _currentHp;
        
        public void TakeDamage(int damage)
        {
            _maxHp -= damage;
            if (_maxHp <= 0)
            {
                _game.Lose();
            }

            UpdateUi();
        }
        
        public void Heal(int hp)
        {
            _currentHp = _currentHp + hp > _maxHp ?
                _maxHp : _currentHp + hp;
            UpdateUi();
        }

        private void UpdateUi()
        {
            hpText.text = _currentHp.ToString();
        }
    }
}
