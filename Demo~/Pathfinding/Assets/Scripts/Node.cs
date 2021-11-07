//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using Lumpn.Pathfinding;
using UnityEngine;

public sealed class Node : INode
{
    public readonly string name;
    public readonly Vector3 position;

    public Node(string name, Vector3 position)
    {
        this.name = name;
        this.position = position;
    }
}
