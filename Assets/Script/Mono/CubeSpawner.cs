using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour, IClickScreen
{
    public GameObject player;
    float _lenght = 0.1f;

    private void Awake()
    {

    }

    public void ClickScreen()
    {
        CutterLogic();
    }

    public void CutterLogic()
    {
        var target = GameController.s_Instance._target;
        var dist = player.transform.position - target;

        if (dist.magnitude > player.transform.localScale.magnitude / 2)
        {
            var obj2 = ObjectPool.s_Instance.GetObject(ObjectType.DynamicCube);
            obj2.transform.position = player.transform.position;
            obj2.transform.localScale = player.transform.localScale;
            obj2.SetActive(true);
            player.SetActive(false);
            GameController.s_Instance.EndGame();
            return;
        }
        dist.y = 0;

        var obj = ObjectPool.s_Instance.GetObject(ObjectType.StaticCube);
        obj.transform.localScale = player.transform.localScale - Utils.VectorAbs(dist);
        var offset = dist / 2;
        offset.y = 0.1f;
        obj.transform.position = offset + target;
        obj.SetActive(true);

        if (dist.x != 0)
        {
            var obj2 = ObjectPool.s_Instance.GetObject(ObjectType.DynamicCube);
            obj2.transform.localScale = new Vector3(Mathf.Abs(dist.x), _lenght, obj.transform.localScale.z);
            obj2.transform.position = new Vector3(dist.x / 2 + obj.transform.localScale.x / 2 * Mathf.Sign(dist.x) + obj.transform.position.x, obj.transform.position.y, target.z);
            obj2.SetActive(true);
        }

        if (dist.z != 0)
        {
            var obj2 = ObjectPool.s_Instance.GetObject(ObjectType.DynamicCube);
            obj2.transform.localScale = new Vector3(obj.transform.localScale.x, obj2.transform.localScale.y, Mathf.Abs(dist.z));
            obj2.transform.position = new Vector3(target.x, obj.transform.position.y, dist.z / 2 + obj.transform.localScale.z / 2 * Mathf.Sign(dist.z) + obj.transform.position.z);
            obj2.SetActive(true);
        }

        player.transform.localScale = obj.transform.localScale;
        GameController.s_Instance._target = obj.transform.position;
        player.SetActive(false);
    }
}
