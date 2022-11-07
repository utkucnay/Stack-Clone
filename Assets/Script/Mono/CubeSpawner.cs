using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour, IClickScreen
{
    public GameObject player;
    float _lenght = 0.1f;

    public void ClickScreen()
    {
        var target = GameController.s_Instance._target;
        var dist = player.transform.position - target;
        dist.y = 0;

        if (InPlayerBaseCube(dist))
        {
            CutterLogic(target, dist);
        }
        else
        {
            CreateCube(ObjectType.DynamicCube, player.transform.position, player.transform.localScale);
            GameController.s_Instance.EndGame();
        }
    }

    public bool InPlayerBaseCube(Vector3 dist)
    {
        return (Mathf.Abs(dist.x) < player.transform.localScale.x  && Mathf.Abs(dist.z) < player.transform.localScale.z);
    }

    public void CutterLogic(Vector3 target, Vector3 dist)
    {
        var loc = dist / 2;
        loc.y = 0.1f;
        loc += target;
        var scale = player.transform.localScale - Utils.VectorAbs(dist);
        CreateCube(ObjectType.StaticCube, loc, scale);

        if (dist.x != 0)
        {
            CreateCube(ObjectType.DynamicCube, new Vector3(dist.x / 2 + scale.x / 2 * Mathf.Sign(dist.x) + loc.x, loc.y, target.z), new Vector3(Mathf.Abs(dist.x), _lenght, scale.z));
        }

        if (dist.z != 0)
        {
            CreateCube(ObjectType.DynamicCube, new Vector3(target.x, loc.y, dist.z / 2 + scale.z / 2 * Mathf.Sign(dist.z) + loc.z), new Vector3(scale.x, 0.1f, Mathf.Abs(dist.z)));
        }

        player.transform.localScale = scale;
        GameController.s_Instance._target = loc;
    }

    public void CreateCube(ObjectType objectType, in Vector3 transform, in Vector3 scale)
    {
        var obj = ObjectPool.s_Instance.GetObject(objectType);
        obj.transform.localScale = scale;
        obj.transform.position = transform;
        obj.SetActive(true);
    }
}
