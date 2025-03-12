using UnityEngine;

namespace GameScene.Entities.Player
{
    public class Player
    {
        public void Teleport(GameObject character, Vector2 direction)
        {
            character.transform.position = new Vector2(character.transform.position.x, character.transform.position.y) * direction;
        }
    }
}