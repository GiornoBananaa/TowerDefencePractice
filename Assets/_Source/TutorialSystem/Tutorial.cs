using System.Collections;
using EnemySystem;
using InputSystem;
using TMPro;
using TowerSystem;
using UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TutorialSystem
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Sprite[] _dialogSprite;
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private Image _dialogImage;
        [SerializeField] private RectTransform _clickHintImage;
        [SerializeField] private RectTransform _clickHintParent;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _endTutorialButton;
        [SerializeField] private RectTransform _upgradeButton;
        [SerializeField] private ObjectOutlineControl _branchOutline;
        [SerializeField] private ObjectOutlineControl _treeOutline;
        [SerializeField] private Transform _towerCell;
        [SerializeField] private GameObject _roadBranch;
        [SerializeField] private TowerOptionsUI _towerOptionsUI;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private TowerInspector _towerInspector;
        [SerializeField] private GameObject _toolCanvas;
        [SerializeField] private int _towerLayer;
        [SerializeField] private int _enemiesKillForEnd;
        private int _dialogWindowNumber;
        private int _actionNumber;
        private Camera _camera;
        private GameObject _firstEnemy;
        private Transform _firstTower;
        private RectTransform _towerBuildButton;

        private void Awake()
        {
            _toolCanvas.SetActive(false);
            _clickHintImage.gameObject.SetActive(false);
            _roadBranch.SetActive(false);
            _endTutorialButton.gameObject.SetActive(false);
            _nextButton.gameObject.SetActive(true);
            _nextButton.onClick.AddListener(NextText);
            _endTutorialButton.onClick.AddListener(EndTutorial);
            _camera = Camera.main;
            _towerOptionsUI.OnUnitSpawn += TowerBuildClick;
            StartCoroutine(TutorialAwake());
        }

        private void LateUpdate()
        {
            switch (_actionNumber)
            {
                case 0:
                    PlatformClick();
                    break;
                case 1:
                    TowerCellClick();
                    break;
                case 2:
                    TowerBuildClickWait();
                    break;
                case 3:
                    FirstEnemyKill();
                    break;
                case 4:
                    TowerClick();
                    break;
                case 5:
                    TowerUpgradeHint();
                    break;
                case 6:
                    AllEnemiesKill();
                    break;
            }
        }

        private void NextText()
        {
            if(_dialogSprite.Length-1 <= _dialogWindowNumber+1)
            {
                _endTutorialButton.gameObject.SetActive(true);
                _nextButton.gameObject.SetActive(false);
            }
            
            
            _dialogWindowNumber++;
            
            switch (_dialogWindowNumber)
            {
                case 5:
                    _clickHintImage.gameObject.SetActive(true);
                    _branchOutline.EnableOutline(true);
                    _branchOutline.ChangeColor(Color.yellow);
                    _branchOutline.AlwaysShowMode(true);
                    EnableDialog(false);
                    break;
                case 8:
                    _towerInspector.OnTowerUpgrade += UpgradeClick;
                    _roadBranch.SetActive(true);
                    _clickHintImage.gameObject.SetActive(true);
                    EnableDialog(false);
                    break;
            }
            
            _dialogImage.sprite = _dialogSprite[_dialogWindowNumber];
        }
        
        private void EnableDialog(bool enable)
        {
            _dialogImage.sprite = _dialogSprite[_dialogWindowNumber];
            _dialogPanel.SetActive(enable);
            _inputListener.enabled = !enable;
            Time.timeScale = enable?0:1;
            _pausePanel.gameObject.SetActive(!enable);
        }
        
        private void MoveClickHint(Transform targetTransform)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_clickHintParent, _camera.WorldToScreenPoint(targetTransform.position), _camera, out Vector2 anchored);
            _clickHintImage.anchoredPosition = anchored;
        }
        
        private void PlatformClick()
        {
            MoveClickHint(_branchOutline.transform);
            if(!Input.GetMouseButtonDown(0)) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray, out RaycastHit _raycastHit) || EventSystem.current.IsPointerOverGameObject()) return;

            if (_raycastHit.transform.gameObject == _branchOutline.gameObject || _raycastHit.transform.gameObject.layer == _towerCell.gameObject.layer)
            {
                _actionNumber++;
                _branchOutline.ResetColor();
                _branchOutline.AlwaysShowMode(false);
            }
        }
        
        private void TowerCellClick()
        {
            MoveClickHint(_towerCell);
            if(!Input.GetMouseButtonDown(0)) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray, out RaycastHit _raycastHit) || EventSystem.current.IsPointerOverGameObject()) return;
            
            if (_raycastHit.transform.TryGetComponent(out TowerCell cell))
            {
                _actionNumber++;
                _towerBuildButton = FindObjectOfType<TowerBuildButton>().GetComponent<RectTransform>();
                _clickHintImage.gameObject.SetActive(false);
            }
        }
        
        private void TowerBuildClickWait()
        {
            MoveClickHint(_towerBuildButton);
            if(_towerBuildButton.gameObject.activeInHierarchy && !_clickHintImage.gameObject.activeSelf)
            {
                _clickHintImage.gameObject.SetActive(true);
            }
            else if(!_towerBuildButton.gameObject.activeInHierarchy && _clickHintImage.gameObject.activeSelf)
                _clickHintImage.gameObject.SetActive(false);
        }
        
        private void TowerBuildClick()
        {
            _clickHintImage.gameObject.SetActive(false);
            _actionNumber++;
            _towerOptionsUI.OnUnitSpawn -= TowerBuildClick;
            _firstTower = FindObjectOfType<Tower>().transform;
            _toolCanvas.SetActive(true);
            _firstEnemy = _enemySpawner.SpawnEnemy();
        }
        
        private void FirstEnemyKill()
        {
            if (_firstEnemy != null && !_firstEnemy.activeSelf)
            {
                _actionNumber++;
                EnableDialog(true);
            }
        }
        
        private void TowerClick()
        {
            MoveClickHint(_firstTower);
            if(!Input.GetMouseButtonDown(0)) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray, out RaycastHit _raycastHit) || EventSystem.current.IsPointerOverGameObject()) return;

            if (_raycastHit.transform.gameObject.layer == _towerLayer)
            {
                _actionNumber++;
            }
        }
        
        private void TowerUpgradeHint()
        {
            if(_towerInspector.IsInspecting)
            {
                MoveClickHint(_upgradeButton);
                if(!_clickHintImage.gameObject.activeSelf)
                    _clickHintImage.gameObject.SetActive(true);
            }
            else if(_clickHintImage.gameObject.activeSelf)
                _clickHintImage.gameObject.SetActive(false);
        }
        
        private void UpgradeClick()
        {
            _actionNumber++;
            _towerInspector.OnTowerUpgrade -= UpgradeClick;
            _enemySpawner.StartSpawning();
            _enemySpawner.enabled = true;
            _clickHintImage.gameObject.SetActive(false);
        }
        
        private void AllEnemiesKill()
        {
            if (_enemySpawner.KilledEnemies >= _enemiesKillForEnd)
            {
                _actionNumber++;
                _enemySpawner.StopSpawning();
                _enemySpawner.ReturnAllTosSpawn();
                StartCoroutine(EnemiesKilled());
            }
        }
        
        private void EndTutorial()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        
        private IEnumerator TutorialAwake()
        {
            yield return new WaitForSeconds(0.1f);
            _treeOutline.EnableOutline(false);
            _branchOutline.EnableOutline(false);
            EnableDialog(true);
            Time.timeScale = 0;
        }
        private IEnumerator EnemiesKilled()
        {
            yield return new WaitForSeconds(3);
            
            EnableDialog(true);
        }
    }
}
