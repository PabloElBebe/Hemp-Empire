using System;
using UnityEngine;

public class SelectBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<ISelectable>() != null)
            col.GetComponent<ISelectable>().Select();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<ISelectable>() != null && Input.GetMouseButton(0))
            col.GetComponent<ISelectable>().Deselect();
    }
}
