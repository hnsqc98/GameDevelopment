using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollosionTest : MonoBehaviour
{
	public float force;
	public float friction;

	public GameObject other;
	public GameObject leftwall;
	public GameObject upwall;
	public GameObject downwall;
	public GameObject rightwall;

	//上一帧结束时的速度
	private Vector3 preV;

	void Start()
	{
		preV = Vector3.zero;
	}

	void Update()
	{
		//只有小球的推力force>最大静摩擦力friction时，小球可以被推动
		//此时滑动摩擦力为常量friction，方向与速度方向相反
		if (force > friction)
		{ 
			//计算用户推力方向
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			Vector3 fDir = new Vector3(moveHorizontal, 0.0f, moveVertical);
			fDir.Normalize();

			//计算摩擦力
			//↓在小球减速至停止时，保证摩擦力不会让小球反向运动
			//使【每帧摩擦力造成的速度衰减】不大于【前一帧速度的模长】
			float changefriction = friction * Time.deltaTime > preV.magnitude ? preV.magnitude : friction * Time.deltaTime;
			//计算小球实时摩擦力
			Vector3 realtimefriction = -changefriction * preV.normalized;

			//计算合力（推力与摩擦力的合力)
			//计算合力的加速度
			Vector3 acceleration = fDir * force + realtimefriction;
			Vector3 prePos = transform.position;

			//应用合力的加速度
			Vector3 curV = preV + Time.deltaTime * acceleration;
			transform.Translate((curV + preV) * Time.deltaTime / 2);
			preV = curV;

			//检测是否与其他球相撞
			Vector3 pos = transform.position;
			if (other == null)
			{
				Debug.LogError("The other ball is missing.");
			}

			OtherSphere otherSphere = other.GetComponent<OtherSphere>();
			Vector3 otherPos = other.transform.position;

			//球体间碰撞检测，判断球心距离与两球半径之和即可
			if (Vector3.Distance(pos, otherPos) < 0.5 + otherSphere.radius) //简单起见，认为自己的半径为0.5
			{
				Debug.Log("两球碰撞发生!");
				Vector3 v1 = preV;
				float m1 = 1.0f; // 简单起见，认为自己的质量为1
				Vector3 v2 = otherSphere.currentV;
				float m2 = otherSphere.mass;

				preV = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
				otherSphere.currentV = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);

				//如果有碰撞，位置回退，防止穿透
				transform.position = prePos;
			}

			//检测是否与墙体碰撞
			if (upwall == null || downwall == null || leftwall == null || rightwall == null)
			{
				Debug.LogError("The wall is missing.");
			}
			//计算碰撞边缘,认为小球半径为0.5
			float xmin = 0.5f + leftwall.transform.position.x + leftwall.GetComponent<BoxCollider>().size.x / 2;
			float xmax = -0.5f + rightwall.transform.position.x - rightwall.GetComponent<BoxCollider>().size.x / 2;
			float zmax = -0.5f + upwall.transform.position.z - upwall.GetComponent<BoxCollider>().size.z / 2;
			float zmin = 0.5f + downwall.transform.position.z + downwall.GetComponent<BoxCollider>().size.z / 2;
			//计算四个碰撞体的法向量
			Vector3 xminF = new Vector3(1, 0, 0);
			Vector3 xmaxF = new Vector3(-1, 0, 0);
			Vector3 zminF = new Vector3(0, 0, 1);
			Vector3 zmaxF = new Vector3(0, 0, -1);
			//预测下一帧的位置，及时做出反应，防止因小球速度过快，碰撞后出现穿模
			Vector3 tempV = preV + Time.deltaTime * acceleration;
			Vector3 temppos = pos;
			temppos.x += ((tempV.x + preV.x) * Time.deltaTime / 2);
			temppos.z += ((tempV.z + preV.z) * Time.deltaTime / 2);
			//用Reflect函数实现完全弹性碰撞
			if (temppos.x <= xmin)
			{
				Vector3 v1 = Vector3.Reflect(preV, xminF);
				preV = v1;
				Debug.Log("白球撞左墙!");
			}
			if (temppos.x >= xmax)
			{
				Vector3 v1 = Vector3.Reflect(preV, xmaxF);
				preV = v1;
				Debug.Log("白球撞右墙!");
			}
			if (temppos.z <= zmin)
			{
				Vector3 v1 = Vector3.Reflect(preV, zminF);
				preV = v1;
				Debug.Log("白球撞下墙!");
			}
			if (temppos.z >= zmax)
			{
				Vector3 v1 = Vector3.Reflect(preV, zmaxF);
				preV = v1;
				Debug.Log("白球撞上墙!");
			}
		}
	}
}

