using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnPrefabOnImage : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    [System.Serializable]
    public struct ImagePrefabPair
    {
        public string imageName;
        public GameObject prefabToSpawn;
    }

    public List<ImagePrefabPair> imagePrefabPairs = new List<ImagePrefabPair>();
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Touch input detected: " + Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject touchedObject = hit.collider.gameObject;
                Debug.Log("Raycast hit: " + touchedObject.name);

                if (spawnedPrefabs.ContainsValue(touchedObject))
                {
                    Debug.Log("Removing prefab: " + touchedObject.name);
                    RemovePrefabForImage(touchedObject);
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object.");
            }

            // Debug Visualization
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.green, 2f);
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            SpawnPrefabForImage(newImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (spawnedPrefabs.ContainsKey(updatedImage.referenceImage.name))
            {
                var prefab = spawnedPrefabs[updatedImage.referenceImage.name];
                prefab.transform.position = updatedImage.transform.position;
                prefab.transform.rotation = updatedImage.transform.rotation;
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            RemovePrefabForImage(removedImage);
        }
    }

    private void SpawnPrefabForImage(ARTrackedImage trackedImage)
    {
        ImagePrefabPair pair = imagePrefabPairs.Find(p => p.imageName == trackedImage.referenceImage.name);
        if (pair.prefabToSpawn != null)
        {
            GameObject prefab = Instantiate(pair.prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedPrefabs.Add(trackedImage.referenceImage.name, prefab);
            Debug.Log("Spawned prefab for image: " + trackedImage.referenceImage.name);
        }
    }

    private void RemovePrefabForImage(ARTrackedImage trackedImage)
    {
        if (spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            Destroy(spawnedPrefabs[trackedImage.referenceImage.name]);
            spawnedPrefabs.Remove(trackedImage.referenceImage.name);
            Debug.Log("Removed prefab for image: " + trackedImage.referenceImage.name);
        }
    }

    private void RemovePrefabForImage(GameObject prefabToRemove)
    {
        string keyToRemove = spawnedPrefabs.GetKeyByValue(prefabToRemove);
        if (!string.IsNullOrEmpty(keyToRemove))
        {
            Destroy(prefabToRemove);
            spawnedPrefabs.Remove(keyToRemove);
            Debug.Log("Removed prefab for image: " + keyToRemove);
        }
    }
}

public static class DictionaryExtensions
{
    public static TKey GetKeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value)
    {
        foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
        {
            if (EqualityComparer<TValue>.Default.Equals(kvp.Value, value))
            {
                return kvp.Key;
            }
        }

        return default(TKey);
    }
}
