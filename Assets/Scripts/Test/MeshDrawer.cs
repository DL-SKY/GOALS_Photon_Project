//https://russianblogs.com/article/58811435604/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOALS.Test
{
    //[RequireComponent(typeof(MeshFilter))]
    //[RequireComponent(typeof(MeshRenderer))]
    //[ExecuteInEditMode]
    public class MeshDrawer : MonoBehaviour
    {
        public float Radius = 20.0f; // Радиус внешнего кольца
        public float Thickness = 10.0f; // Толщина, радиус внешнего круга минус радиус внутреннего круга
        public float Depth = 1.0f; // Толщина
        public float NumberOfSides = 30.0f; // Сколько лиц состоит из
        public float DrawArchDegrees = 90.0f; // Как долго рисовать
        public Material archMaterial = null;

        private List<Vector3> vertexList = new List<Vector3>();
        private List<int> triangleList = new List<int>();
        private List<Vector2> uvList = new List<Vector2>();

        [ContextMenu("GenerateVertex")]
        // Start is called before the first frame update
        void Start()
        {
            GenerateVertex();
        }

        void GenerateVertex()
        {
            // Координаты вершины
            vertexList.Clear();
            float incrementAngle = DrawArchDegrees / NumberOfSides;
            // Меньше или равно, потому что n + 1 линия может образовывать n граней
            for (int i = 0; i <= NumberOfSides; i++)
            {
                float angle = 180 - i * incrementAngle;
                float innerX = (Radius - Thickness) * Mathf.Cos(angle * Mathf.Deg2Rad);
                float innerY = (Radius - Thickness) * Mathf.Sin(angle * Mathf.Deg2Rad);
                vertexList.Add(new Vector3(innerX, 0, innerY));
                float outsideX = Radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                float outsideY = Radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                vertexList.Add(new Vector3(outsideX, 0, outsideY));
            }

            // Индекс треугольника
            triangleList.Clear();
            int direction = 1;
            for (int i = 0; i < NumberOfSides * 2; i++)
            {
                int[] triangleIndexs = getTriangleIndexs(i, direction);
                direction *= -1;
                for (int j = 0; j < triangleIndexs.Length; j++)
                {
                    triangleList.Add(triangleIndexs[j]);
                }
            }

            // УФ-индекс
            uvList.Clear();
            for (int i = 0; i <= NumberOfSides; i++)
            {
                float angle = 180 - i * incrementAngle;
                float littleX = (1.0f / NumberOfSides) * i;
                uvList.Add(new Vector2(littleX, 0));
                float bigX = (1.0f / NumberOfSides) * i;
                uvList.Add(new Vector2(bigX, 1));
            }
            Mesh mesh = new Mesh()
            {
                vertices = vertexList.ToArray(),
                uv = uvList.ToArray(),
                triangles = triangleList.ToArray(),
            };

            mesh.RecalculateNormals();

            MeshFilter meshFilter;
            if (!gameObject.TryGetComponent<MeshFilter>(out meshFilter))
                meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;

            MeshRenderer meshRenderer;
            if (!gameObject.TryGetComponent<MeshRenderer>(out meshRenderer))
                meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = archMaterial;
        }

        int[] getTriangleIndexs(int index, int direction)
        {
            int[] triangleIndexs = new int[3] { 0, 1, 2 };
            for (int i = 0; i < triangleIndexs.Length; i++)
            {
                triangleIndexs[i] += index;
            }
            if (direction == -1)
            {
                int temp = triangleIndexs[0];
                triangleIndexs[0] = triangleIndexs[2];
                triangleIndexs[2] = temp;
            }
            return triangleIndexs;
        }
    }
}