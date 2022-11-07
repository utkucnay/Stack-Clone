using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour, IClickScreen
{
    GameObject player;
    Vector3 target = Vector3.zero;
    float _lenght = 0.1f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ClickScreen()
    {
        var obj = ObjectPool.s_Instance.GetObject(ObjectType.StaticCube);
        var dist = player.transform.position - target;
        if (dist.magnitude > player.transform.localScale.magnitude / 2) 
        {
            var obj2 = ObjectPool.s_Instance.GetObject(ObjectType.DynamicCube);
            obj2.transform.position = player.transform.position;
            obj2.transform.localScale = player.transform.localScale;
            var movement = obj2.GetComponent<Movement>();
            movement._target = Vector3.down * 40;
            movement._speed = 9.8f;
            obj2.SetActive(true);
            player.SetActive(false);
            return;
        }
        dist.y = 0;
        obj.transform.localScale = player.transform.localScale;
        obj.transform.localScale -= Utils.VectorAbs(dist);
        var movepoint = dist / 2;
        movepoint.y = _lenght;
        obj.transform.position += movepoint;
        if (dist.x != 0)
        {
            var obj2 = ObjectPool.s_Instance.GetObject(ObjectType.DynamicCube);
            obj2.transform.position = player.transform.position;
            obj2.transform.localScale = player.transform.localScale;
            obj2.transform.localScale = new Vector3(dist.x , _lenght, obj2.transform.localScale.z);
            obj2.transform.position = new Vector3(dist.x / 2 + obj.transform.localScale.x / 2 * Mathf.Sign(dist.x) + obj.transform.position.x, _lenght, dist.z);
            var movement = obj2.GetComponent<Movement>();
            movement._target = Vector3.down * 40;
            movement._speed = 9.8f;
            obj2.SetActive(true);
        }
        if (dist.z != 0)
        {
            var obj2 = ObjectPool.s_Instance.GetObject(ObjectType.DynamicCube);
            obj2.transform.position = player.transform.position;
            obj2.transform.localScale = player.transform.localScale;
            obj2.transform.localScale = new Vector3(obj2.transform.localScale.x, obj2.transform.localScale.y, dist.z);
            obj2.transform.position = new Vector3(dist.x, _lenght, dist.z / 2 + obj.transform.localScale.z / 2 * Mathf.Sign(dist.x) + obj.transform.position.z);
            var movement = obj2.GetComponent<Movement>();
            movement._target = Vector3.down * 40;
            movement._speed = 9.8f;
            obj2.SetActive(true);
        }
        obj.SetActive(true);
        player.SetActive(false);
    }
}
