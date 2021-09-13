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

    protected Playable()
    {
        this.currentXPToLevel = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseXPTolevel()
    {
        this.currentXPToLevel = 300 + (int)Mathf.Pow(this.currentXPToLevel, 1.05f);
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
        Debug.Log($"xpGains: {xpGains}, currentXP: {this.currentXP}, currentXPToLevel: {this.currentXPToLevel}");

        this.currentXP += xpGains;

        if (this.currentXP >= this.currentXPToLevel)
        {
            IncreaseXPTolevel();
            IncreaseSize();
            IncreaseSP();
        }
    }
}
