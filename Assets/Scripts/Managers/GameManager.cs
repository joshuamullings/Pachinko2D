using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance

        public static GameManager Instance => _instance;
        
        private static GameManager _instance;

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

    public Vector2 GravityScale { get; set; }

    private void Start()
    {
        GravityScale = 2.0f * Physics2D.gravity;
    }
}