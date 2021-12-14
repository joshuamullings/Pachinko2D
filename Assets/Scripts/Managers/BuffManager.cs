using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    #region Instance

        public static BuffManager Instance => _instance;
        
        private static BuffManager _instance;

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

    public List<Buff> AvailableBuffsList = new List<Buff>();
    [HideInInspector] public List<Buff> ActiveBuffsList = new List<Buff>();
    [HideInInspector] public float[] BuffEndTimes; // 0.0f indicates buff is inactive or expired

    [SerializeField] private Rect _spawnBounds;
    [SerializeField] private float _spawnRadiusCheck = 0.5f;
    [SerializeField] private float _spawnScaleCheck = 0.4f;
    [SerializeField] private float _timeBetweenSpawns = 20.0f;
    private float _timeSinceLastSpawn;
    [SerializeField] private float _timebetweenActiveBuffsCheck = 1.0f;
    private float _timeSinceActiveBuffsCheck;

    private void Start()
    {
        EventManager.Instance.OnBuffCollected += OnBuffCollected;
        BuffEndTimes = new float[AvailableBuffsList.Count];
    }

    private void Update()
    {
        if (Time.time - _timeSinceLastSpawn > _timeBetweenSpawns)
        {
            SpawnBuff();
        }

        if (Time.time - _timeSinceActiveBuffsCheck > _timebetweenActiveBuffsCheck)
        {
            UpdateActiveBuffs();
        }
    }

    private void SpawnBuff()
    {
        // select random buff
        int buffIndex = UnityEngine.Random.Range(0, AvailableBuffsList.Count);

        // select corresponding prefab
        Buff prefab = AvailableBuffsList[buffIndex];

        // find valid location
        Vector2 validLocation = FindValidLocation();

        // spawn prefab
        Buff _spawnedBuff = Instantiate(
            prefab,
            validLocation,
            Quaternion.identity
        );

        _spawnedBuff.ID = buffIndex;
        _spawnedBuff.transform.SetParent(GameObject.FindWithTag("Buffs").transform);
        _timeSinceLastSpawn = Time.time;
    }

    private Vector2 FindValidLocation()
    {
        // generate location
        Vector2 spawnLocation = GenerateSpawnLocation();

        // hold collider overlaps
        Collider2D overlaps;

        // check overlaps
        overlaps = CheckOverlaps(spawnLocation);

        // find location with no overlaps
        while (overlaps != null)
        {
            spawnLocation = GenerateSpawnLocation();
            overlaps = CheckOverlaps(spawnLocation);
        }

        // return location
        return spawnLocation;
    }

    private Vector2 GenerateSpawnLocation()
    {
        Vector2 spawnLocation = new Vector2(
            Random.Range(_spawnBounds.x, _spawnBounds.x + _spawnBounds.width),
            Random.Range(_spawnBounds.y, _spawnBounds.y + _spawnBounds.height)
        );

        return spawnLocation;
    }

    private Collider2D CheckOverlaps(Vector2 location)
    {
        Collider2D overlap = Physics2D.OverlapCircle(
            location,
            _spawnScaleCheck * _spawnRadiusCheck
        );

        return overlap;
    }

    private void OnBuffCollected(Buff buff)
    {
        // only timed buffs are dealt with
        if (buff.Timed)
        {
            // if it's not already active
            if (BuffEndTimes[buff.ID] == 0.0f)
            {
                BuffEndTimes[buff.ID] = Time.time + buff.Duration;
                ActiveBuffsList.Add(buff);
            }
            // else it's alreadu active
            else
            {
                BuffEndTimes[buff.ID] += buff.Duration;
                Destroy(buff.gameObject);
            }
        }
    }

    private void UpdateActiveBuffs()
    {
        if (ActiveBuffsList.Count > 0)
        {
            // check and cache buffs to be removed
            List<Buff> toBeRemoved = new List<Buff>();

            foreach (Buff buff in ActiveBuffsList)
            {
                if (BuffEndTimes[buff.ID] <= Time.time)
                {
                    toBeRemoved.Add(buff);
                }
            }

            // remove buffs using the cached list
            foreach (Buff buff in toBeRemoved)
            {
                buff.RemoveEffect();
                ActiveBuffsList.Remove(buff);
                BuffEndTimes[buff.ID] = 0.0f;
            }

            ActiveBuffsList.TrimExcess();
        }

        UIManager.Instance.UpdateRemainingDurations(BuffEndTimes);
        _timeSinceActiveBuffsCheck = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(_spawnBounds.x, _spawnBounds.y),
            new Vector2(_spawnBounds.x + _spawnBounds.width, _spawnBounds.y + _spawnBounds.height)
        );
    }
}