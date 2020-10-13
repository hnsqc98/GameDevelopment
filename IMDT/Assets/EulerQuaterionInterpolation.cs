using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InterpolationType
{
    Euler,
    QuaternionSimple,
    QuaternionShortest,
    QuaternionSlerp,
}

[ExecuteInEditMode]
public class EulerQuaterionInterpolation : MonoBehaviour
{
    public InterpolationType type;
    [Range(0, 1)]
    public float t;

    [Range(-180, 180)]
    public float euler1X;
    [Range(-180, 180)]
    public float euler1Y;
    [Range(-180, 180)]
    public float euler1Z;


    [Range(-180, 180)]
    public float euler2X;
    [Range(-180, 180)]
    public float euler2Y;
    [Range(-180, 180)]
    public float euler2Z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(type)
        {
            case InterpolationType.Euler:
                {
                    Vector3 euler1 = new Vector3(euler1X, euler1Y, euler1Z);
                    Vector3 euler2 = new Vector3(euler2X, euler2Y, euler2Z);
                    Vector3 euler = euler1 * (1 - t) + euler2 * t;
                    transform.rotation = Quaternion.Euler(euler);
                    break;
                }
            case InterpolationType.QuaternionSimple:
                {
                    Quaternion q1 = Quaternion.Euler(euler1X, euler1Y, euler1Z);
                    Quaternion q2 = Quaternion.Euler(euler2X, euler2Y, euler2Z);
                    Quaternion q = SimpleLerp(q1, q2, t);
                    transform.rotation = q;
                    break;
                }
            case InterpolationType.QuaternionShortest:
                {
                    Quaternion q1 = Quaternion.Euler(euler1X, euler1Y, euler1Z);
                    Quaternion q2 = Quaternion.Euler(euler2X, euler2Y, euler2Z);
                    if(Quaternion.Dot(q1, q2)<0)
                    {
                        q2.Set(-q2.x, -q2.y, -q2.z, -q2.w);
                    }
                    Quaternion q = SimpleLerp(q1, q2, t);
                    transform.rotation = q;
                    break;
                }
            case InterpolationType.QuaternionSlerp:
                {
                    Quaternion q1 = Quaternion.Euler(euler1X, euler1Y, euler1Z);
                    Quaternion q2 = Quaternion.Euler(euler2X, euler2Y, euler2Z);
                    Quaternion q = Quaternion.Slerp(q1, q2, t);
                    transform.rotation = q;
                    break;
                }
        }
       

    }


    static Quaternion SimpleLerp(Quaternion q1, Quaternion q2, float t)
    {
        Quaternion tmpQuat = new Quaternion();
        tmpQuat.Set(q1.x + t* (q2.x - q1.x),
                q1.y + t* (q2.y - q1.y),
                q1.z + t* (q2.z - q1.z),
                q1.w + t* (q2.w - q1.w));
        
        return tmpQuat.normalized;
    }
}
