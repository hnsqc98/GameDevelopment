using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EulerGizmo : MonoBehaviour
{
    [Range(-180, 180)]
    public float eulerX;
    [Range(-180, 180)]
    public float eulerY;
    [Range(-180, 180)]
    public float eulerZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(eulerX, eulerY, eulerZ);
    }
}
