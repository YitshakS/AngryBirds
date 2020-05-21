using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _cloudParticlePrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // אם אויב פגע באויב כלום לא יתרחש
        if (collision.collider.GetComponent<Enemy>())
            return;

        // אם הציפור פגעה באויב הוא יושמד
        if (collision.collider.GetComponent<Bird>())
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity); // אפקט ענן מתפוצץ
            Destroy(gameObject); // האויב מושמד
            return;
        }

        // אם חפץ אחר (ארגז) מתנגש באויב מלמעלה בזוית הנתונה האויב יושמד
        if (collision.contacts[0].normal.y < -0.5)
            Destroy(gameObject);
    }
}