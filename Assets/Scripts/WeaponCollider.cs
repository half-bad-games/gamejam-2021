using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private bool isPlayer;
    private GameObject playerObject;
    //private GameObject playerObject;
    
    // Start is called before the first frame update
    void Start()
    {
        var camera = GameObject.Find("Main Camera");
        playerObject = GameObject.Find(camera.GetComponent<CameraFollow>().playerId);
        if (playerObject != null && (transform.parent.gameObject.name == playerObject.gameObject.name))
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayer)
        {
            Player player = playerObject.GetComponent<Player>();
            Enemy enemy;
            enemy = other.gameObject.GetComponent<Enemy>();

            if ((player != null && enemy != null) && (player.size > enemy.size))
            {
                player.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            } else if (false)
            {
                
            }
        }
        else
        {
            Player player = playerObject.GetComponent<Player>();
            Enemy enemy;
            enemy = other.gameObject.GetComponent<Enemy>();

            if ((player != null && enemy != null) && (player.size < enemy.size))
            {
                enemy.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            } else if (false)
            {
                
                
            }
        }
    }
}
