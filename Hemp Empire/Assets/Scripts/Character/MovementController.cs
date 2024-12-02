using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MovementController : MonoBehaviour
    {
        private List<IMovable> _characters = new List<IMovable>();
        private float _timer;
        
        private void Awake()
        {
            CharacterSpawner.SpawnCharacter += AddCharacter;
        }
        
        private void AddCharacter(GameObject character)
        {
            _characters.Add(character.GetComponent<IMovable>());
        }

        private void RemoveCharacter(GameObject character)
        {
            _characters.Remove(character.GetComponent<IMovable>());
        }

        private void Update()
        {
            if (_characters.Count <= 0)
                return;

            if (_timer <= 0)
            {
                _timer = 0.1f;
                
                foreach (IMovable character in _characters)
                {
                    character.Move();
                }
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }
}
