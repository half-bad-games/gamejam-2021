using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public float spawnRate;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject spawnObject;
    [SerializeField] public Camera camera;
    [SerializeField] public float mapX;
    [SerializeField] public float mapY;
    
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
            var vertExtent = camera.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;
    
            var xMin = horzExtent - mapX / 2.0f;
            var xMax = mapX / 2.0f - horzExtent;
            var yMin = vertExtent - mapY / 2.0f;
            var yMax = mapY / 2.0f - vertExtent;

            var screenX = Random.Range(xMin, xMax);
            var screenY = Random.Range(yMin, yMax);
            Vector2 pos = new Vector2(screenX, screenY);

            GameObject spawnedObject = Instantiate(spawnObject, pos, Quaternion.identity);
            spawnedObject.name = spawnedObject.GetInstanceID().ToString();
            dynamic spawnedObjectBaseComponent = spawnedObject.GetComponent(
                System.Type.GetType(spawnObject.name)
            );

            var p = new GameObjectAdapterComponent(spawnedObject.name, spawnedObjectBaseComponent.GetType());
            dynamic baseComponent = spawnedObject.GetComponent(System.Type.GetType(spawnObject.name));
            var comp = new DecoratorFactory(p, baseComponent).generate(10);
            var stats = comp.extend();
            baseComponent.health = stats.health;

            spawnedObject.transform.SetParent(this.gameArea.transform, false);
        
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void DestroyObjects()
    {
        
    }
}
