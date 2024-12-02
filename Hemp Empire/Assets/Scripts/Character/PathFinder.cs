using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Tile _selectTile1;
    [SerializeField] private Tile _selectTile2;
    private Tilemap _walkableTilemap;

    private void Start()
    {
        _walkableTilemap = GameObject.FindGameObjectWithTag("Walkable Tilemap").GetComponent<Tilemap>();
    }

    public List<Vector3Int> FindPath(Vector3Int startPosition, Vector3Int targetPosition)
    {
        List<Node> currentPath = new List<Node>();

        List<Node> checkedPositions = new List<Node>();
        List<Node> notCheckedPositions = new List<Node>();

        Node startPos = new Node(startPosition, null, 0, GetTCoast(startPosition, targetPosition));
        
        checkedPositions.Add(startPos);
        
        foreach (Vector3Int neighbor in GetNeighbors(startPos.Position))
        {
            notCheckedPositions.Add(new Node(neighbor, startPos, 1, GetTCoast(neighbor, targetPosition)));
        }

        int iter = 0;
        
        while (checkedPositions[^1].Position != targetPosition && iter < 500000)
        {
            iter++;
            
            Node lowestPriceNode = GetNodeWithLowestCoast(notCheckedPositions);

            Node node = lowestPriceNode;

            List<Vector3Int> neighbors = GetNeighbors(node.Position);

            foreach (Vector3Int neighbor in neighbors.Where(neighbor => CheckForObjects(neighbor)))
            {
                notCheckedPositions.Add(new Node(neighbor, node, GetFCoast(neighbor, node.Position), GetTCoast(neighbor, targetPosition)));
                _walkableTilemap.SetTile(neighbor, _selectTile1);
            }

            notCheckedPositions.Remove(node);
            checkedPositions.Add(node);
        }

        foreach (Node node in checkedPositions)
        {
            _walkableTilemap.SetTile(node.Position, _selectTile2);
        }
        
        List<Vector3Int> path = checkedPositions.Select(node => node.Position).ToList();

        return path;
    }
    
    private List<Vector3Int> GetNeighbors(Vector3Int position)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();
        
        neighbors.Add(position + Vector3Int.up);
        neighbors.Add(position + Vector3Int.right);
        neighbors.Add(position + Vector3Int.down);
        neighbors.Add(position + Vector3Int.left);

        return neighbors;
    }
    
    private bool CheckForObjects(Vector3Int center)
    {
        RaycastHit2D[] hitColliders2D = Physics2D.RaycastAll(new Vector2(center.x, center.y), Vector3.forward);

        List<RaycastHit2D> hitList = hitColliders2D.ToList();

        return hitList.Count == 0 && _walkableTilemap.GetTile(center) != null;
    }

    private float GetFCoast(Vector3 position, Vector3 startPosition)
    {
        return Vector3.Distance(position, startPosition);
    }
    
    private float GetTCoast(Vector3 position, Vector3 targetPosition)
    {
        return Vector3.Distance(position, targetPosition);
    }

    private Node GetNodeWithLowestCoast(List<Node> nodes)
    {
        Node currentNode = nodes[0];

        foreach (Node node in nodes.Where(node => currentNode.GCoast > node.GCoast))
        {
            currentNode = node;
        }

        return currentNode;
    }
}

internal class Node
{
    public Vector3Int Position;
    public Node Parent;
    public float FCoast;
    public float TCoast;
    public float GCoast;

    public Node(Vector3Int position, Node parent, float FCoast, float TCoast)
    {
        Position = position;
        Parent = parent;
        this.FCoast = FCoast;
        this.TCoast = TCoast;
        GCoast = FCoast + TCoast;
    }
}
