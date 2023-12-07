using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The CloudsOnLoad class is responsible for controlling the cluster of clouds that appear when the user logs in, after the map is loaded.
/// </summary>
public class CloudsOnLoad : MonoBehaviour
{
    public GameObject cloud;
    public int numClouds;
    public float moveSpeed;

    /// <summary>
    /// Spawns the cluster of clouds and destroys them after 10 seconds.
    /// </summary>
    void Start()
    {
        for (int i = 0; i < numClouds; i++)
        {
            float z = Random.Range(-15f, 10f);
            float y = Random.Range(3.5f, 4.5f);
            float startX = Random.Range(-10f, 10f);
            Vector3 spawnLocation = new Vector3(startX, y, z);
            GameObject c = Instantiate(cloud, spawnLocation, Quaternion.identity);
            Vector3 scale = new Vector3(Random.Range(1f, 1.5f), Random.Range(0.2f, 0.5f), Random.Range(1f, 1.5f));
            c.transform.localScale = scale;
            float x = Random.Range(1f, 2f);
            c.GetComponent<Rigidbody>().velocity = new Vector3(-x * moveSpeed, 0, 0);
            Destroy(c, 10f);
        }
        Destroy(gameObject, 10f);
    }
}
