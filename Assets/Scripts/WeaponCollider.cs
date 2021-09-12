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
            Debug.Log("Player has LightSaber");
            isPlayer = true;
        }
        else
        {
            // Debug.Log("Enemy has LightSaber");
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
            Debug.Log("Player TRIGGERING");
            Enemy enemy;
            enemy = other.gameObject.GetComponent<Enemy>();

            Debug.Log("p " + player);
            if ((player != null && enemy != null) && (player.size > enemy.size))
            {
                Debug.Log("ASDF");
                player.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            } else if (false)
            {
                
            }
        }
        else
        {
            // Debug.Log("Enemy TRIGGERING");
            Player player = playerObject.GetComponent<Player>();
            Debug.Log("Player TRIGGERING");
            Enemy enemy;
            enemy = other.gameObject.GetComponent<Enemy>();

            Debug.Log("p " + player);
            if ((player != null && enemy != null) && (player.size < enemy.size))
            {
                Debug.Log("ASDF");
                enemy.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            } else if (false)
            {
                
                
            }
        }
    }
}
