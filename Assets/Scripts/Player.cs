using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] public float health;
    private Ray ray;
    private RaycastHit hit;
    Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        var p = new PlayerAdapterComponent(this);
        var comp = new DecoratorFactory(p).generate(10);
        stats = comp.extend();
        health = stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
        HandlePlayerMovement();
    }

    void HandlePlayerMovement()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w"))
        {
            pos.y += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += movementSpeed * Time.deltaTime;
        }

        transform.position = pos;
        
        var modelPosition = transform.position;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var delta_x = modelPosition.x - mousePosition.x;
        var delta_y = modelPosition.y - mousePosition.y;

        var delta_x_norm = delta_x / (Mathf.Sqrt(delta_x * delta_x + delta_y * delta_y));
        var delta_y_norm = delta_y / (Mathf.Sqrt(delta_x * delta_x + delta_y * delta_y));
        var player_angle = (Mathf.Atan2(delta_y_norm, delta_x_norm) * (Mathf.Rad2Deg));
        
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, player_angle + 90));
    }
}
