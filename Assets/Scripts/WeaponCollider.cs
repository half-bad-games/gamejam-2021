using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private bool isPlayer;
    private GameObject playerObject;
    
    // Start is called before the first frame update
    void Start()
    {
        var camera = GameObject.Find("Main Camera");
        this.playerObject = GameObject.Find(camera.GetComponent<CameraFollow>().playerId);

        if (playerObject != null && (this.transform.parent.gameObject.name == this.playerObject.gameObject.name))
        {
            this.isPlayer = true;
        }
        else
        {
            this.isPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerObject == null || other == null)
        {
            return;
        }

        Player player = playerObject.GetComponent<Player>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (player == null || enemy == null)
        {
            return;
        }

        if (this.isPlayer)
        {
            if (player.size >= enemy.size)
            {
                player.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            }
        }
        else
        {
            if (player.size <= enemy.size)
            {
                enemy.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            }
        }
    }
}
