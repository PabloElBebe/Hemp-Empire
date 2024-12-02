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
    private List<Vector3Int> Path = new List<Vector3Int>();

    private void Start()
    {
        _walkableTilemap = GameObject.FindGameObjectWithTag("Walkable Tilemap").GetComponent<Tilemap>();
    }

    public List<Vector3Int> FindPath(Vector3Int startPosition, Vector3Int targetPosition)
    {
        List<Vector3Int> currentPath = new List<Vector3Int>();

        List<Node> checkedPositions = new List<Node>();
        List<Node> notCheckedPositions = new List<Node>();

        Node startPos = new Node(startPosition, null, 0, GetTCoast(startPosition, targetPosition));
        Node lowestPriceNode = startPos;
        
        checkedPositions.Add(startPos);

        foreach (Vector3Int neighbor in GetNeighbors(startPos.Position))
        {
            notCheckedPositions.Add(new Node(neighbor, startPos, 1, GetTCoast(neighbor, targetPosition)));
        }

        int iter = 0;

        while (checkedPositions[^1].Position != targetPosition && iter < 5000)
        {
            currentPath = ReloadPath(lowestPriceNode);
            
            iter++;

            lowestPriceNode = GetNodeWithLowestCoast(notCheckedPositions);

            List<Vector3Int> neighbors = GetNeighbors(lowestPriceNode.Position);

            foreach (Vector3Int neighbor in neighbors.Where(CheckForObjects))
            {
                if (checkedPositions.Exists(n => n.Position == neighbor))
                    continue;

                if (_walkableTilemap.GetTile(neighbor) == null)
                    continue;
                
                notCheckedPositions.Add(
                    new Node(neighbor, lowestPriceNode, GetFCoast(neighbor, lowestPriceNode.Position), GetTCoast(neighbor, targetPosition)));
            }

            notCheckedPositions.Remove(lowestPriceNode);
            checkedPositions.Add(lowestPriceNode);
        }

        currentPath.Add(targetPosition);
        return currentPath;
    }

    private List<Vector3Int> ReloadPath(Node endNode)
    {
        List<Vector3Int> newPath = new List<Vector3Int>();

        Node currentNode = endNode;

        while (currentNode != null)
        {
            newPath.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        
        newPath.Reverse();

        return newPath;
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

        return hitList.Count == 0;
    }

    private float GetFCoast(Vector3Int position, Vector3Int startPosition)
    {
        return Vector3Int.Distance(position, startPosition);
    }

    private float GetTCoast(Vector3Int position, Vector3Int targetPosition)
    {
        return Vector3Int.Distance(position, targetPosition);
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
    public Vector3Int Position { get; }
    public Node Parent { get; }
    private float FCoast;
    private float TCoast;
    public float PCoast { get; }
    public float GCoast { get; }

    public Node(Vector3Int position, Node parent, float FCoast, float TCoast)
    {
        Position = position;
        Parent = parent;
        this.FCoast = FCoast;
        this.TCoast = TCoast;

        Node parentNode = parent;
        int iter = 0;
        while (parentNode != null && iter < 10000)
        {
            iter++;
            parentNode = parent.Parent;
            PCoast++;
        }

        GCoast = FCoast + TCoast;
    }
}
