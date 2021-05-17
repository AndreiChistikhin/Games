using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{
    List<GameObject> lineRenderers;
	void Start()
	{
        EventManager.AddPathTraversalCompleteListener(StopDrawingEdges);

        // add a line renderer for each graph edge
        lineRenderers = new List<GameObject>();
        Graph<Waypoint> graph = GraphBuilder.Graph;
        foreach (GraphNode<Waypoint> node in graph.Nodes)
        {
            foreach (GraphNode<Waypoint> neighbor in node.Neighbors)
            {
                // add line renderer and draw line
                GameObject lineObj = new GameObject("LineObj");
                LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
                lineRenderer.material = new Material(Shader.Find("Hidden/Internal-Colored"));
                lineRenderers.Add(lineObj);

                
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;

                
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                
                lineRenderer.positionCount = 2;
   
                lineRenderer.SetPosition(0, node.Value.transform.position);
                lineRenderer.SetPosition(1, neighbor.Value.transform.position);
            }
        }
	}

    public void StopDrawingEdges()
    {
        
        for (int i = lineRenderers.Count - 1; i >= 0; i--)
        {
            Destroy(lineRenderers[i]);
        }
    }
}
