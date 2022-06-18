using System.Collections.Generic;
using UnityEngine;

namespace GOALS.Test
{
    public class SpreadDrawer : MonoBehaviour
    {
        [SerializeField] private float _radius = 20.0f;
        [SerializeField] private float _centerOffset = 10.0f;
        [SerializeField] private float _faces = 30.0f;
        [SerializeField] private float _angle = 90.0f;
        [SerializeField] private Vector2 _direction = Vector2.up;

        [Space()]
        [SerializeField] private Material _archMaterial = null;

        private List<Vector3> _vertexList = new List<Vector3>();
        private List<int> _triangleList = new List<int>();
        private List<Vector2> _uvList = new List<Vector2>();


        [ContextMenu("GenerateVertex")]
        private void GenerateVertex()
        {
            _vertexList.Clear();
            float incrementAngle = _angle / _faces;
            float directionAngle = CalculateDirectionAngle(_direction);
            for (int i = 0; i <= _faces; i++)
            {
                float angle = (directionAngle + _angle / 2) - i * incrementAngle;
                float innerX = _centerOffset * Mathf.Cos(angle * Mathf.Deg2Rad);
                float innerY = _centerOffset * Mathf.Sin(angle * Mathf.Deg2Rad);
                _vertexList.Add(new Vector3(innerX, 0, innerY));

                float outsideX = _radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                float outsideY = _radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                _vertexList.Add(new Vector3(outsideX, 0, outsideY));
            }

            _triangleList.Clear();
            int direction = 1;
            for (int i = 0; i < _faces * 2; i++)
            {
                int[] triangleIndexs = GetTriangleIndexs(i, direction);
                direction *= -1;
                for (int j = 0; j < triangleIndexs.Length; j++)
                {
                    _triangleList.Add(triangleIndexs[j]);
                }
            }

            _uvList.Clear();
            for (int i = 0; i <= _faces; i++)
            {
                float angle = 180 - i * incrementAngle;
                float littleX = (1.0f / _faces) * i;
                _uvList.Add(new Vector2(littleX, 0));
                float bigX = (1.0f / _faces) * i;
                _uvList.Add(new Vector2(bigX, 1));
            }
            Mesh mesh = new Mesh()
            {
                vertices = _vertexList.ToArray(),
                uv = _uvList.ToArray(),
                triangles = _triangleList.ToArray(),
            };

            mesh.RecalculateNormals();

            MeshFilter meshFilter;
            if (!gameObject.TryGetComponent<MeshFilter>(out meshFilter))
                meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;

            MeshRenderer meshRenderer;
            if (!gameObject.TryGetComponent<MeshRenderer>(out meshRenderer))
                meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = _archMaterial;
        }

        private float CalculateDirectionAngle(Vector2 direction)
        {
            var angle = Vector2.SignedAngle(Vector2.right, direction);
            angle = angle < 0 ? 360.0f + angle : angle;
            return angle;
        }

        private int[] GetTriangleIndexs(int index, int direction)
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