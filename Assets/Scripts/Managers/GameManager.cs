using System.Collections.Generic;
using Data;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class GameManager: MonoBehaviour
    {
        private List<IInit> inits = new ();
        private List<IUpdatable> updates = new ();
        private List<IDispose> disposes = new ();

        private GameData gameData;
        public GameData GameData => gameData;

        public static GameManager Instance;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;    
        }

        private void Start()
        {
            foreach (IInit init in inits)
                init.Init();
        }

        private void Update()
        {
            foreach (IUpdatable update in updates)
                update.Update();
        }

        private void OnDestroy()
        {
            foreach (IDispose dispose in disposes)
                dispose.Dispose();
        }
    }
}