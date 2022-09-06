using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public class DowerChestManager : MonoBehaviour
{
    [Inject] private Player _player;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameObject _dowerChest;
    [SerializeField] private Button _openPanelWithLocksButton;
    [SerializeField] private PanelWithLocks _panelWithLocks;

    private void OnEnable()
    {
        _openPanelWithLocksButton.onClick.AddListener(ActivatePanelWithLock);
        _scoreManager.SpawnDowerChest += SpawnDowerChest;
        _scoreManager.SpawnDowerChest += ActivateOpenButton;
    } 
    
    private void OnDisable()
    {
        _openPanelWithLocksButton.onClick.RemoveListener(ActivatePanelWithLock);
        _scoreManager.SpawnDowerChest -= SpawnDowerChest;
        _scoreManager.SpawnDowerChest -= ActivateOpenButton;
    }

    private void SpawnDowerChest()
    {
        var randomPosition = Random.insideUnitCircle * 3;
        _dowerChest.SetActive(true);
        _dowerChest.transform.position = _player.transform.position + new Vector3(randomPosition.x, _player.transform.position.y, randomPosition.y);
    }

    private void ActivateOpenButton()
    {
        _openPanelWithLocksButton.gameObject.SetActive(true);
    }

    private void ActivatePanelWithLock()
    {
        _openPanelWithLocksButton.gameObject.SetActive(false);
        _panelWithLocks.gameObject.SetActive(true);
        _dowerChest.SetActive(false);
        _panelWithLocks.gameObject.SetActive(true);
        
    }
}