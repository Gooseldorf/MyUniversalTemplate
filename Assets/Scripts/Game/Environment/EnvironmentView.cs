using UnityEngine;

namespace Game.Environment
{
    public class EnvironmentView : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private RectTransform gameFieldRect;
        [SerializeField] private Canvas gameFieldCanvas;

        public void Init()
        {
            gameFieldCanvas.worldCamera = Camera.main;
        }

        public Bounds GetGameFieldBounds()
        {
            Vector3[] corners = new Vector3[4];
            gameFieldRect.GetWorldCorners(corners);

            float top = corners[1].y;
            float bottom = corners[3].y;
            float left = corners[0].x;
            float right = corners[2].x;
            
            return new Bounds(Vector3.zero, new Vector3(right-left,top-bottom,0));
        }
    }
}