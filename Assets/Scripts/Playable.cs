using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    private int currentXPToLevel;
    private int currentXP;
    public int currentSP;
    private int growth;

    // Start is called before the first frame update
    void Start()
    {
        this.stats = new Stats();
        this.growth = 0;
        this.size = 1;
        this.currentXPToLevel = 100;
        this.currentXP = 0;
        this.currentSP = 0;
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

    public int GetXp()
    {
        return this.currentXP;
    }

    public int GetSp()
    {
        return this.currentSP;
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
        size++;
        transform.localScale = new Vector3(size, size, size);
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
