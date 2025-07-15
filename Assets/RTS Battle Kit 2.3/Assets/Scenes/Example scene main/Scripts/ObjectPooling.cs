using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private int _startObjectCount = 50;
    private Transform _parent;
    private Stack<GameObject> _objects;
    private T _objectToPool;

    public ObjectPool(T objectToPool ,int startObjectCount, Transform parent)
    {
        _objectToPool = objectToPool;
        _startObjectCount = startObjectCount;
        _parent = parent;
        _objects = new Stack<GameObject>();

        for(int i = 0; i < _startObjectCount; i++)
        {
            AddNewObjectToPool();
        }
    }

    private void AddNewObjectToPool()
    {
        var newObj = InstantiateObject();
        _objects.Push(newObj);
    }

    public T GetFromPool()
    {
        if(_objects.Count == 0)
        {
            AddNewObjectToPool();
        }
        
        var obj = _objects.Pop();
        obj.SetActive(true);
        return obj.GetComponent<T>();
    }

    public void ReturnToPool(T obj)
    {
        if(obj == null) return;
        obj.transform.position = Vector3.zero;
        obj.gameObject.SetActive(false);
        _objects.Push(obj.gameObject);
    }

    private GameObject InstantiateObject()
    {
        var newObj = MonoBehaviour.Instantiate(_objectToPool, Vector3.zero,_objectToPool.transform.rotation, _parent.transform);
        newObj.gameObject.SetActive(false);
        
        return newObj.gameObject;
    }
    
}
