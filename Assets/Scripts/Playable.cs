using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Stats
{
    public int speed;
    public int damage;
    public float health;
    public int agility;
    public int vision;
}

public class Playable : MonoBehaviour
{
    [SerializeField] public Stats stats;
    [SerializeField] public int size;
    [SerializeField] public float health;
    [SerializeField] public int currentXPToLevel;
    [SerializeField] public int currentXP;
    [SerializeField] public int currentSP;
    private int growth = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.stats = new Stats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseXPTolevel()
    {
        this.currentXPToLevel = 100 + (int)Mathf.Pow(this.currentXPToLevel, 1.1f);
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
        transform.localScale = new Vector3(local.x + 0.2f * amount, local.y + 0.2f * amount, local.z + 0.2f * amount);
        size++;
        growth++;
    }

    public void IncreaseCurrentXP(int xpGains)
    {
        this.currentXP += xpGains;
        while (this.currentXP >= this.currentXPToLevel)
        {
            IncreaseXPTolevel();
            IncreaseSize();
            IncreaseSP();
        }
    }
}
