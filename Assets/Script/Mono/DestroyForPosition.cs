using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyForPosition : MonoBehaviour
{
    public float _y;
    public ObjectType _objectType;
    private void Update()
    {
        if (transform.position.y < _y)
        {
            ObjectPool.s_Instance.SetObject(_objectType, this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
