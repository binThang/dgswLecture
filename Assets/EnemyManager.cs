using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager Instance;
    public static EnemyManager GetInsance()
    {
        return Instance;
    }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float SpawnTime;

    Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        // DontDestroyOnLoad(gameObject);
        Instance = this;

        foreach (Transform t in transform)
        {
            queue.Enqueue(t.gameObject);
        }
    }

    private GameObject getEnemy()
    {
        GameObject enemy;
        bool success = queue.TryDequeue(out enemy);

        if (success)
        {
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            enemy = Instantiate(enemyPrefab, transform);
            return enemy;
        }       
    }

    public void returnEnemy(GameObject enemyObject)
    {
        //Destroy(gameObject);
        enemyObject.SetActive(false);
        queue.Enqueue(enemyObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTime);

            GameObject enemy = getEnemy();
            if (enemy == null)
                continue;

            enemy.transform.position = new Vector3(10, -2, 0);
        }
    }
}
