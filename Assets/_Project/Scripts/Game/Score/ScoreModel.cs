namespace GameScene.Models
{
    public class ScoreModel
    {
        public float Score { get; private set; }
        
        public void AddScore(int scoreSize)
        {
            Score += scoreSize;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}