using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class BasicCharacterMovement : MonoBehaviour, IMovable
    {
        private float _moveSpeed;

        private float _timer;
        private List<Vector3Int> CurrentPath = new List<Vector3Int>();
        private int _moveProgress;

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
            CurrentPath = GetComponent<PathFinder>()
                .FindPath(Vector3Int.RoundToInt(transform.position), Vector3Int.RoundToInt(MouseUtils.MousePositionToWorld()));
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
            if (_moveProgress >= CurrentPath.Count)
                return;

            transform.position = CurrentPath[_moveProgress];
            _moveProgress++;
        }
    }
}
