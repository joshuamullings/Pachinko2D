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
    [SerializeField] private List<GameObject> AvailableBuffIconsList = new List<GameObject>();
    [SerializeField] private Vector2 _buffAnchor = new Vector2(0.0f, 0.0f);
    [SerializeField] private float _buffAnchorSpacing = 1.0f;
    private List<GameObject> _iconCache = new List<GameObject>();
    private List<ActiveBuffIconsList> _activeBuffIconsList = new List<ActiveBuffIconsList>();
    
    public int Score { get; set; } = 0;

    public void BuffListener(int ID, float duration)
    {
        int i = 0;
        bool alreadyExists = false;

        // loop through current active buffs
        for (; i < _activeBuffIconsList.Count; i++)
        {
            // check if buff already exists in list
            if (_activeBuffIconsList[i].BuffID == ID)
            {
                alreadyExists = true;
                break;
            }
        }

        if (alreadyExists)
        {
            // update duration if so
            _activeBuffIconsList[i].Duration = duration;
        }
        else
        {
            // else add to list
            _activeBuffIconsList.Add(
                new ActiveBuffIconsList(
                    i, BuffManager.Instance.ActiveBuffsList[i].Duration - (Time.time - BuffManager.Instance.ActiveBuffsList[i].ActivationTime)
                )
            );
        }
    }

    public void BuffListenerDisable(int ID, float duration)
    {
        // loop through current active buffs
        for (int i = 0; i < _activeBuffIconsList.Count; i++)
        {
            // find the buff in the list
            if (_activeBuffIconsList[i].BuffID == ID)
            {
                _iconCache[ID].SetActive(false);
                _activeBuffIconsList.RemoveAt(i);
                _activeBuffIconsList.TrimExcess();
            }
        }
    }

    private void Start()
    {
        // sort the active buffs list by duration
        _activeBuffIconsList.Sort((x, y) => x.Duration.CompareTo(y.Duration));

        if (AvailableBuffIconsList.Count == BuffManager.Instance.AvailableBuffsList.Count)
        {
            for (int i = 0; i < AvailableBuffIconsList.Count; i++)
            {
                GameObject icon = Instantiate(AvailableBuffIconsList[i], Vector3.zero, Quaternion.identity);
                icon.SetActive(false);
                _iconCache.Add(icon);
            }
        }
        else
        {
            Debug.Log("Mismatch between available buffs and buff icons!");
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
        // sort the active buffs list by duration
        _activeBuffIconsList.Sort((x, y) => x.Duration.CompareTo(y.Duration));

        for (int i = 0; i < _activeBuffIconsList.Count; i++)
        {
            _iconCache[i].gameObject.GetComponentInChildren<Text>().text = (BuffManager.Instance.ActiveBuffsList[i].Duration - (Time.time - BuffManager.Instance.ActiveBuffsList[i].ActivationTime)).ToString("0");
            _iconCache[i].SetActive(true);
            _iconCache[i].transform.position = _buffAnchor - (_buffAnchorSpacing * new Vector2(0.0f, 1.0f));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_buffAnchor, 0.1f);
    }

    public class ActiveBuffIconsList
    {
        public float Duration { get; set; }
        public int BuffID { get; set; }

        public ActiveBuffIconsList(int buffID, float duration)
        {
            BuffID = buffID;
            Duration = duration;
        }
    }
}