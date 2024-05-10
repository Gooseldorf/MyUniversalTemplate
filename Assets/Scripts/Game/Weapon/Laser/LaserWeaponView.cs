using Unity.Mathematics;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserWeaponView : MonoBehaviour
    {
        [SerializeField] private Transform[] shootPoints;
        private int lastShootPointIndex;

        private void Awake()
        {
            lastShootPointIndex = 0;
        }
        
        public Transform[] GetNextShootPoints(int count)
        {
            if (count > shootPoints.Length)
                count = shootPoints.Length;
            
            Transform[] points = new Transform[count];
            
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