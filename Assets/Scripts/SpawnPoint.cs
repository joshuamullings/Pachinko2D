using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    #region Instance

        public static SpawnPoint Instance => _instance;
        
        private static SpawnPoint _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

    #endregion

    private BallManager _ballManager;
    private Transform _ballsTransform;
    private GameObject _ball;
    private float _timeSinceLastSpawn;

    public void UpdatePosition(Vector2 position)
    {
        transform.position = position;
    }

    private void Start()
    {
        _ballManager = BallManager.Instance;
        _ballsTransform = GameObject.FindWithTag("Balls").transform;
        _ball = (GameObject)Resources.Load("Ball");
    }

    private void Update()
    {
        if (Time.time - _timeSinceLastSpawn > _ballManager.AutoSpawnDelay)
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        if (_ballManager.CurrentBalls < _ballManager.MaximumBalls)
        {
            GameObject gameObject = Instantiate(_ball, transform.position, Quaternion.identity);
            gameObject.transform.SetParent(_ballsTransform);
            _ballManager.CurrentBalls++;
            _timeSinceLastSpawn = Time.time;
        }
    }
}