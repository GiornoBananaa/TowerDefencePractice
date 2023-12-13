using TowerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class TowerInspector: MonoBehaviour
    {
        [SerializeField] private Projector _rangeProjector;
        [SerializeField] private GameObject _inspectorPanel;
        
        private Tower _inspectedTower;
        
        public void InspectTower(Tower tower)
        {
            _inspectedTower = tower;
            _inspectorPanel.SetActive(true);
        }

        public void StopInspection()
        {
            HideAttackRange();
            _inspectorPanel.SetActive(false);
        }
        
        public void ShowAttackRange(Vector3 rangePoint, float rangeSize)
        {
            _rangeProjector.transform.position = rangePoint + new Vector3(0,2,0);
            _rangeProjector.orthographicSize = rangeSize;
            _rangeProjector.gameObject.SetActive(true);
        }
        public void HideAttackRange()
        {
            _rangeProjector.gameObject.SetActive(false);
        }
    }
}
