using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    [SerializeField] EnemyManager _enemyManager;

    [SerializeField] float TimeLimit = 30f;

    // For UI
    [SerializeField] TMPro.TextMeshProUGUI timerText;

    float timer;
    List<GameObject> enemies = new List<GameObject>();

    // 1: Clear, 2: Die, 3:Timeover
    int isCleared = 0;

    private void Awake()
    {
        
    }

    private void Start()
    {
        StartCoroutine(GameLogic());
    }

    IEnumerator GameLogic()
    {
        // 게임 시작

        // 플레이어가 초기 위치

        // 타이머 시작
        timer = 0f;

        // 적이 생성되기 시작
        for (int i = 0; i < 3; i++)
        {
            var randomPosition = Random.insideUnitCircle * 20f;
            randomPosition = new Vector2(randomPosition.x, Mathf.Abs(randomPosition.y));
            var enemy = _enemyManager.SpawnEnemy(randomPosition);
            enemies.Add(enemy);
        }


        while (true)
        {
            // 적이 모두 죽으면 클리어
            if (enemies.Count == 0)
            {
                isCleared = 1;
                break;
            }

            // 플레이어가 죽어도 게임 오버
            if (_player.HP <= 0)
            {
                isCleared = 2;
                break;
            }

            timer += Time.deltaTime;
            timerText.text = $"Time Left {Mathf.Ceil(timer)}";

            // 시간이 다되면 게임 오버
            if (timer >= TimeLimit)
            {
                isCleared = 3;
                break;
            }

            yield return null;
        }

        // 게임 오버 UI 처리
        switch (isCleared)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
