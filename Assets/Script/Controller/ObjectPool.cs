using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    StaticCube,
    DynamicCube
}

[System.Serializable]
public struct ObjectArray
{
    public ObjectType _objectType;
    public int _count;
    public GameObject _prefab;
}

public class ObjectPool : Singleton<ObjectPool> , IDebug
{
    [SerializeField] public ObjectArray[] _objects;

    Dictionary<ObjectType, Stack<GameObject>> _objectPool;
    Dictionary<ObjectType, GameObject> _objectPrefab;

    List<GameObject> _roots = new List<GameObject>(); 

    [SerializeField] bool debug;
    public bool Debug { get => debug; }

    public override void Awake()
    {
        _objectPool = new Dictionary<ObjectType, Stack<GameObject>>();
        _objectPrefab = new Dictionary<ObjectType, GameObject>();
        base.Awake();
        foreach (var obj in _objects)
        {
            Stack<GameObject> gameObjects = new Stack<GameObject>();
            var root = new GameObject();
            _roots.Add(root);
            root.transform.parent = this.gameObject.transform;
            root.name = obj._objectType.ToString();
            for (int i = 0; i < obj._count; i++)
            {
                var gObj = Instantiate(obj._prefab, root.transform);
                gObj.SetActive(false);
                gameObjects.Push(gObj);
            }
            if (_objectPool.ContainsKey(obj._objectType))
                Utils.LogError("2 kere ayný obje türünü eklemeye çalýþtýn.", this);
            else
            {
                _objectPrefab.Add(obj._objectType, obj._prefab);
                _objectPool.Add(obj._objectType, gameObjects);
            }
        }
    }

    public GameObject GetObject(ObjectType objectType)
    {
        if (_objectPool[objectType].Count > 0)
        {
            var obj = _objectPool[objectType].Pop();
            return obj;
        }
        else
        {
            SetObject(objectType, Instantiate<GameObject>(_objectPrefab[objectType], _roots[(int)objectType].transform));
            return GetObject(objectType);
        }
    }

    public void SetObject(ObjectType objectType, GameObject gameObject)
    {
        _objectPool[objectType].Push(gameObject);
    }
}
