﻿using System;
using UnityEngine;

public class SelectSystem : MonoBehaviour
{
    public static Action DeselectAll;

    [SerializeField] private GameObject _selectBoxPrefab;
    private Vector3 _startPosition;

    private GameObject _selectBox;
    private Vector3 _mousePosition;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) DeselectAll?.Invoke();
        
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = MouseUtils.MousePositionToWorld();
                _selectBox = Instantiate(_selectBoxPrefab, _startPosition, Quaternion.identity);
            }

            Selecting();
        }
        else
        {
            if (_startPosition == Vector3.zero)
                return;
            _startPosition = Vector3.zero;
            
            if (_selectBox == null)
                return;
            Destroy(_selectBox);
        }
    }

    private void Selecting()
    {
        Vector3 endPosition = MouseUtils.MousePositionToWorld();
        
        _selectBox.transform.localScale = endPosition - _startPosition;
        _selectBox.transform.position = (endPosition - _startPosition) / 2 + _startPosition;
    }
}