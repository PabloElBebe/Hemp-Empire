using UnityEngine;

namespace Character
{
    public class BasicCharacter : MonoBehaviour, IMovable
    {
        private float _moveSpeed;

        public void Initialize(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        
        public void Move()
        {
            int iterations = 0;
            Vector2Int tilePos = new Vector2Int(Random.Range(-10, 10), Random.Range(-10, 10));

            while (Vector2.Distance(transform.position, tilePos) > 0.9 && iterations < 100000)
            {
                transform.position = Vector2.Lerp(transform.position, tilePos, Time.deltaTime * _moveSpeed);
                iterations++;
            }
        }
    }
}
