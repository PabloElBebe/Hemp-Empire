using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class BasicCharacterMovement : MonoBehaviour, IMovable
    {
        private float _moveSpeed;

        private float _timer;
        private List<Vector3Int> CurrentPath = new List<Vector3Int>();
        private int _moveProgress;

        private void Start()
        {
            StartMovement();
        }

        private void Update()
        {
            if (_timer <= 0)
            {
                _timer = 0.075f;

                Move();
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }

        public void StartMovement()
        {
            /*CurrentPath = GetComponent<PathFinder>()
                .FindPath(Vector3Int.RoundToInt(transform.position), Vector3Int.RoundToInt(MouseUtils.MousePositionToWorld()));*/
            CurrentPath = GetComponent<PathFinder>()
                .FindPath(Vector3Int.RoundToInt(transform.position), new Vector3Int(Random.Range(-25, 26), Random.Range(-15, 16)));
            _moveProgress = 0;
        }

        public void SetSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Move()
        {
            if (CurrentPath.Count <= 0)
                return;
            if (_moveProgress < CurrentPath.Count)
            {
                transform.position = CurrentPath[_moveProgress];
                _moveProgress++;
            }
            else
            {
                StartMovement();
            }
        }
    }
}
