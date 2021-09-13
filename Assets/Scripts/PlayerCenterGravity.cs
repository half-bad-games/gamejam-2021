using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCenterGravity : MonoBehaviour
{
    private bool isPlayer;
    public GameObject playerObject;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if ((this.playerObject != null) && this.gameObject.GetInstanceID() != this.playerObject.gameObject.GetInstanceID())
        {
            this.isPlayer = false;

            var comp = this.gameObject.GetComponent<Playable>();

            var thrust = 30f * comp.stats.speed;
            var torque = 10f * comp.stats.speed;

            this.rb.AddForce(
                new Vector2(Random.Range(-thrust, thrust), Random.Range(-thrust, thrust))
            );
            this.rb.AddTorque(Random.Range(-torque, torque));
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
