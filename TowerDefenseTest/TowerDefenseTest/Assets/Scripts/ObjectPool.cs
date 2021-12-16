using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Fields
    static List<GameObject> pooledObjects;
    [SerializeField]
    GameObject objectToPool;
    [SerializeField]
    int amountToPool;
    #endregion
    #region Unity methods
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }
    #endregion
    #region Public Methods
    public static GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) return pooledObjects[i];
        }
        return null;
    }

    public static void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    #endregion
}