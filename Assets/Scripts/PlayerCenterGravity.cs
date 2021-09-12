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
        if (gameObject.GetInstanceID() != player.gameObject.GetInstanceID())
        {
            isPlayer = false;
            Vector2 thrust = new Vector2(Random.Range(-this.thrust, this.thrust), Random.Range(-this.thrust, this.thrust));
            float torque = Random.Range(-this.torque, this.torque);
        
            rb.AddForce(thrust);
            rb.AddTorque(torque);
        }
        else
        {
            isPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            var modelPosition = transform.position;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var delta_x = modelPosition.x - mousePosition.x;
            var delta_y = modelPosition.y - mousePosition.y;

            var delta_x_norm = delta_x / (Mathf.Sqrt(delta_x * delta_x + delta_y * delta_y));
            var delta_y_norm = delta_y / (Mathf.Sqrt(delta_x * delta_x + delta_y * delta_y));
            var player_angle = (Mathf.Atan2(delta_y_norm, delta_x_norm) * (Mathf.Rad2Deg));

            // GameObject parent = transform.parent.gameObject;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, player_angle + 90));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("AM TRIGGERING");
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
        else
        {
            
        }
    }
}
