using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{
    // Bullet variables
    public float bulletHspeed;
    public float bulletVspeed;
    public float hspeed;
    public float vspeed;
    public float bullet_speed;
    public float walking_speed;
    public float rpm;
    public int weaponSpread;
    public enum weaponTypes { Gun, Shotgun, Machinegun };
    public int currentWeapon;
    public float bulletSync;
    public bool isFiring;
    public bool sprint;
    public bool run;
    public int rngX;
    public int rngZ;
    public int spawnRate;
    public float health;
    public float knockBack_hspeed;
    public float knockBack_vspeed;
    public bool potatoe_mode;

    // Enemy
    public float timer;
    public float spawnTimer;
    public GameObject enemy;
    private GameObject newEnemy;

    // variable to hold a reference to our SpriteRenderer component
    public Rigidbody rb;
    public Animator anim;

    public Timer_Script timer_script;

    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        bulletHspeed = 1.0f;
        bulletVspeed = 0.0f;
        bullet_speed = 0.0f;
        walking_speed = 100.0f;
        knockBack_hspeed = 0;
        knockBack_vspeed = 0;
        health = 100;
        rpm = 0.0f;
        bulletSync = 0.0f;
        weaponSpread = 0;
        isFiring = false;
        run = false;
        currentWeapon = (int)weaponTypes.Gun;
        rb = GetComponent<Rigidbody>();

        enemy = GameObject.Find("enemy");
        timer = 0.0f;
        spawnRate = 2;

        // timer_script = GameObject.Find("Timer").GetComponent<Timer_Script>();

        var p = new PlayerAdapterComponent(this);
        var comp = new DecoratorFactory(p).generate(10);
        stats = comp.extend();
        health = stats.health;
        
        // Debug.Log("Stats 1");
        // Debug.Log(stats.speed);
        // Debug.Log(stats.health);
        // Debug.Log(stats.damage);
        // Debug.Log(stats.agility);
        // Debug.Log("");

        // for (var i = 0; i < 200; i++)
        // {
        //     var p = new PlayerAdapterComponent(this);
        //     var comp = new DecoratorFactory(p).generate(10);
        //     var stats = comp.extend();
            
        //     Debug.Log("Stats " + i);
        //     Debug.Log(stats.speed);
        //     Debug.Log(stats.health);
        //     Debug.Log(stats.damage);
        //     Debug.Log(stats.agility);
        //     Debug.Log("");
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ############### Player Movement ###############
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        
        hspeed = walking_speed * h + knockBack_hspeed;
        vspeed = walking_speed * v + knockBack_vspeed;


        float tmpPushBackTime = 20;
        if (knockBack_hspeed > 0)
            knockBack_hspeed -= knockBack_hspeed / tmpPushBackTime;

        if (knockBack_hspeed < 0)
            knockBack_hspeed += -knockBack_hspeed / tmpPushBackTime;

        if (knockBack_vspeed > 0)
            knockBack_vspeed -= knockBack_vspeed / tmpPushBackTime;

        if (knockBack_vspeed < 0)
            knockBack_vspeed += -knockBack_vspeed / tmpPushBackTime;

        // ############### Player Movement ###############

        // ############### Player Animations 1 ###############
        anim.SetFloat("run", Mathf.Abs(h) + Mathf.Abs(v));
        //anim.SetBool("sprint", sprint);
        // ############### Player Animations 1 ###############


        // ############### Weapon Controls ###############
        if (sprint == false)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                bulletHspeed = hspeed * Time.deltaTime + bullet_speed;
                isFiring = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                bulletHspeed = hspeed * Time.deltaTime - bullet_speed;
                isFiring = true;
            }

            if (Input.GetKey(KeyCode.UpArrow) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            {

                bulletVspeed = vspeed * Time.deltaTime + bullet_speed;
                isFiring = true;
            }
            if (Input.GetKey(KeyCode.DownArrow) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            {
                bulletVspeed = vspeed * Time.deltaTime - bullet_speed;
                isFiring = true;
            }

            if ((!Input.GetKey(KeyCode.RightArrow)) && (!Input.GetKey(KeyCode.LeftArrow)))
            {
                bulletHspeed = hspeed * Time.deltaTime;
            }
            if ((!Input.GetKey(KeyCode.UpArrow)) && (!Input.GetKey(KeyCode.DownArrow)))
            {
                bulletVspeed = vspeed * Time.deltaTime;
            }

            if (bulletSync > rpm)
            {
                isFiring = false;
                bulletSync = 0.0f;
            }

            if (bulletSync == 0f)
            {
                if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.DownArrow)))
                {
                    bulletHspeed = bulletHspeed * Time.deltaTime;
                    bulletVspeed = bulletVspeed * Time.deltaTime;

                    if (isFiring)
                    {
                        if (getCurrentWeapon() == 1)
                        {
                            health -= 2;
                        }
                        else
                        {
                            health -= 4f;
                        }

                        if (currentWeapon == 1)
                        {
                            for (int i = 0; i < weaponSpread; i++)
                            {

                                GameObject newBullet = Instantiate(GameObject.Find("Bullet"));
                                if (i == 0)
                                    newBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                                else if (i == 1)
                                    newBullet.transform.position = new Vector3(transform.position.x + Random.Range(1, 3), transform.position.y + Random.Range(1, 3), transform.position.z + Random.Range(1, 10));
                                else if (i == 2)
                                    newBullet.transform.position = new Vector3(transform.position.x + Random.Range(1, 3), transform.position.y + Random.Range(1, 3), transform.position.z + Random.Range(1, 10));
                                else if (i == 3)
                                    newBullet.transform.position = new Vector3(transform.position.x + Random.Range(1, 3), transform.position.y + Random.Range(1, 3), transform.position.z + Random.Range(1, 10));
                                else if (i == 4)
                                    newBullet.transform.position = new Vector3(transform.position.x + Random.Range(1, 3), transform.position.y + Random.Range(1, 3), transform.position.z + Random.Range(1, 10));
                            }
                        }
                        else
                        {
                            GameObject newBullet = Instantiate(GameObject.Find("Bullet"));
                            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                        }
                    }
                }
                bulletSync = 0.0f;
            }

            // Wait time
            if (isFiring)
                bulletSync += Time.deltaTime;

        }
        // ############### Weapon Controls ###############

        // ############### Player Collision ###############
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject.Find("player_flip_channel").transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject.Find("player_flip_channel").transform.localScale = new Vector3(-1, 1, 1);
        }
        // ############### Player Collision ###############


        // ############### Player Sprint ###############
        if (potatoe_mode == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GameObject.Find("Body").GetComponent<SpriteRenderer>().flipX = true;
                GameObject.Find("Hat").GetComponent<SpriteRenderer>().flipX = false;
                GameObject.Find("Right Arm").GetComponent<SpriteRenderer>().flipX = true;
                GameObject.Find("Left Arm").GetComponent<SpriteRenderer>().flipX = true;
                GameObject.Find("Right Leg").GetComponent<SpriteRenderer>().flipX = true;
                GameObject.Find("Left Leg").GetComponent<SpriteRenderer>().flipX = true;
                GameObject.Find("spud_shotgun").GetComponent<SpriteRenderer>().enabled = false;

                if ((Input.GetKey(KeyCode.A)) && (sprint))
                {
                    GameObject.Find("player_flip_channel").transform.localScale = new Vector3(1, 1, 1);
                }

                if ((Input.GetKey(KeyCode.D)) && (sprint))
                {
                    GameObject.Find("player_flip_channel").transform.localScale = new Vector3(-1, 1, 1);
                }

                sprint = true;
                walking_speed = 100f;
            }
            else
            {
                GameObject.Find("Body").GetComponent<SpriteRenderer>().flipX = false;
                GameObject.Find("Hat").GetComponent<SpriteRenderer>().flipX = false;
                GameObject.Find("Right Arm").GetComponent<SpriteRenderer>().flipX = false;
                GameObject.Find("Left Arm").GetComponent<SpriteRenderer>().flipX = false;
                GameObject.Find("Right Leg").GetComponent<SpriteRenderer>().flipX = false;
                GameObject.Find("Left Leg").GetComponent<SpriteRenderer>().flipX = false;
                GameObject.Find("spud_shotgun").GetComponent<SpriteRenderer>().enabled = true;

                sprint = false;
                walking_speed = 60f;
            }
        }
        // ############### Player Sprint ###############

        // if (health > 100)
        //     health = 100;

        // ############### Player PLANT ###############
        if ((Input.GetKey(KeyCode.Space)) || (Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightControl)))
        {
            sprint = false;
            potatoe_mode = true;
            GameObject.Find("Body").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Hat").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Right Arm").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Left Arm").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Right Leg").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Left Leg").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("spud_shotgun").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("TurretFront").GetComponent<SpriteRenderer>().enabled = true;
            health += Time.deltaTime * 5;
            setCurrentWeapon(0);
            walking_speed = 10f;
            setBulletSpeed(150.0f);
            setRpm(0.1f);
            setWeaponSpread(1);


        }
        else
        {
            potatoe_mode = false;
            GameObject.Find("Body").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Hat").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Right Arm").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Left Arm").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Right Leg").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Left Leg").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("TurretFront").GetComponent<SpriteRenderer>().enabled = false;
            setCurrentWeapon(1);
            setBulletSpeed(150.0f);
            setRpm(0.8f);
            setWeaponSpread(5);
        }
        // ############### Player PLANT ###############

        // ############### Player Layering ###############
        GameObject.Find("Body").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z;
        GameObject.Find("Hat").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z - 1;
        GameObject.Find("Right Arm").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z + 1;
        GameObject.Find("Left Arm").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z - 1;
        GameObject.Find("Right Leg").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z + 1;
        GameObject.Find("Left Leg").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z - 1;
        GameObject.Find("spud_shotgun").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z + 1;
        GameObject.Find("TurretFront").GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z;

        rb.velocity = new Vector3(hspeed, 0, vspeed);
        // ############### Player Layering ###############

        // ############### Generate Enemy ###############
        timer += Time.deltaTime;
        rngX = Random.Range(-500, -300);
        rngZ = Random.Range(-500, -300);

        if (timer > 1.0f + spawnTimer)
        {
            for (int i = 0; i < spawnRate; i++)
            {
                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        rngX = Mathf.Abs(rngX);
                        rngZ = Mathf.Abs(rngZ);
                        break;
                    case 2:
                        rngX = Mathf.Abs(rngX);
                        break;
                    case 3:
                        rngZ = Mathf.Abs(rngZ);
                        break;
                    case 4:
                        break;
                }

                // newEnemy = Instantiate(enemy) as GameObject;
                // newEnemy.transform.position = new Vector3(transform.position.x + Random.Range(rngX, rngX / 2), 18, transform.position.z + Random.Range(rngZ, rngZ / 2));
            }

            timer = 0.0f;
            if (spawnTimer < 6.50f)
                setSpawnTimer(spawnTimer + 0.25f);

            if (spawnTimer > 3.0f && spawnTimer < 5.0f)
                setSpawnRate(3);
            else if (spawnTimer > 5.25f && spawnTimer < 6.0f)
                setSpawnRate(4);
            else if (spawnTimer > 6.25f)
                setSpawnRate(5);
        }
        // ############### Generate Enemy ###############

        // health
        GameObject.Find("HEALTH").GetComponent<UnityEngine.UI.Text>().text = ((int)health).ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
            // timer_script.setMinuteCountSaved(timer_script.getMinuteCount());
            // timer_script.setSecoundCountSaved(timer_script.getSecoundCount());
            // timer_script.setMinuteCount(0);
            // timer_script.setSecoundCount(0.0f);
        }

    }
    public int getCurrentWeapon() { return currentWeapon; }
    public void setCurrentWeapon(int aCurrentWeapon) { currentWeapon = aCurrentWeapon; }

    public float getBulletSpeed() { return bullet_speed; }
    public void setBulletSpeed(float aBulletSpeed) { bullet_speed = aBulletSpeed; }

    public float getRpm() { return rpm; }
    public void setRpm(float aRpm) { rpm = aRpm; }

    public float getWeaponSpread() { return weaponSpread; }
    public void setWeaponSpread(int aWeaponSpread) { weaponSpread = aWeaponSpread; }

    public int getSpawnRate() { return spawnRate; }
    public void setSpawnRate(int aSpawnRate) { spawnRate = aSpawnRate; }

    public float getSpawnTimer() { return spawnTimer; }
    public void setSpawnTimer(float aSpawnTimer) { spawnTimer = aSpawnTimer; }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "enemy(Clone)")
        {
            health -= 15;
            knockBack_hspeed = GameObject.Find("enemy(Clone)").GetComponent<enemy_script>().hspeed * 4;
            knockBack_vspeed = GameObject.Find("enemy(Clone)").GetComponent<enemy_script>().vspeed * 4;
        }
    }

}
