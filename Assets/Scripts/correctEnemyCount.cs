using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class correctEnemyCount : MonoBehaviour
{
    private int enemies;
    private int dummies;
    private int total;
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("count", 0.7f);
    }

    void count()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        dummies = GameObject.FindGameObjectsWithTag("dummy").Length;
        total = enemies + dummies;
        Debug.Log("There are " +  enemies + " Enemies and " + dummies + " Dummies. In total there are " + total + " Enemies and Dummies");
        if (total > manager.totalEnemyCount)
        {
            manager.totalEnemyCount = total;
            
        }
    }
}
