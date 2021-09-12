using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] public float movementSpeed;
    [SerializeField] public float health;
    [SerializeField] public int size;
    [SerializeField] public int currentXPToLevel;
    [SerializeField] public int currentXP;
    [SerializeField] public int currentSP;
    private Stats stats;
    private float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.name = this.GetInstanceID().ToString();
        var p = new GameObjectAdapterComponent(this.name, this.GetType());
        var comp = new DecoratorFactory(p, this).generate(10);
        stats = comp.extend();
        health = stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        // GameObject.Find("HEALTH").GetComponent<UnityEngine.UI.Text>().text = ((int)health).ToString();
        HandlePlayerMovement();
        HandleStayInsideScreen();
        if (Input.GetKey("space"))
        {
            IncreaseSize(1);
        }

        if (camera.orthographicSize < size)
        {
            IncreaseCameraSize(camera.orthographicSize);
        }
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
    
    private void IncreaseCameraSize(float camSize)
    {
        t += Time.deltaTime;
        camera.orthographicSize = camSize * 3;
    }

    private void IncreaseXPToleven()
    {
        currentXPToLevel *= (int) 1.2f;
    }

    public void IncreaseCurrentXP(int xpGains)
    {
        currentXP += xpGains;
        while (currentXP >= currentXPToLevel)
        {
            IncreaseXPToleven();
            IncreaseSize(1);
            currentXP = currentXP - currentXPToLevel;
            IncreaseSP();
        }
    }

    private void IncreaseSP()
    {
        currentSP += 1;
    }

    public void DecreaseSP(int amount)
    {
        currentSP -= amount;
        if (currentSP > 0)
        {
            currentSP = 0;
        }
    }

    private void IncreaseSize(int amount)
    {
        Vector3 local = transform.localScale;
        transform.localScale = new Vector3(local.x + 0.2f * amount,local.y + 0.2f * amount,local.z + 0.2f * amount);
        size += amount;
    }
    
}
