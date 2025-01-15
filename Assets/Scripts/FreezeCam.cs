using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCam : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float horizontalRays = 100;
    [SerializeField] private float verticalRays = 70;
    [SerializeField] private float rayDistance = 10;
    [SerializeField] private float size = 5;
    [SerializeField] private float startDistance = 4;
    [SerializeField] private Transform player;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private GameObject flashLight;
    private Vector3 flashOffset;
    private Camera cam;

    private List<Vector3> corners = new List<Vector3>();

    private Vector3[] vertices = new Vector3[8];
    private Vector2[] uv = new Vector2[8];
    private int[] triangles = new int[36];

    private GameObject meshObject;
    private Mesh mesh;

    private int triangleIndex;

    private List<Ray> gizmoRays = new List<Ray>();

    void Start()
    {
        cam = GetComponent<Camera>();
        flashOffset = cam.transform.position - transform.position;

    }

    
    void Update()
    {
        corners.Clear();

        AddPositions(startDistance);
        AddPositions(startDistance + size);


        if (Input.GetMouseButtonDown(1))
        {
            gizmoRays.Clear();
            Vector3 pos = new(0, Screen.height);
            for (int y = (int)verticalRays; y > 0; y--)
            {
                pos.y = (y / verticalRays) * Screen.height;
                for (int x = 0; x < horizontalRays; x++)
                {
                    pos.x = (x / horizontalRays) * Screen.width;
                    Ray point = cam.ScreenPointToRay(pos);
                    gizmoRays.Add(point);
                    if (Physics.Raycast(point, out RaycastHit hit, rayDistance, mask))
                    {
                        hit.collider.gameObject.GetComponent<IFreezable>().Freeze();
                        Debug.Log(hit.point);
                    }

                    
                }
            }

            Vector3 upperLeftScreen = new Vector3(0, Screen.height, rayDistance);
            Vector3 upperRightScreen = new Vector3(Screen.width, Screen.height, rayDistance);
            Vector3 lowerLeftScreen = new Vector3(0, 0, rayDistance);
            Vector3 lowerRightScreen = new Vector3(Screen.width, 0, rayDistance);

            //Corner locations in world coordinates
            Vector3 upperLeft = cam.ScreenToWorldPoint(upperLeftScreen);
            Vector3 upperRight = cam.ScreenToWorldPoint(upperRightScreen);
            Vector3 lowerLeft = cam.ScreenToWorldPoint(lowerLeftScreen);
            Vector3 lowerRight = cam.ScreenToWorldPoint(lowerRightScreen);
            

            CreateObject();
            Destroy(Instantiate(flashLight, transform.position, player.rotation), 5);
            
        }
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

        //meshObject.AddComponent<MeshCollider>();
        meshObject.GetComponent<Renderer>().material = flashMaterial;
        meshObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

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
        Gizmos.color = Color.yellow;
        foreach (Vector3 position in corners)
        {
            Gizmos.DrawSphere(position, 0.2f);
        }
        foreach(Ray ray in gizmoRays) 
        {
            Gizmos.DrawRay(ray);
        }
        
    }
}
