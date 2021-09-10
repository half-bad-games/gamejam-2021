using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countEnemy : MonoBehaviour
{
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;
    }

    public int getEnemyCount() { return enemyCount; }
}
