using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScene.Entities.Asteroid
{
    public class Asteroid
    {
        public void Activate(GameObject thisObject, Vector2 positionSpawn, Rigidbody2D rb, Vector2 velocity, float sprayVelocity)
        {
            thisObject.SetActive(true);
            thisObject.transform.position = positionSpawn;
            
            float velocityX = Random.Range(velocity.x - sprayVelocity, velocity.x + sprayVelocity);
            float velocityY = Random.Range(velocity.x - sprayVelocity, velocity.x + sprayVelocity);
            Vector2 newVelocity = new Vector2(velocityX, velocityY);
            rb.linearVelocity = newVelocity;
        }
        
        public void Deactivate(GameObject thisObject)
        {
            thisObject.SetActive(false);
        }
    }
}