using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class MovementController : MonoBehaviour
    {
        private List<IMovable> _characters;

        private void Awake()
        {
            CharacterSpawner.SpawnCharacter += AddCharacter;
            CharacterDestroyer.DestroyCharacter += RemoveCharacter;
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
            foreach (IMovable character in _characters)
            {
                character.Move();
            }
        }
    }
}
