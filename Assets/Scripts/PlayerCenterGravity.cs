using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCenterGravity : MonoBehaviour
{
    private bool isPlayer;
    private float thrust = 500;
    private float torque = 500;
    public GameObject player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
            Vector2 thrust = new Vector2(Random.Range(-this.thrust, this.thrust), Random.Range(-this.thrust, this.thrust));
            float torque = Random.Range(-this.torque, this.torque);
        
            rb.AddForce(thrust);
            rb.AddTorque(torque);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player;
        Enemy enemy;
        if (isPlayer)
        {
            player = gameObject.GetComponent<Player>();
            enemy = other.gameObject.GetComponent<Enemy>();
        
            if ((player != null && enemy != null) && (player.size > enemy.size))
            {
                player.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            }
        }
    }
}
