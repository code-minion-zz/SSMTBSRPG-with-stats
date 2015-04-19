using UnityEngine;

namespace UnitySampleAssets.Effects
{
    public class WaterHoseParticles : MonoBehaviour
    {
        private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
        public static float lastSoundTime;
        public float force = 1;

        private void OnParticleCollision(GameObject other)
        {
            ParticleSystem particleSystem = GetComponent<ParticleSystem>();

            int safeLength = particleSystem.GetSafeCollisionEventSize();//.safeCollisionEventSize;

            if (collisionEvents.Length < safeLength)
            {
                collisionEvents = new ParticleCollisionEvent[safeLength];
            }

            int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
            int i = 0;

            while (i < numCollisionEvents)
            {

                if (Time.time > lastSoundTime + 0.2f)
                {
                    lastSoundTime = Time.time;
                }

                var col = collisionEvents[i].collider;

                if (col.attachedRigidbody != null)
                {
                    Vector3 vel = collisionEvents[i].velocity;
                    col.attachedRigidbody.AddForce(vel*force, ForceMode.Impulse);
                }

                other.BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);

                i++;
            }
        }
    }
}