using UnityEngine;

namespace Character
{
    public class BasicCharacter : MonoBehaviour, IMovable
    {
        private float _moveSpeed;
        private Vector3 _targetPosition;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        public void Initialize(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        
        public void Move()
        {
            if (Vector3.Distance(transform.position, _targetPosition) > 1f)
            {
                Vector3 normalizedPos = (_targetPosition - transform.position).normalized;
                transform.position += new Vector3Int((int)Mathf.Round(normalizedPos.x), (int)Mathf.Round(normalizedPos.y));
            }
            else
            {
                _targetPosition = new Vector3Int(Random.Range(-30, 30), Random.Range(-20, 20));
            }
        }
    }
}
