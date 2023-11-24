using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject dirLight;
    public GameObject cloud;
    public float moveSpeed;
    public float spawnInterval;
    float timer = 0;
    public float dayCycle = 24;
    public Vector2 zRange;
    public float xOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer){
            timer = Time.time + spawnInterval;
            float z = Random.Range(zRange.x,zRange.y);
            float y = Random.Range(3.5f,4.5f);
            Vector3 spawnLocation = new Vector3(xOffset + Camera.main.transform.position.x, y, z + Camera.main.transform.position.z);
            GameObject c = Instantiate(cloud, spawnLocation, Quaternion.identity);
            Vector3 scale = new Vector3(Random.Range(1f,1.5f),Random.Range(0.2f,0.5f),Random.Range(1f,1.5f));
            c.transform.localScale = scale;
            float x = Random.Range(1f,2f);
            c.GetComponent<Rigidbody>().velocity = new Vector3(-x * moveSpeed, 0, 0);
            Destroy(c, 60f / x);
        }
        dirLight.GetComponent<Light>().intensity = 0.2f + Mathf.PingPong(Time.time + 24, dayCycle) / dayCycle;
    }

    public void OnLogin(){
        spawnInterval = 0.01f;
        moveSpeed = 5f;
    }
}
