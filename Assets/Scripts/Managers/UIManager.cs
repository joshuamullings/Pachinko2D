using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Instance

        public static UIManager Instance => _instance;
        
        private static UIManager _instance;

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
    
    [SerializeField] private Text _scoreText;
    [SerializeField] private List<GameObject> _buffTimers = new List<GameObject>();
    private List<BuffDetails> _buffDetails = new List<BuffDetails>();

    public int Score { get; set; } = 0;

    public void UpdateRemainingDurations(float[] durations)
    {
        for (int i = 0; i < _buffDetails.Count; i++)
        {
            _buffDetails[i] = new BuffDetails(_buffDetails[i].Buff, durations[i]);
        }
    }

    private void Start()
    {
        // populate buff details list
        List<Buff> buffList = BuffManager.Instance.AvailableBuffsList;
        float[] endTimes = BuffManager.Instance.BuffEndTimes;

        for (int i = 0; i < buffList.Count; i++)
        {
            _buffDetails.Add(
                new BuffDetails(buffList[i], endTimes[i])
            );
        }
    }

    private void Update()
    {
        RenderScore();
        RenderBuffs();
    }

    private void RenderScore()
    {
        _scoreText.text = Score.ToString().PadLeft(8, '0');
    }

    private void RenderBuffs()
    {
        for (int i = 0; i < _buffDetails.Count; i++)
        {
            if (_buffDetails[i].Buff.Timed)
            {
                if (_buffDetails[i].RemainingDuration > 0.0f)
                {
                    UpdateBuffAlpha(i, 1.0f, _buffDetails[i].RemainingDuration.ToString("0"));
                }
                else
                {
                    UpdateBuffAlpha(i, 0.2f, "");
                }
            }
        }
    }

    private void UpdateBuffAlpha(int i, float alpha, string text)
    {
        _buffTimers[i].GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
        _buffTimers[i].GetComponentInChildren<Text>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
        _buffTimers[i].GetComponentInChildren<Text>().text = text;
    }

    private struct BuffDetails
    {
        public Buff Buff { get; set; }
        public float RemainingDuration { get; set; }

        public BuffDetails(Buff buff, float endTime)
        {
            Buff = buff;
            RemainingDuration = endTime - Time.time;

            if (RemainingDuration < 0.0f)
            {
                RemainingDuration = 0.0f;
            }
        }
    }
}