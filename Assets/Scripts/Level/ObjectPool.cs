using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private List<GameObject> _objectsToSpawn;
    [SerializeField] private int _amount;
    
    protected List<GameObject> _pooledObjects = new List<GameObject>();
    private float screenPosition;
    private const float Offset = 65f;

    protected void Initialize()
    {
        foreach (GameObject item in _objectsToSpawn)
        {
            for (int i = 0; i < _amount; i++)
            {
                GameObject spawned = Instantiate(item, _container.transform);
                spawned.SetActive(false);
                _pooledObjects.Add(spawned);
            }
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pooledObjects.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }

    protected bool TryGetRandomObject(out GameObject result)
    {
        var tempList = _pooledObjects.Where(p => p.activeSelf == false).ToList();
        result = tempList.ElementAtOrDefault(Random.Range(0, tempList.Count));
        return result != null;
    }

    protected void DisabelObjectAbroadScreen(Camera camera)
    {
        foreach (var item in _pooledObjects)
        {
            if (item.activeSelf == true)
            {
                screenPosition = camera.transform.position.x;
                if (screenPosition > item.transform.position.x + Offset)
                    item.SetActive(false);
            }
        }
    }
}