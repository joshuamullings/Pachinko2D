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

    public List<Buff> AvailableBuffsList;
    public List<ActiveBuffs> ActiveBuffsList = new List<ActiveBuffs>();

    [SerializeField] private Rect _spawnBounds;
    [SerializeField] private float _spawnRadiusCheck = 0.5f;
    [SerializeField] private float _spawnScaleCheck = 0.4f;
    [SerializeField] private float _timeBetweenSpawns = 20.0f;
    private float _timeSinceLastSpawn;

    private void Start()
    {
        // build active buffs list
        for (int i = 0; i < AvailableBuffsList.Count; i++)
        {
            ActiveBuffs newBuff = new ActiveBuffs(
                AvailableBuffsList[i], i
            );

            ActiveBuffsList.Add(newBuff);
        }
    }

    private void Update()
    {
        if (Time.time - _timeSinceLastSpawn > _timeBetweenSpawns)
        {
            SpawnBuff();
        }

        // loop through all buffs
        for (int i = 0; i < ActiveBuffsList.Count; i++)
        {
            // check which are active
            if (ActiveBuffsList[i].IsActive)
            {
                // check if the active time is more than the duration
                if ((Time.time - ActiveBuffsList[i].ActivationTime) > ActiveBuffsList[i].Duration)
                {
                    // deactivate if exceeding duration
                    ActiveBuffsList[i].DeactivateBuff();
                }
            }
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

        // setup the buff index
        _spawnedBuff.BuffID = buffIndex;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(_spawnBounds.x, _spawnBounds.y),
            new Vector2(_spawnBounds.x + _spawnBounds.width, _spawnBounds.y + _spawnBounds.height)
        );
    }

    public class ActiveBuffs
    {
        public Buff _buff;
        public int BuffID { get; set; }
        public bool IsActive { get; set; } = false;
        public float ActivationTime { get; set; } = 0.0f;
        public float Duration { get; set; } = 0.0f;

        public ActiveBuffs(Buff buff, int buffID)
        {
            _buff = buff;
            BuffID = buffID;
        }

        public void ActivateBuff(float duration)
        {
            if (!IsActive)
            {
                ActivationTime = Time.time;
            }

            IsActive = true;
            Duration += duration;
            UIManager.Instance.BuffListener(BuffID, Duration);
        }

        public void DeactivateBuff()
        {
            _buff.RemoveEffect();
            ActivationTime = 0.0f;
            Duration = 0.0f;
            IsActive = false;
            UIManager.Instance.BuffListenerDisable(BuffID, Duration);
        }
    }
}