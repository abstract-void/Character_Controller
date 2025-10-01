using UnityEngine;

namespace ProjectDew.CharacterController
{
    public class Raycaster
    {
        private const float SAFETY_DISTANCE_FACTOR = 0.001f; // Small factor added to prevent clipping issues when the sensor range is calculated

        private readonly Controller controller;

        private LayerMask layerMask;

        private RaycastHit hitInfo;

        public Collider HitCollider => hitInfo.collider;

        public Vector3 HitNormal => hitInfo.normal;

        public float DistanceToHit => RayLength - hitInfo.distance;

        public bool HasDetectedHit => hitInfo.collider != null;

        public float RayLength { get; private set; }

        public Raycaster(Controller controller, float baseRayLength)
        {
            this.controller = controller;

            RayLength = baseRayLength;

            CalculateLayerMask();
        }

        public int CalculateLayerMask()
        {
            int objectLayer = controller.Transform.gameObject.layer;

            layerMask = Physics.AllLayers;

            for (int i = 0; i < 32; i++)
            {
                if (Physics.GetIgnoreLayerCollision(objectLayer, i))
                {
                    layerMask &= ~(1 << i);
                }
            }

            int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");

            layerMask &= ~(1 << ignoreRaycastLayer);

            return layerMask.value;
        }

        public void CastRay(Vector3 origin, Vector3 direction) => CastRay(origin, direction, RayLength);

        public void CastRay(Vector3 origin, Vector3 direction, float length)
        {
            if (direction.magnitude > Math.One)
            {
                direction.Normalize();
            }

			float adjustedLength = length * (Math.One + SAFETY_DISTANCE_FACTOR);

            Physics.Raycast(origin, direction, out hitInfo, adjustedLength, layerMask, QueryTriggerInteraction.Ignore);
        }
    }
}