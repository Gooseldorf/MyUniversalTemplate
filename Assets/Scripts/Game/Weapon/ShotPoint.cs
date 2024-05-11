using Unity.Mathematics;
using UnityEngine;

namespace Game.Weapon
{
    public class ShotPoint : MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private Transform muzzle;

        public float3 GetDirection() => muzzle.position - center.position;
        public float3 GetCenterPosition() => center.position;
        public Quaternion GetRotation() => Quaternion.LookRotation(muzzle.position - center.position);
    }
}
