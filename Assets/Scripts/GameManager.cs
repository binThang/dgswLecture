using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;

    [SerializeField] PlayerController _player;
    [SerializeField] EnemyManager _enemyManager;

    [SerializeField] float TimeLimit = 30f;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameClearPanel;

    // For UI
    [SerializeField] TMPro.TextMeshProUGUI timerText;

    float timer;

    // 1: Clear, 2: Die, 3:Timeover
    int isCleared = 0;

    private void Awake()
    {
        
    }

    private void Start()
    {
        StartCoroutine(GameLogic());
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        SoundManager.Instance.PlayBGM(false, 0);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlayBGM(true, 0);
    }

    IEnumerator GameLogic()
    {
        // 게임 시작
        SoundManager.Instance.PlayBGM(true, 0);

        // 플레이어가 초기 위치

        // 타이머 시작
        timer = 0f;

        // 적이 생성되기 시작
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            //var randomPosition = Random.insideUnitCircle * 20f;
            //randomPosition = new Vector2(randomPosition.x, Mathf.Abs(randomPosition.y));
            var enemy = _enemyManager.SpawnEnemy(spawnPoints[i].transform.position);
        }

        while (true)
        {
            // 적이 모두 죽으면 클리어
            if (EnemyManager.GetInsance().liveEnemies.Count == 0)
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

        Time.timeScale = 0f;

        // 게임 오버 UI 처리
        switch (isCleared)
        {
            case 1:
                gameClearPanel.SetActive(true);
                break;
            case 2:
            case 3:
                gameOverPanel.SetActive(true);
                break;
        }
    }
}
