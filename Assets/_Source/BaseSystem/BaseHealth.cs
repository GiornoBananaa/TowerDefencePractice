using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace BaseSystem
{
    public class BaseHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHp;
        [SerializeField] private TMP_Text _hpText;
        private Game _game;
        private int _currentHp;

        public void Construct(Game game)
        {
            _game = game;
            _currentHp = 100;
        }
        
        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                _game.Lose();
            }

            if (_currentHp < 0)
                _currentHp = 0;
            
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
            _hpText.text = _currentHp.ToString();
        }
    }
}
