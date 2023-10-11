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

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float SpawnTime;

    Queue<GameObject> queue = new Queue<GameObject>();
    public List<GameObject> liveEnemies = new List<GameObject>();

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
        }
        else
        {
            enemy = Instantiate(enemyPrefab, transform);
        }

        liveEnemies.Add(enemy);
        return enemy;
    }

    // 적이 죽었을 때 or 비활성화될 때
    public void returnEnemy(GameObject enemyObject)
    {
        //Destroy(gameObject);
        enemyObject.SetActive(false);
        liveEnemies.Remove(enemyObject);
        queue.Enqueue(enemyObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawn());
    }

    public GameObject SpawnEnemy(Vector3 position)
    {
        GameObject enemy = getEnemy();
        enemy.transform.position = position;
        enemy.GetComponent<Enemy>().Target = player;

        return enemy;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTime);

            GameObject enemy = getEnemy();
            enemy.GetComponent<Enemy>().Target = player;

            if (enemy == null)
                continue;

            enemy.transform.position = new Vector3(10, -2, 0);
        }
    }
}
