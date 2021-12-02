using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    #region Instance

        public static BallManager Instance => _instance;
        
        private static BallManager _instance;

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

    private Transform _ballsTransform;
    private Transform _boardTransform;
    private GameObject _ball;
    private GameObject _spawnPoint;

    public int CurrentBalls { get; set; } = 0;
    public int MaximumBalls { get; set; } = 3;

    private void Start()
    {
        _ballsTransform = GameObject.FindWithTag("Balls").transform;
        _boardTransform = GameObject.FindWithTag("Board").transform;
        _ball = (GameObject)Resources.Load("Ball");
        _spawnPoint = (GameObject)Resources.Load("Spawn Point");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBall(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CreateSpawnPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void SpawnBall(Vector2 mousePosition)
    {
        if (CurrentBalls < MaximumBalls && ValidPosition(mousePosition))
        {
            GameObject gameObject = Instantiate(_ball, mousePosition, Quaternion.identity);
            gameObject.transform.SetParent(_ballsTransform);
            CurrentBalls++;
        }
    }

    private void CreateSpawnPoint(Vector2 mousePosition)
    {
        if (ValidPosition(mousePosition))
        {
            if (SpawnPoint.Instance == null)
            {
                GameObject gameObject = Instantiate(_spawnPoint, mousePosition, Quaternion.identity);
                gameObject.transform.SetParent(_boardTransform);
            }
            else
            {
                SpawnPoint.Instance.UpdatePosition(mousePosition);
            }
        }
    }

    private bool ValidPosition(Vector2 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast
        (
            mousePosition,
            Vector2.zero
        );

        if (hit.collider != null && hit.collider.CompareTag("Spawn Zone"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}