using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] items;
    public int cost = -1;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if(Vector3.Distance(playerPos,transform.position) < 2)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(GameManager.SpendMoney(cost))
                {
                    int rand = Random.Range(0,items.Length);
                    Instantiate(items[rand], transform.position,Quaternion.identity);
                    Debug.Log(GameManager.money);
                    Destroy(gameObject);
                }
            }
        }
    }
}
