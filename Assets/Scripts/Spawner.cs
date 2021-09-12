using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public float spawnRate;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject spawnObject;
    [SerializeField] public GameObject gameArea;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            MeshCollider mesh = gameArea.GetComponent<MeshCollider>();
            float screenX, screenY;
            screenX = Random.Range(mesh.bounds.min.x, mesh.bounds.max.x);
            screenY = Random.Range(mesh.bounds.min.y, mesh.bounds.max.y);
            Vector2 pos = new Vector2(screenX, screenY);

            GameObject spawnedObject = Instantiate(spawnObject, pos, Quaternion.identity);
            spawnedObject.transform.parent = gameArea.transform;
        
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void DestroyObjects()
    {
        
    }
}
