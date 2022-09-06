using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class PanelWithLocks : MonoBehaviour
{
    [SerializeField] private GameOverCanvas _winCanvas;
    [Inject] private Player _player;
    [Inject] private InputService _inputService;
    private bool _pointerDown;
    private  GraphicRaycaster _raycaster;
    private Key _currentKey;
    private Vector3 _keyPosition;
    private Lock _lockComponent;
    private List<GameObject> usedObject;

    private int _numberLock = 3;
    private int numberLock
    {
        get => _numberLock;
        set
        {
            _numberLock = value;
            if (_numberLock <= 0)
            {
                _numberLock = 3;
                ResetObject();
                gameObject.SetActive(false);
                _player.gameObject.SetActive(false);
                _winCanvas.SetText("You Win!");
                _winCanvas.gameObject.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        usedObject = new List<GameObject>();
        _raycaster = GetComponent<GraphicRaycaster>();
    }

    private void OnEnable()
    {
        _inputService.AddPointerDownListener(DownEventHandler);
        _inputService.AddPointerMoveListener(MoveEventHandler);
        _inputService.AddPointerUpListener(UpEventHandler);
    }

    private void OnDisable()
    {
        _inputService.RemovePointerDownListener(DownEventHandler);
        _inputService.RemovePointerMoveListener(MoveEventHandler);
        _inputService.RemovePointerUpListener(UpEventHandler);
    }

    private void UpEventHandler(Vector2 obj)
    {
        if (_pointerDown)
        {
            _pointerDown = false;
            if (!_currentKey)
            {
                return;
            }
            
            var downResults = GetRaycastResults(obj);

            foreach (RaycastResult result in downResults)
            {
                var isLock = result.gameObject.GetComponent<Lock>();
                if (isLock != null)
                {
                    _lockComponent = isLock;
                }
            }
            
            if (_lockComponent == null)
            {
                _currentKey.transform.position = _keyPosition;
                return;
            }

            if (_lockComponent.Color == _currentKey.Color)
            {
                _currentKey.gameObject.transform.position = _keyPosition;
                usedObject.Add(_currentKey.gameObject);
                usedObject.Add(_lockComponent.gameObject);
                _currentKey.gameObject.SetActive(false);
                _lockComponent.gameObject.SetActive(false);
                numberLock--;
            }
            else
            {
                _currentKey.transform.position = _keyPosition;
            }
        }
    }

    private List<RaycastResult> GetRaycastResults(Vector2 obj)
    {
        PointerEventData downPointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> downResults = new List<RaycastResult>();

        downPointerData.position = obj;
        _raycaster.Raycast(downPointerData, downResults);
        return downResults;
    }

    private void MoveEventHandler(Vector2 obj)
    {
        if (_pointerDown) 
            if (_currentKey != null) 
                _currentKey.gameObject.transform.position = obj;
    }

    private void DownEventHandler(Vector2 obj)
    {
        if (!_pointerDown)
        {
            _pointerDown = true;
            var upResults = GetRaycastResults(obj);
            
            foreach (RaycastResult result in upResults)
            {
                var isKey = result.gameObject.GetComponent<Key>();
                if (isKey != null)
                {
                    _keyPosition = isKey.transform.position;
                    _currentKey = isKey;
                }
            }
        }
    }

    private void ResetObject()
    {
        foreach (var obj in usedObject)
        {
            obj.SetActive(true);
        }
    }
}