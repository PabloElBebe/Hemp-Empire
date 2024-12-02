using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PathFinder))]
    public class BasicCharacter : Character, IMovable
    {
        [SerializeField] private Color _selectColor;
        private float _moveSpeed;
        private Vector3 _targetPosition;

        private float _timer;
        private List<Vector3Int> CurrentPath = new List<Vector3Int>();
        private int _moveProgress;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (isSelected && Input.GetKeyDown(KeyCode.F))
            {
                CurrentPath = GetComponent<PathFinder>()
                    .FindPath(Vector3Int.RoundToInt(transform.position), Vector3Int.RoundToInt(MouseUtils.MousePositionToWorld()));
                _moveProgress = 0;
            }

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

        public void Initialize(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public override void Select()
        {
            GetComponent<SpriteRenderer>().color = _selectColor;
            isSelected = true;
        }

        public override void Deselect()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            isSelected = false;
        }

        public override void SetTargetPosition(Vector3 position)
        {
            _targetPosition = position;
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

        private Vector3Int GetNeighbor(Vector3 position)
        {
            Vector3Int neighbor = Vector3Int.zero;

            if (!CheckForObjects(position + Vector3Int.up))
                neighbor = Vector3Int.up;
            else if (!CheckForObjects(position + Vector3Int.right))
                neighbor = Vector3Int.right;
            else if (!CheckForObjects(position + Vector3Int.down))
                neighbor = Vector3Int.down;
            else if (!CheckForObjects(position + Vector3Int.left))
                neighbor = Vector3Int.left;

            return neighbor;
        }

        private bool CheckForObjects(Vector3 center)
        {
            RaycastHit2D[] hitColliders2D = Physics2D.RaycastAll(center, Vector3.forward);

            List<RaycastHit2D> hitList = hitColliders2D.ToList();

            foreach (RaycastHit2D raycastHit2D in hitColliders2D)
            {
                if (raycastHit2D.collider.gameObject == gameObject)
                    hitList.Remove(raycastHit2D);
            }

            return hitList.Count == 0;
        }
    }
}
