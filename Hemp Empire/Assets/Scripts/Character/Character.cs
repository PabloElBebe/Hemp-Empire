using System;
using UnityEngine;

namespace Character
{
    public abstract class Character : MonoBehaviour, ISelectable
    {
        [NonSerialized] public bool isSelected;

        private void Awake()
        {
            SelectSystem.DeselectAll += Deselect;
        }
        
        public abstract void Select();

        public abstract void Deselect();
    }
}
