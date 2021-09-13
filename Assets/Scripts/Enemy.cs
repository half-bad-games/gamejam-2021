using System;
using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Enemy : Playable
{
    [SerializeField] public int xpGains;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        this.currentXPToLevel = 100;
        this.currentXP = 0;
        this.currentSP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // wwHandleStayInsideScreen();
    }
    

    void HandleStayInsideScreen()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        
        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y
        );
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect .xMin + 3, cameraRect.xMax - 3),
            Mathf.Clamp(transform.position.y, cameraRect .yMin + 3, cameraRect.yMax - 3),
            transform.position.z
        );
    }

    public Stats getStats()
    {
        return this.stats;
    }
}
