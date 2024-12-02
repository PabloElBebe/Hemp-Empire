using System;
using UnityEngine;

namespace Character
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _characterPrefab;
        public static Action<GameObject> SpawnCharacter;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                Spawn(_characterPrefab);
        }

        private void Spawn(GameObject character)
        {
            GameObject spawned = Instantiate(character, Vector2.zero, Quaternion.identity);
            spawned.GetComponent<BasicCharacter>().Initialize(4);
        }
    }
}
