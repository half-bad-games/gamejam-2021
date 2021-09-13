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

            Vector2 pos = Camera.main.ViewportToWorldPoint(randomSpawn(-1.2f, -1.2f, 1.2f, 1.2f));

            GameObject spawnedObject = Instantiate(spawnObject, pos, Quaternion.identity);
            spawnedObject.name = spawnedObject.GetInstanceID().ToString();

            var obj = GameObject.Find(camera.GetComponent<CameraFollow>().playerId);

            if (obj != null)
            {
                Player player = obj.GetComponent<Player>();
            
                Enemy enemy = spawnedObject.GetComponent<Enemy>();
                enemy.size = Random.Range(0, player.size + 2);
                if (enemy.size > 0)
                {
                    enemy.xpGains *= enemy.size;
                }

                spawnedObject.transform.localScale = new Vector3(enemy.size,enemy.size,enemy.size);
            }

            var p = new PlayableAdapterComponent(spawnedObject.name);
            var baseComponent = spawnedObject.GetComponent<Playable>();
            new EnemyDecoratorFactory(p, baseComponent)
                .generate(3)
                .extend();

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private Vector2 randomSpawn(float outerSpawnMinX, float outerSpawnMinY, float outerSpawnMaxX, float outerSpawnMaxY)
    {
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
