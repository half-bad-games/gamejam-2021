using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public float spawnRate;
    [SerializeField] public GameObject spawnObject;
    [SerializeField] public GameObject playerObject;
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

            // var screenX = Random.Range(xMin, xMax);
            // var screenY = Random.Range(yMin, yMax);
            //Vector2 pos = new Vector2(screenX, screenY);
            Vector3 a = randomSpawn(-1.1f, -1.1f, 1.1f, 1.1f);
            // var screenX = Random.Range(1.1f, 2.0f);
            // var screenY = Random.Range(1.1f, 2.0f);
            Vector2 pos = Camera.main.ViewportToWorldPoint(a);

            GameObject spawnedObject = Instantiate(spawnObject, pos, Quaternion.identity);
            spawnedObject.name = spawnedObject.GetInstanceID().ToString();
            dynamic spawnedObjectBaseComponent = spawnedObject.GetComponent(
                System.Type.GetType(spawnObject.name)
            );
            var obj = GameObject.Find(camera.GetComponent<CameraFollow>().playerId);
            Enemy enemy = spawnedObject.GetComponent<Enemy>();
            Player player = obj.GetComponent<Player>();
            enemy.size = Random.Range(player.size - 5, player.size + 5);
            if (enemy.size > 0)
            {
                enemy.xpGains *= enemy.size;
            }
            
            Vector3 local = transform.localScale;
            spawnedObject.transform.localScale = new Vector3(enemy.size,enemy.size,enemy.size);

            var p = new GameObjectAdapterComponent(spawnedObject.name, spawnedObjectBaseComponent.GetType());
            dynamic baseComponent = spawnedObject.GetComponent(System.Type.GetType(spawnObject.name));
            var comp = new DecoratorFactory(p, baseComponent).generate(10);
            var stats = comp.extend();
            baseComponent.stats = stats;

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private Vector2 randomSpawn(float outerSpawnMinX, float outerSpawnMinY, float outerSpawnMaxX, float outerSpawnMaxY)
    {
        Vector2 vector;
        float x, y;
        // top/bottom (true) or left/right (false)
        if (Random.Range(0, 2) == 1)
        {
            // set x position randomly
            x = Random.Range(outerSpawnMinX, outerSpawnMaxX);
 
            // spawn on top (true) or bottom (false)
            if (Random.Range(0, 2) == 1) {
                // set the z position to top
                y = outerSpawnMaxY;
 
            } else {
                // set the z position to bottom
                y = outerSpawnMinY;
 
            }
 
        } else {
            // set z position randomly
            y = Random.Range(outerSpawnMinY, outerSpawnMaxY);
 
            // spawn on left (true) or right (false)
            if (Random.Range(0, 2) == 1) {
                // set the x position to left
                x = outerSpawnMinX;
 
            } else {
                // set the x position to right
                x = outerSpawnMaxX;
            }
        }

        return new Vector2(x, y);
    }
}
