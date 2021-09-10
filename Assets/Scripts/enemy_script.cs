using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy_script : MonoBehaviour
{
    public float hspeed;
    public float vspeed;
    public float walking_speed;
    public GameObject player;
    public float player_offset_x;
    public float player_offset_y;
    private float hp;
    // variable to hold a reference to our SpriteRenderer component
    public Rigidbody rb;
    public int maxEnemy;

    public countEnemy count_enemy;

    // Start is called before the first frame update
    void Start()
    {
        count_enemy = GameObject.Find("Main Camera").GetComponent<countEnemy>();
        
        player = GameObject.Find("player");
        walking_speed = Random.Range(10f, 50f);
        hp = 100;
        player_offset_x = 2;
        player_offset_y = 0.0f;
        rb = GetComponent<Rigidbody>();

        maxEnemy = 40;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.name == "enemy(Clone)")
        {
            // Set render layer to z.axis
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z;

            // Follow enemy
            if (transform.position.x < player.transform.position.x + player_offset_x)
            {
                if (hspeed < walking_speed)
                {
                    hspeed += walking_speed / 10;
                }
            }
            else
            if (transform.position.x > player.transform.position.x + player_offset_x)
            {
                if (hspeed > -walking_speed)
                {
                    hspeed -= walking_speed / 10;
                }
            }
            else
            {
                hspeed = 0;
            }

            if (transform.position.z < player.transform.position.z + player_offset_y)
            {
                if (vspeed < walking_speed)
                {
                    vspeed += walking_speed / 10;
                }
            }
            else
            if (transform.position.z > player.transform.position.z + player_offset_y)
            {
                if (vspeed > -walking_speed)
                {
                    vspeed -= walking_speed / 10;
                }
            }
            else
            {
                vspeed = 0;
            }

            if ((hp <= 0 && gameObject.name == "enemy(Clone)") || count_enemy.getEnemyCount() > maxEnemy)
            {
                Destroy(gameObject);
            }

            GameObject.Find("enemies_on_screen").GetComponent<UnityEngine.UI.Text>().text = count_enemy.getEnemyCount().ToString() + "/40";
            
            if (count_enemy.getEnemyCount() > 40)
            {
                SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
            }

            rb.velocity = new Vector3(hspeed, 0, vspeed);
        }
    }

    //void OnCollisionEnter(Collision col)
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Bullet(Clone)")
        {
            hp -= GameObject.Find("Bullet(Clone)").GetComponent<bullet_movement>().bullet_damage;
            Destroy(col.gameObject);
        }
    }
}
