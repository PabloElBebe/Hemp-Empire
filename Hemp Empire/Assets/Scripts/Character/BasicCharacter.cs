using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Character
{
    public class BasicCharacter : Character, IMovable
    {
        [SerializeField] private Color _selectColor;
        private float _moveSpeed;
        private Vector3 _targetPosition;

        private float _timer;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && isSelected)
                _targetPosition = MouseUtils.MousePositionToWorld();

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
            if (!CheckForObjects(transform.position))
                Debug.Log("Z");

            if (!(Vector3.Distance(transform.position, _targetPosition) > 1f))
                return;

            Vector3 normalizedPos = (_targetPosition - transform.position).normalized;
            Vector3Int currentPos = new Vector3Int((int)Mathf.Round(normalizedPos.x), (int)Mathf.Round(normalizedPos.y));

            if (!CheckForObjects(transform.position + currentPos))
                return;

            transform.position += currentPos;

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
