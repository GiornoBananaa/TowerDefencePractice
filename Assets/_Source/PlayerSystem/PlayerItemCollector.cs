using UnityEngine;

namespace PlayerSystem
{
    public class PlayerItemCollector : MonoBehaviour
    {
        [SerializeField] private int _coinLayer;
        [SerializeField] private int _startCoins = 10;
        private PlayerInventory _inventory;

        public void Construct(PlayerInventory inventory)
        {
            _inventory = inventory;
            inventory.AddCoins(_startCoins);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _coinLayer)
            {
                _inventory.AddCoins(1);
                Destroy(other.gameObject);
            }
        }
    }
}
