using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{
    public float bullet_damage;
    public float hspeed;
    public float vspeed;
    Vector3 bulletPosition;
    private float bulletTime;
    public List<int> arr = new List<int>();
    private float bulletLife;

    public player_movement player_script;

    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.Find("player").GetComponent < player_movement > ();
        hspeed = player_script.bulletHspeed;
        vspeed = player_script.bulletVspeed;
        //player_script.setCurrentWeapon(1);

        // ############### Weapon Config ###############
        switch (player_script.getCurrentWeapon())
        {
            case 0:
                bulletLife = 0.75f;
                player_script.setBulletSpeed(150.0f);
                player_script.setRpm(0.1f);
                player_script.setWeaponSpread(1);
                bullet_damage = 50;
                break;
            case 1:
                bulletLife = 0.45f;
                player_script.setBulletSpeed(150.0f);
                player_script.setRpm(0.8f);
                player_script.setWeaponSpread(5);
                bullet_damage = 20;
                break;
            case 2:
                bulletLife = 0.75f;
                player_script.setBulletSpeed(150.0f);
                player_script.setRpm(0.15f);
                player_script.setWeaponSpread(1);
                bullet_damage = 20;
                break;
            default:
                bulletLife = 1.0f;
                player_script.setBulletSpeed(300.0f);
                player_script.setRpm(0.5f);
                player_script.setWeaponSpread(1);
                bullet_damage = 20;
                break;
        }
        // ############### Weapon Config ###############

        bulletTime = 0.0f;
        arr.Add(1);
    }

    // Update is called once per frame
    void Update()
    {
        // Set render layer to z.axis
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1000;

        bulletPosition = new Vector3(hspeed*150, 0, vspeed*150);
        transform.Translate(bulletPosition * Time.deltaTime);

        if (arr.Count > 1)
        {
            bulletTime += Time.deltaTime;

            if (bulletTime > bulletLife)
                Destroy(gameObject);
        }
    }
}
