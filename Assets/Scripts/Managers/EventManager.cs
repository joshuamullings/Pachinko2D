using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Instance

        public static EventManager Instance => _instance;
        
        private static EventManager _instance;

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

    public event Action<Buff> OnBuffCollected;

    public void BuffCollected(Buff buff)
    {
        if (OnBuffCollected != null)
        {
            OnBuffCollected(buff);
        }
    }
}