using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PathFinder))]
    [RequireComponent(typeof(BasicCharacterMovement))]
    public class BasicCharacter : Character
    {
        [SerializeField] private Color _selectColor;

        private void Update()
        {
            if (isSelected && Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(1))
            {
                GetComponent<BasicCharacterMovement>().StartMovement();;
            }
        }

        public void Initialize(float moveSpeed)
        {
            GetComponent<BasicCharacterMovement>().SetSpeed(moveSpeed);
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
