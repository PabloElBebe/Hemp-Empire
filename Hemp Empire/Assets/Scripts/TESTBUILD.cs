using UnityEngine;
using UnityEngine.Tilemaps;

public class TESTBUILD : MonoBehaviour
{
    [SerializeField] private Tile _wallTile;
    [SerializeField] private Tile _floorTile;
    [SerializeField] private Tilemap _selectTilemap;

    private bool _isBuilding;
    private bool _isSelecting;

    private Vector3Int _startPosition;
    private Vector3Int _mousePosition;

    private void Update()
    {
        _mousePosition = Vector3Int.RoundToInt(MouseUtils.MousePositionToWorld());

        if (Input.GetMouseButtonDown(1) && _isSelecting)
            StopSelecting();

        if (Input.GetMouseButtonDown(1) && _isBuilding)
            StopBuilding();

        
        if (_isSelecting)
        {
            Selecting();
        }

        if (_isBuilding)
        {
            Building();
        }
    }

    public void StartSelecting()
    {
        _isSelecting = true;
    }

    private void Selecting()
    {
        ReloadTilemap();
        _selectTilemap.SetTile(_mousePosition, _wallTile);

        if (!Input.GetMouseButtonDown(0))
            return;
        _startPosition = _mousePosition;
        _selectTilemap.SetTile(_startPosition, _wallTile);
        _isSelecting = false;
        _isBuilding = true;
    }

    private void StopSelecting()
    {
        _isSelecting = false;
        _selectTilemap.ClearAllTiles();
    }

    private void Building()
    {
        ReloadTilemap();

        int invertX = 1;
        if (_mousePosition.x - _startPosition.x != 0)
            invertX = (_mousePosition.x - _startPosition.x) / Mathf.Abs(_mousePosition.x - _startPosition.x);

        int invertY = 1;
        if (_mousePosition.y - _startPosition.y !=0)
            invertY = (_mousePosition.y - _startPosition.y) / Mathf.Abs(_mousePosition.y - _startPosition.y);
        
        for (int x = _startPosition.x; x * invertX <= _mousePosition.x * invertX; x += invertX)
            for (int y = _startPosition.y; y * invertY <= _mousePosition.y * invertY; y += invertY)
                if (x == _startPosition.x || x == _mousePosition.x || y == _startPosition.y || y == _mousePosition.y)
                    _selectTilemap.SetTile(new Vector3Int(x, y), _wallTile);
                else
                    _selectTilemap.SetTile(new Vector3Int(x, y), _floorTile);
    }

    private void StopBuilding()
    {
        _isBuilding = false;
        _selectTilemap.ClearAllTiles();
    }

    private void ReloadTilemap()
    {
        _selectTilemap.ClearAllTiles();
    }
}
