using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationTest : MonoBehaviour
{
    public Vector3 acceleration;

    //上一帧结束时的速度
    private Vector3 preV;
    private void Start()
    {
        preV = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curV = preV + Time.deltaTime * acceleration;
        transform.Translate((curV + preV) * Time.deltaTime / 2);
        preV = curV;
    }
}
