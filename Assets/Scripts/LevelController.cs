using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    static int _nextLevelIndex = 1; // כל המופעים של המחלקה יחלקו את אותו אינדקס של השלב הבא
    Enemy[] _enemies; // מערך המחזיק את כל האויבים בשלב הנוכחי

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>(); // אחתול המערך בכל האויבים בשלב הנוכחי

        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log(_enemies.Length);

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Enemy enemy in _enemies)
        {
            if (enemy) // אם יש עדיין אויב
                return; // לא יבוצע כלום
        }

        Debug.Log("All enemies were destroyed");

        // אם כל האויבים הושמדו יטען השלב הבא
        _nextLevelIndex++;
        SceneManager.LoadScene("Level" + _nextLevelIndex);
    }
}