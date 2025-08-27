using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get;  private set; }
    public static List<PooledObjectsInfo> ObjectPools = new List<PooledObjectsInfo>();

    private GameObject _SpawnedObjectsContainer;
    private static GameObject _SceneObjectsContainer;
    private static GameObject _InGameObjectsContainer;
    private static GameObject _PlayerObjectsContainer;
    private static GameObject _ManagerObjectsContainer;

    public enum ObjectPoolType
    {
        SceneObjects,
        InGameObjects,
        PlayerObject,
        ManagerObject
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Create the main container for all spawned objects to organize the hierarchy
            _SpawnedObjectsContainer = new GameObject("SpawnedObjectsContainer");

            _SceneObjectsContainer = new GameObject("SceneObjectsContainer");
            _SceneObjectsContainer.transform.SetParent(_SpawnedObjectsContainer.transform);

            _InGameObjectsContainer = new GameObject("InGameObjectsContainer");
            _InGameObjectsContainer.transform.SetParent(_SpawnedObjectsContainer.transform);

            _PlayerObjectsContainer = new GameObject("PlayerObjectsContainer");
            _PlayerObjectsContainer.transform.SetParent(_SpawnedObjectsContainer.transform);

            _ManagerObjectsContainer = new GameObject("ManagerObjectsContainer");
            _ManagerObjectsContainer.transform.SetParent(_SpawnedObjectsContainer.transform);
        } else
        {
            Destroy(gameObject);
        }
    }

    public static GameObject SpawnPooledObject(GameObject gameObjectPrefab, Vector3 spawnPosition, Quaternion spawnRotation, ObjectPoolType objectPoolType)
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
            // Find the parent object based on the pool type
            GameObject parentObject = SetParentObject(objectPoolType);
            // Instantiate a new object from the prefab
            spawnableObject = Instantiate(gameObjectPrefab, spawnPosition, spawnRotation);

            // Ensure the object is active after instantiation
            spawnableObject.SetActive(false);
            // If the parent object is not null, set it as the parent of the new object
            if (parentObject != null)
            {
                spawnableObject.transform.SetParent(parentObject.transform);
            }

            // Activate the object
            spawnableObject.SetActive(true);
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

    private static GameObject SetParentObject(ObjectPoolType objectPoolType)
    {
        switch (objectPoolType)
        {
            case ObjectPoolType.SceneObjects:
                return _SceneObjectsContainer;
            case ObjectPoolType.InGameObjects:
                return _InGameObjectsContainer;
            case ObjectPoolType.PlayerObject:
                return _PlayerObjectsContainer;
            case ObjectPoolType.ManagerObject:
                return _ManagerObjectsContainer;
            default:
                Debug.LogError("Invalid ObjectPoolType specified.");
                return null;
        }
    }
}

public class PooledObjectsInfo
{
    public string lookupString;
    public List<GameObject> inactiveObjects = new List<GameObject>();
}
