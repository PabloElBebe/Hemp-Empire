using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Character
{
    public class MovementController : MonoBehaviour
    {
        /*private List<GameObject> _characters = new List<GameObject>();
        private float _timer;
        
        private void Awake()
        {
            CharacterSpawner.SpawnCharacter += AddCharacter;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                foreach (GameObject character in _characters.Where(character => character.GetComponent<Character>().isSelected))
                    character.GetComponent<Character>().SetTargetPosition(MouseUtils.MousePositionToWorld());
            
            if (_characters.Count <= 0)
                return;

            if (_timer <= 0)
            {
                _timer = 0.075f;
                
                foreach (GameObject character in _characters)
                {
                    character.GetComponent<IMovable>().Move();
                }
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
        
        private void AddCharacter(GameObject character)
        {
            _characters.Add(character);
        }

        private void RemoveCharacter(GameObject character)
        {
            _characters.Remove(character);
        }*/
    }
}
