using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 target;

    public Vector3 _target
    {
        get { return target; }
        set 
        { 
            target = value;
            dir = target.normalized;
        }
    }

    public float _speed;
    Vector3 dir;

    private void Start()
    {
        dir = _target.normalized;
    }
    void Update()
    {
        transform.position += _speed * Time.deltaTime * dir;
    }

    public Vector3 CalculateDir(in Vector3 target, in Vector3 me)
    {
        var loc = target - me;
        return Vector3.Normalize(loc);
    }
}
