using UnityEngine;

public static class MouseUtils
{
    public static Vector3 MousePositionToWorld()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        return new Vector3(mousePos.x, mousePos.y, 0);
    }
}
