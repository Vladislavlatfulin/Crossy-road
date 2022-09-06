using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action SpawnDowerChest;
    private TextMeshProUGUI _text;
    private int _countEnemy = 2;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "Осталось врагов: " + _countEnemy;
    }

    public void DecreaseScore()
    {
        _countEnemy--;
        if (_countEnemy == 0)
        {
            SpawnDowerChest?.Invoke();
        }
        _text.text = "Осталось врагов: " + _countEnemy;
    }

    public void resetCountEnemy()
    {
        _countEnemy = 2;
        _text.text = "Осталось врагов: " + _countEnemy;
    }
}