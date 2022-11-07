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
        var playerColor = player.GetComponent<PlayerChangeColor>()._currColor;
        if (PlayerInBaseCube(dist))
        {
            CutterLogic(target, dist, playerColor);
        }
        else
        {
            CreateCube(ObjectType.DynamicCube, player.transform.position, player.transform.localScale, playerColor);
            GameController.s_Instance.EndGame();
        }
    }

    public bool PlayerInBaseCube(in Vector3 dist)
    {
        return (Mathf.Abs(dist.x) < player.transform.localScale.x  && Mathf.Abs(dist.z) < player.transform.localScale.z);
    }

    public void CutterLogic(in Vector3 target, in Vector3 dist, in Color color)
    {
        var loc = dist / 2;
        loc.y = 0.1f;
        loc += target;
        var scale = player.transform.localScale - Utils.VectorAbs(dist);

        CreateCube(ObjectType.StaticCube, loc, scale, color);

        if (dist.x != 0)
        {
            var pos = new Vector3(dist.x / 2 + scale.x / 2 * Mathf.Sign(dist.x) + loc.x, loc.y, target.z);
            var scaleDynamic = new Vector3(Mathf.Abs(dist.x), _lenght, scale.z);
            CreateCube(ObjectType.DynamicCube, pos , scaleDynamic, color);
        }

        if (dist.z != 0)
        {
            var pos = new Vector3(target.x, loc.y, dist.z / 2 + scale.z / 2 * Mathf.Sign(dist.z) + loc.z);
            var scaleDynamic = new Vector3(scale.x, 0.1f, Mathf.Abs(dist.z));
            CreateCube(ObjectType.DynamicCube, pos, scaleDynamic, color);
        }

        player.transform.localScale = scale;
        GameController.s_Instance._target = loc;
    }

    public void CreateCube(in ObjectType objectType, in Vector3 transform, in Vector3 scale, in Color color)
    {
        var obj = ObjectPool.s_Instance.GetObject(objectType);
        obj.transform.localScale = scale;
        obj.transform.position = transform;
        obj.GetComponent<Renderer>().material.color = color;
        obj.SetActive(true);
    }
}
