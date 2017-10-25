using UnityEngine;
using UnityEngine.SceneManagement;

public class meteorSpawner : MonoBehaviour
{
    public float spawnDelay = .3f;
    public GameObject Meteor;
    public Transform[] spawnPoints;
    float nextTimeToSpawn = 0f;

    void Update ()
    {
            if (nextTimeToSpawn <= Time.time)
            {
                SpawnMeteor();
                nextTimeToSpawn = Time.time + spawnDelay;
            }     
    }
    public void SpawnMeteor ()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex]; 
        Instantiate(Meteor, spawnPoint.position, spawnPoint.rotation);
    }
}
