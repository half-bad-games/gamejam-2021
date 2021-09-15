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

            var thrust = comp.stats.size * Mathf.Pow(comp.stats.speed, 1.5f);
            var torque = comp.stats.size * Mathf.Pow((comp.stats.speed / 3f), 1.5f);

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
            if (player.stats.size >= enemy.stats.size)
            {
                player.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            }
        }
        else
        {
            if (player.stats.size <= enemy.stats.size)
            {
                enemy.IncreaseCurrentXP(enemy.xpGains);
                Destroy(other.gameObject);
            }
        }
    }
}
