using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    Queue<GameObject> queue = new Queue<GameObject>();
    [SerializeField] GameObject bulletPrefab;

    private void Awake()
    {
        foreach (Transform t in transform)
        {
            queue.Enqueue(t.gameObject);
        }
    }

    public GameObject getBullet()
    {
        GameObject bullet;
        bool success = queue.TryDequeue(out bullet);

        if (success)
        {
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().pool = this;
            return bullet;
        }
        else
        {
            bullet = Instantiate(bulletPrefab, transform);
            return bullet;
        }
    }

    public void returnBullet(GameObject bulletObject)
    {
        //Destroy(gameObject);
        bulletObject.SetActive(false);
        queue.Enqueue(bulletObject);
    }
}
