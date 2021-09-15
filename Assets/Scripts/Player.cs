using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class Player : Playable
{
    [SerializeField] private Camera camera;
    [SerializeField] public GameObject buyMenu;
    [SerializeField] public GameObject pause;
    [SerializeField] public AudioSource ambientAudio;
    [SerializeField] public Text SPText;

    private PlayableAdapterComponent adapterComponent;

    // Start is called before the first frame update
    void Start()
    {
        var name = this.GetInstanceID().ToString();
        camera.GetComponent<CameraFollow>().playerId = name;
        this.name = name;

        this.name = this.GetInstanceID().ToString();
        this.adapterComponent = new PlayableAdapterComponent(this.name);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandlePlayerMovement();
        // HandleStayInsideScreen();

        if (camera.orthographicSize <= this.stats.size * 10)
        {
            IncreaseCameraSize(camera.orthographicSize);
        }

        SPText.text = this.GetSp().ToString();

        HandleBuyMenu();
    }

    void HandleMouseInput()
    {
        if (this.pause.activeSelf == true)
        {
            return;
        }

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
    void Pause()
    {
        if (this.pause.activeSelf == false)
        {
            this.pause.SetActive(true);
            Time.timeScale = 0;
            this.ambientAudio.mute = true;
        }
        else
        {
            this.pause.SetActive(false);
            Time.timeScale = 1;
            this.ambientAudio.mute = false;
        }
    }

    void HandlePlayerMovement()
    {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown("escape"))
        {
            this.Pause();
        }

        if (Input.GetKey("w"))
        {
            pos.y += this.stats.speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= this.stats.speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= this.stats.speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += this.stats.speed * Time.deltaTime;
        }

        transform.position = pos;
    }

    public void Item0()
    {
        this.AttachDecorator("MouthDecorator");
    }
    public void Item1()
    {
        this.AttachDecorator("BasicFinsDecorator");
    }
    public void Item2()
    {
        this.AttachDecorator("EyesDecorator");
    }
    public void Item3()
    {
        this.AttachDecorator("WhiskersDecorator");
    }
    public void Item4()
    {
        this.AttachDecorator("SlimeDecorator");
    }
    public void Item5()
    {
        this.AttachDecorator("SpearDecorator");
    }
    public void Item6()
    {
        this.AttachDecorator("SpikesDecorator");
    }
    public void Item7()
    {
        this.AttachDecorator("ThirdEyeDecorator");
    }
    public void Item8()
    {
        this.AttachDecorator("FishTailDecorator");
    }

    void AttachDecorator(string name)
    {
        if (this.currentSP >= 1)
        {
            new PlayerDecoratorFactory(this.adapterComponent, this)
                .generate(name)
                .extend();

            this.currentSP -= 1;
        }
    }

    void HandleBuyMenu()
    {
        if (Input.GetKeyDown("p"))
        {
            if (this.buyMenu.activeSelf == false)
            {
                this.buyMenu.SetActive(true);
            }
            else
            {
                this.buyMenu.SetActive(false);
            }
        }
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
        camera.orthographicSize = camSize * 5;
    }

    public Stats getStats()
    {
        return this.stats;
    }
}
