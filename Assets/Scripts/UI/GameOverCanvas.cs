using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    public event Action RestartLevel;
    [SerializeField] private TextMeshProUGUI _gameStateText;
    [SerializeField] private Button _restartLevelButton;
    private void OnEnable()
    {
        Time.timeScale = 0f;
        _restartLevelButton.onClick.AddListener(RestartHandler);  
    }

    private void OnDisable()
    {
        _restartLevelButton.onClick.RemoveListener(RestartHandler);  
    }

    public void SetText(string text)
    {
        _gameStateText.text = text;
    }
    
    private void RestartHandler()
    {
        Time.timeScale = 1f;
        RestartLevel?.Invoke();
    }
}