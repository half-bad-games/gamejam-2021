using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCenterGravity : MonoBehaviour
{
    private bool isPlayer;
    private float thrust = 4000;
    private float torque = 500;
    public GameObject playerObject;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if ((this.playerObject != null) && this.gameObject.GetInstanceID() != this.playerObject.gameObject.GetInstanceID())
        {
            this.isPlayer = false;

            this.rb.AddForce(
                new Vector2(Random.Range(-this.thrust, this.thrust), Random.Range(-this.thrust, this.thrust))
            );
            this.rb.AddTorque(Random.Range(-this.torque, this.torque));
        }
        else
        {
            this.isPlayer = true;
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
