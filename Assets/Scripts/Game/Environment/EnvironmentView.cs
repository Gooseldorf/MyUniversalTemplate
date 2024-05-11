using System;
using UnityEngine;

namespace Game.Environment
{
    public class EnvironmentView : MonoBehaviour
    {
        public Canvas GameFieldCanvas;

        private void Start()
        {
            GameFieldCanvas.worldCamera = Camera.main;
        }
    }
}