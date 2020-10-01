using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public GameObject player;  //主角
    public float smoothing = 5f;
    Vector3 offset;
    public float minPosx;  //相机不超过背景边界允许的最小值
    public float maxPosx;  //相机不超过背景边界允许的最大值
    public float minPosy;  //相机不超过背景边界允许的最小值
    public float maxPosy;  //相机不超过背景边界允许的最大值


    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        FixCameraPos();
    }

    void FixCameraPos()
    {
        float pPosX = player.transform.position.x;  //主角 x轴方向时实坐标值
        float cPosX = transform.position.x;             //相机 x轴方向时实坐标值
        float pPosY = player.transform.position.y;
        float cPosY = transform.position.y;

        if ((pPosX - cPosX > 3 || pPosX - cPosX < -3 || pPosY - cPosY > 1 || pPosY - cPosY < -1))    // 并不是死死地跟随，是相机和主角之间距离超过某个值时才跟随
        {
            Vector3 playercampos = player.transform.position + offset;
            if (playercampos.x > maxPosx) playercampos.x = maxPosx;
            if (playercampos.x < minPosx) playercampos.x = minPosx;
            // float realPosX = Mathf.Clamp(transform.position.x, minPosx, maxPosx);
            // float realPosY = Mathf.Clamp(transform.position.y, minPosy, maxPosy);
            // playercampos.x = realPosX;
            // playercampos.y = realPosY;
            transform.position = Vector3.Lerp(transform.position, playercampos, smoothing * Time.deltaTime);
        }
    }
}
