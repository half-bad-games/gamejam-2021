using System;
using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int size;
    [SerializeField] public float health;
    [SerializeField] public int xpGains;
    [SerializeField] public int currentXPToLevel = 100;
    [SerializeField] public int currentXP = 0;
    [SerializeField] public int currentSP = 0;
    private int growth = 0;
    public Stats stats;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        health = stats.health;
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
    
    private void IncreaseXPToleven()
    {
        Debug.Log("WTF");
        currentXPToLevel *= (int) 1.2;
    }

    public void IncreaseCurrentXP(int xpGains)
    {
        currentXP += xpGains;
        while (currentXP >= currentXPToLevel)
        {
            IncreaseXPToleven();
            IncreaseSize();
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

    private void IncreaseSize()
    {
        int amount = size - growth;
        Vector3 local = transform.localScale;
        transform.localScale = new Vector3(local.x + 0.2f * amount,local.y + 0.2f * amount,local.z + 0.2f * amount);
        size++;
        growth++;
    }
}
