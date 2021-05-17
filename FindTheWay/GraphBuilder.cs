using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphBuilder : MonoBehaviour
{
    static Graph<Waypoint> graph;

    public void Awake()
    {
        Waypoint start = GameObject.FindGameObjectWithTag("Start").GetComponent<Waypoint>();
        Waypoint end = GameObject.FindGameObjectWithTag("End").GetComponent<Waypoint>();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        // add nodes (all waypoints, including start and end) to graph
        graph = new Graph<Waypoint>();
        graph.AddNode(start);
        graph.AddNode(end);
        foreach (GameObject waypoint in waypoints)
        {
            graph.AddNode(waypoint.GetComponent<Waypoint>());
        }

        // add neighbors for each node in graph
        foreach (GraphNode<Waypoint> firstNode in graph.Nodes)
        {
            foreach (GraphNode<Waypoint> secondNode in graph.Nodes)
            {
                // no self edges
                if (firstNode != secondNode)
                {
                    Vector2 positionDelta = firstNode.Value.Position -
                        secondNode.Value.Position;
                    if (Mathf.Abs(positionDelta.x) < 3.5f &&
                        Mathf.Abs(positionDelta.y) < 3f)
                    {
                        firstNode.AddNeighbor(secondNode, positionDelta.magnitude);
                    }
                }
            }
        }
    }

    public static Graph<Waypoint> Graph
    {
        get { return graph; }
        set { graph = value; }
    }
}
