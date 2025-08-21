using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectsInfo> ObjectPools = new List<PooledObjectsInfo>();
    public static GameObject SpawnPooledObject(GameObject gameObjectPrefab, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // Check if the pool for this prefab exists with the lookup string and prefab name
        PooledObjectsInfo poolInfo = ObjectPools.Find(pool => pool.lookupString == gameObjectPrefab.name);

        // If the pool isn't existing, create a new one
        if (poolInfo == null)
        {
            poolInfo = new PooledObjectsInfo();
            poolInfo.lookupString = gameObjectPrefab.name;
            ObjectPools.Add(poolInfo);
        }

        // Check if there are any inactive objects in the pool
        GameObject spawnableObject = poolInfo.inactiveObjects.FirstOrDefault();

        // If there are inactive objects, create a new one
        if (spawnableObject == null)
        {
            spawnableObject = Instantiate(gameObjectPrefab, spawnPosition, spawnRotation);
        }
        else
        {
            // If there is an inactive object, activate it and set its position and rotation
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            // Remove it from the inactive list
            poolInfo.inactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        // Return the spawned object
        return spawnableObject;
    }

    public static void ReturnPooledObject(GameObject gameObjectPrefab)
    {
        // As pool instantiate will include the (clone), we need to find the original prefab name to match the pool lookup string
        string pooledObjectPrefab = gameObjectPrefab.name.Replace("(Clone)", "").Trim();
        PooledObjectsInfo poolInfo = ObjectPools.Find(pool => pool.lookupString == pooledObjectPrefab);

        // If pool isn't existing, log an error
        if (poolInfo == null)
        {
            Debug.LogError($"Object pool for {pooledObjectPrefab} does not exist.");
            return;
        }
        else
        {
            // Deactivate the object and add it to the inactive list
            gameObjectPrefab.SetActive(false);
            poolInfo.inactiveObjects.Add(gameObjectPrefab);
        }

    }
}

public class PooledObjectsInfo
{
    public string lookupString;
    public List<GameObject> inactiveObjects = new List<GameObject>();
}
