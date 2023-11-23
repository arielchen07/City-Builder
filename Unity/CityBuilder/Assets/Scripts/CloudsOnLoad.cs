using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsOnLoad : MonoBehaviour
{
    public GameObject cloud;
    public int numClouds;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numClouds; i++){
            float z = Random.Range(-15f,10f);
            float y = Random.Range(3.5f,4.5f);
            float startX = Random.Range(-10f,10f);
            Vector3 spawnLocation = new Vector3(startX, y, z);
            GameObject c = Instantiate(cloud, spawnLocation, Quaternion.identity);
            Vector3 scale = new Vector3(Random.Range(1f,1.5f),Random.Range(0.2f,0.5f),Random.Range(1f,1.5f));
            c.transform.localScale = scale;
            float x = Random.Range(1f,2f);
            c.GetComponent<Rigidbody>().velocity = new Vector3(-x * moveSpeed, 0, 0);
            Destroy(c, 10f);
        }
        Destroy(this.gameObject, 10f);
    }
}
