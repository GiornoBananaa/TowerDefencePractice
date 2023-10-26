using UnityEngine;

namespace PlayerSystem
{
    public class PlayerItemCollector : MonoBehaviour
    {
        [SerializeField] private int _coinLayer;
        private PlayerInventory _inventory;

        public void Construct(PlayerInventory inventory)
        {
            _inventory = inventory;
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
