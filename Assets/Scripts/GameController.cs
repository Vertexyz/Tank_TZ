using UnityEngine.SceneManagement;

public class GameController
{
    
    public static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameController();

            return _instance;
        }
    }

    public int EnemiesCount = 0;
    public int MaxEnemiesCount = 10;

    public void RestartGame()
    {
        ScoreLabel.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
