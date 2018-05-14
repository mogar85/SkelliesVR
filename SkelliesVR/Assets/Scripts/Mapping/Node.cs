using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    Node[] connections;

    public Node[] Connections
    {
        get
        {
            return connections;
        }

        set
        {
            connections = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (Connections == null || Connections.Length == 0)
        {
            Connections = GetConnections();
        }
    }

    private Node[] GetConnections()
    {
        int c = transform.parent.childCount;
        List<Node> newConnections = new List<Node>();
        for (int i = 0; i < c; i++)
        {
            GameObject node = transform.parent.GetChild(i).gameObject;
            if (!node.activeInHierarchy)
                continue;

            if (node != gameObject)
            {
                if (!Physics.Linecast(transform.position, node.transform.position))
                {
                    if (node.GetComponent<Node>())
                    {
                        newConnections.Add(node.GetComponent<Node>());
                    }
                }
            }
        }

        return newConnections.ToArray();
    }
}
