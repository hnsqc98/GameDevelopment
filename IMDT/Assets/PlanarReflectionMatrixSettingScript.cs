using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlanarReflectionMatrixSettingScript : MonoBehaviour
{

    private static readonly Plane plane = new Plane(Vector3.up, Vector3.zero);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        if(renderer && renderer.sharedMaterial)
        {
            Matrix4x4 reflectionMat = CalculateReflectionMatrix(plane);
            renderer.sharedMaterial.SetMatrix("_ReflectionMat", reflectionMat);
        }
    }


    private static Matrix4x4 CalculateReflectionMatrix(Plane plane)
    {
        Matrix4x4 reflectionMat;
        reflectionMat.m00 = (1F - 2F * plane.normal.x * plane.normal.x);
        reflectionMat.m01 = (-2F * plane.normal.x * plane.normal.y);
        reflectionMat.m02 = (-2F * plane.normal.x * plane.normal.z);
        reflectionMat.m03 = (-2F * plane.distance * plane.normal.x);

        reflectionMat.m10 = (-2F * plane.normal.y * plane.normal.x);
        reflectionMat.m11 = (1F - 2F * plane.normal.y * plane.normal.y);
        reflectionMat.m12 = (-2F * plane.normal.y * plane.normal.z);
        reflectionMat.m13 = (-2F * plane.distance * plane.normal.y);

        reflectionMat.m20 = (-2F * plane.normal.z * plane.normal.x);
        reflectionMat.m21 = (-2F * plane.normal.z * plane.normal.y);
        reflectionMat.m22 = (1F - 2F * plane.normal.z * plane.normal.z);
        reflectionMat.m23 = (-2F * plane.distance * plane.normal.z);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;
        return reflectionMat;
    }
}
