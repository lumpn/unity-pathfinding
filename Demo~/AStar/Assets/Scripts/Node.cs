//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using UnityEngine;

public sealed class Node
{
    public readonly int id;
    public readonly string name;
    public readonly Vector3 position;

    public Node(int id, string name, Vector3 position)
    {
        this.id = id;
        this.name = name;
        this.position = position;
    }
}

