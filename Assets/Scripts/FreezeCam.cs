using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCam : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float size = 5;
    [SerializeField] private float startDistance = 4;
    private Camera cam;

    private List<Vector3> corners = new List<Vector3>();

    private Vector3[] vertices = new Vector3[8];
    private Vector2[] uv = new Vector2[8];
    private int[] triangles = new int[36];

    private GameObject meshObject;
    private Mesh mesh;

    private int triangleIndex;

    void Start()
    {
        cam = GetComponent<Camera>();

    }

    private void CreateObject()
    {
        for (int i = 0; i < 8; i++) 
        {
            vertices[i] = corners[i];
        }


        triangleIndex = 0;
        AddFace(new Vector4(0, 1, 2, 3));
        AddFace(new Vector4(4, 5, 1, 0));
        AddFace(new Vector4(4, 0, 3, 7));
        AddFace(new Vector4(3, 2, 6, 7));
        AddFace(new Vector4(1, 5, 6, 2));
        AddFace(new Vector4(7, 6, 5, 4));

        mesh = new Mesh();
        mesh.name = "Custom Mesh";

        meshObject = new GameObject("Mesh Object", typeof(MeshRenderer), typeof(MeshFilter));
        meshObject.GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshObject.AddComponent<MeshCollider>();

        Destroy(meshObject, 2);
    }

    private void AddFace(Vector4 corners)
    {
        triangles[triangleIndex] = (int)corners.x;
        triangleIndex++;
        triangles[triangleIndex] = (int)corners.y;
        triangleIndex++;
        triangles[triangleIndex] = (int)corners.z;
        triangleIndex++;

        triangles[triangleIndex] = (int)corners.x;
        triangleIndex++;
        triangles[triangleIndex] = (int)corners.z;
        triangleIndex++;
        triangles[triangleIndex] = (int)corners.w;
        triangleIndex++;
    }


    void Update()
    {
        corners.Clear();

        AddPositions(startDistance);
        AddPositions(startDistance + size);


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 3, transform.forward, 3, mask);
            foreach (RaycastHit hit in hits)
            {
                hit.collider.gameObject.GetComponent<IFreezable>().Freeze();

                
            }

            CreateObject();
        }

        




    }


    private void AddPositions(float depth)
    {
        // Screens coordinate corner location
        Vector3 upperLeftScreen = new Vector3(0, Screen.height, depth);
        Vector3 upperRightScreen = new Vector3(Screen.width, Screen.height, depth);
        Vector3 lowerLeftScreen = new Vector3(0, 0, depth);
        Vector3 lowerRightScreen = new Vector3(Screen.width, 0, depth);

        //Corner locations in world coordinates
        Vector3 upperLeft = cam.ScreenToWorldPoint(upperLeftScreen);
        Vector3 upperRight = cam.ScreenToWorldPoint(upperRightScreen);
        Vector3 lowerLeft = cam.ScreenToWorldPoint(lowerLeftScreen);
        Vector3 lowerRight = cam.ScreenToWorldPoint(lowerRightScreen);


        corners.Add(lowerLeft);
        corners.Add(upperLeft);
        corners.Add(upperRight);
        corners.Add(lowerRight);
    }



    private void OnDrawGizmos()
    {
        foreach (Vector3 position in corners)
        {
            Gizmos.DrawSphere(position, 0.2f);
        }
    }
}
