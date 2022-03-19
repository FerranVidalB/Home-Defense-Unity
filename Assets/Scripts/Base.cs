using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base : MonoBehaviour {

    public float maxHealth = 10;
    public PauseMenu pauseMenu;
    private NavMeshSurface surface;
    private GameObject map;
    private GameObject spawner;
    private float health;
    private int totalCoins;
    public int spawners = 4;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map");
        surface = GameObject.FindObjectOfType<NavMeshSurface>();
        //surface.BuildNavMesh();
        spawner = (GameObject)Resources.Load("Prefabs/Structures/Spawner", typeof(GameObject));
        health = maxHealth;
        totalCoins = 1000;
        pauseMenu.UpdateCoins(totalCoins);
        //GenerateSpawner();
        StartCoroutine(CallSpawners());
    }

    private void Update() {
        //Debug.Log(health);
    }
    private void GenerateSpawner() {
        while (spawners > 0) {
            Vector3 randomPoint = transform.position + GetRandomInDonut(80f, 210f);
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
                Vector3 result = hit.position;
                float radius = 20f;
                if (!Physics.CheckSphere(result, radius)) {
                    Transform newSpawner = Instantiate(spawner.transform, result, Quaternion.Euler(0, 0, 0));
                    spawners--;

                    Camera.main.gameObject.AddComponent<SpawnerPoint>().SetTarget(newSpawner);
                   // Camera.main.GetComponent<SpawnerPoint>().SetTarget(newSpawner);
                }
            }
        }
        map.GetComponent<BoxCollider>().enabled = true;
        surface.BuildNavMesh();
        GameObject.FindObjectOfType<PauseMenu>().MapLoaded();

    }

    IEnumerator CallSpawners() {
        yield return new WaitForSeconds(4f);
        
        surface.BuildNavMesh();
        GenerateSpawner();
    }

    public void Damage(int damage) {
        health -= damage;
        pauseMenu.UpdateHealth(health / maxHealth);
    }

    public int GetTotalCoins() {
        return totalCoins;
    }
    public void PayTurret(int price)
    {
        totalCoins -= price;
        pauseMenu.UpdateCoins(totalCoins);
    }

    public void SetTotalCoins(int coins) {
        totalCoins += coins;
        pauseMenu.UpdateCoins(totalCoins);
    }


    private Vector3 GetRandomInDonut(float min, float max) {
        float rot = Random.Range(1f, 360f);
        Vector3 direction = Quaternion.AngleAxis(rot, Vector3.up) * Vector3.forward;
        Ray ray = new Ray(Vector3.zero, direction);
        return ray.GetPoint(Random.Range(min, max));
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 80f);
        Gizmos.DrawWireSphere(transform.position, 210f);
    }
}
