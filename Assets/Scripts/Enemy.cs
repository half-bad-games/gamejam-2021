using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] public GameObject gameArea;
    [SerializeField] public int size;
    [SerializeField] public float health;
    private Stats stats;
    private Vector2 movement;

    private float thrust = 100;
    private float torque = 100;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 thrust = new Vector2(Random.Range(-this.thrust, this.thrust), Random.Range(-this.thrust, this.thrust));
        float torque = Random.Range(-this.torque, this.torque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);
    }

    // Update is called once per frame
    void Update()
    {
        HandleEnemyMovement();
        HandleStayInsideScreen();
    }

    void HandleEnemyMovement()
    {
        // MeshCollider mesh = gameArea.GetComponent<MeshCollider>();
        // MeshFilter meshF = gameArea.GetComponent<MeshFilter>();
        // if (meshF.mesh.bounds.Contains(transform.position))
        // {
        //     Debug.Log("ASDASDASDASDA");
        // }
        // float screenX, screenY;
        // screenX = Random.Range(mesh.bounds.min.x, mesh.bounds.max.x);
        // screenY = Random.Range(mesh.bounds.min.y, mesh.bounds.max.y);
        // // Debug.Log("X Y " + screenX + " " + screenY);
        // var newPosition = new Vector2(screenX, screenY);
        // transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime);
    }

    void HandleStayInsideScreen()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        
        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect .xMin + 3, cameraRect .xMax - 3),
            Mathf.Clamp(transform.position.y, cameraRect .yMin + 3, cameraRect .yMax - 3),
            transform.position.z);
    }
}
