using UnityEngine;

namespace Game.Weapon
{
    public class WeaponViewBase : MonoBehaviour
    {
        [SerializeField] private ShotPoint[] shootPoints;
        private int lastShootPointIndex;

        private void Awake()
        {
            lastShootPointIndex = 0;
        }
        
        public ShotPoint[] GetNextShootPoints(int count)
        {
            if (count > shootPoints.Length)
                count = shootPoints.Length;
            
            ShotPoint[] points = new ShotPoint[count];
            
            for (int i = 0; i < count; i++)
            {
                lastShootPointIndex++;
                if (lastShootPointIndex >= shootPoints.Length)
                    lastShootPointIndex = 0;
                points[i] = shootPoints[lastShootPointIndex];
            }

            return points;
        }
    }
}