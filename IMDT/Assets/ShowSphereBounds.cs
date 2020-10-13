using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSphereBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        /*
        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();
        Gizmos.color = Color.yellow;
        foreach (var renderer in renderers)
        {
            float r = Mathf.Max(renderer.bounds.size.x, renderer.bounds.size.y);
            r = Mathf.Max(r, renderer.bounds.size.z);
            Gizmos.DrawWireSphere(renderer.bounds.center, r/2);//renderer.bounds
        }
        */
    }
}
