using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZoneManager : MonoBehaviour
{
    #region Instance

        public static ScoreZoneManager Instance => _instance;
        
        private static ScoreZoneManager _instance;

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

    public int ScoreScale { get; set; } = 1;
}