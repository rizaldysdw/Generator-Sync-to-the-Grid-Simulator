using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorCapabilityCurve : Graphic
{
    public int numPoints = 10; // Total number of data points on the curve
    public float maxPowerOutput = 100f; // Maximum power output in MW
    public float minPowerFactor = 0.85f; // Minimum power factor
    public float maxPowerFactor = 0.95f; // Maximum power factor

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        List<UIVertex> vertices = new List<UIVertex>();

        float stepSize = maxPowerOutput / (numPoints - 1); // Calculate the step size between each data point

        for (int i = 0; i < numPoints; i++)
        {
            float powerOutput = stepSize * i;
            float powerFactor = minPowerFactor + (maxPowerFactor - minPowerFactor) * (powerOutput / maxPowerOutput);

            float x = powerOutput;
            float y = powerOutput * Mathf.Tan(Mathf.Acos(powerFactor));

            UIVertex vertex = UIVertex.simpleVert;
            vertex.position = new Vector3(x, y);
            vertex.color = color;

            vertices.Add(vertex);
        }

        int vertexCount = vertices.Count;

        vh.AddUIVertexStream(vertices, GenerateIndices(vertexCount));
    }

    private List<int> GenerateIndices(int vertexCount)
    {
        List<int> indices = new List<int>();

        int numRows = numPoints - 1;
        int numCols = vertexCount / numRows;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols - 1; col++)
            {
                int currentVertexIndex = row * numCols + col;

                indices.Add(currentVertexIndex);
                indices.Add(currentVertexIndex + 1);
                indices.Add(currentVertexIndex + numCols);

                indices.Add(currentVertexIndex + 1);
                indices.Add(currentVertexIndex + numCols + 1);
                indices.Add(currentVertexIndex + numCols);
            }
        }

        return indices;
    }
}
