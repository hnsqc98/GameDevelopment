using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSphere : MonoBehaviour
{
    public float friction;
    public float mass;
    public float radius;
    public GameObject leftwall;
    public GameObject upwall;
    public GameObject downwall;
    public GameObject rightwall;

    [NonSerialized]
    public Vector3 currentV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * currentV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = currentV + frictionDeltaV;
        if (finalV.x * currentV.x <= 0)
            frictionDeltaV.x = -currentV.x;
        if (finalV.y * currentV.y <= 0)
            frictionDeltaV.y = -currentV.y;
        if (finalV.z * currentV.z <= 0)
            frictionDeltaV.z = -currentV.z;

        //应用加速度
        Vector3 curV = currentV + frictionDeltaV;
        transform.Translate((curV + currentV) * Time.deltaTime / 2);
        currentV = curV;

		//检测是否与墙体碰撞
		if (upwall == null || downwall == null || leftwall == null || rightwall == null)
		{
			Debug.LogError("The wall is missing.");
		}
		//计算碰撞边缘，认为大球半径为radius
		float xmin = radius + leftwall.transform.position.x + leftwall.GetComponent<BoxCollider>().size.x / 2;
		float xmax = -radius + rightwall.transform.position.x - rightwall.GetComponent<BoxCollider>().size.x / 2;
		float zmax = -radius + upwall.transform.position.z - upwall.GetComponent<BoxCollider>().size.z / 2;
		float zmin = radius + downwall.transform.position.z + downwall.GetComponent<BoxCollider>().size.z / 2;

		//计算四个碰撞体的法向量
		Vector3 xminF = new Vector3(1, 0, 0);
		Vector3 xmaxF = new Vector3(-1, 0, 0);
		Vector3 zminF = new Vector3(0, 0, 1);
		Vector3 zmaxF = new Vector3(0, 0, -1);
		
		Vector3 temppos = transform.position;
		//用Reflect函数实现完全弹性碰撞
		if (temppos.x <= xmin)
		{
			Vector3 v1 = Vector3.Reflect(currentV, xminF);
			currentV = v1;
			Debug.Log("红球撞左墙!");
		}
		if (temppos.x >= xmax)
		{
			Vector3 v1 = Vector3.Reflect(currentV, xmaxF);
			currentV = v1;
			Debug.Log("红球撞右墙!");
		}
		if (temppos.z <= zmin)
		{
			Vector3 v1 = Vector3.Reflect(currentV, zminF);
			currentV = v1;
			Debug.Log("红球撞下墙!");
		}
		if (temppos.z >= zmax)
		{
			Vector3 v1 = Vector3.Reflect(currentV, zmaxF);
			currentV = v1;
			Debug.Log("红球撞上墙!");
		}
	}
}
