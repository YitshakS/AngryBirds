using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Bird : MonoBehaviour
{
    Vector3 _initialPosition; // מיקומה ההתחלתי של הציפור לפני השיגור
    bool _birdWasLaunched; // האם הציפור שוגרה
    float _timeSittingAround; // הזמן מרגע שהציפור נחתה

    [SerializeField] float _launchPower; // כוח השיגור

    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] Image Background;

    void Awake()
    {
        _initialPosition = transform.position; // שימור מיקומה ההתחלתי של הציפור לפני השיגור
    }

    void OnMouseDown() // כשהעכבר לחוץ על הציפור
    {
        GetComponent<SpriteRenderer>().color = Color.red; //  הציפור תיצבע באדום
        GetComponent<LineRenderer>().enabled = true; // החיצים לכיוון שתשוגר יראו
    }

    void OnMouseUp() // כשהעכבר עוזב את הציפור
    {
        GetComponent<SpriteRenderer>().color = Color.white; // הציפור תוחזר לצבעה המקורי
        GetComponent<LineRenderer>().enabled = false; // החיצים לכיוון שתשוגר יעלמו
        GetComponent<Rigidbody2D>().AddForce((_initialPosition - transform.position) * _launchPower); // העפת הציפור לכיוון הנגדי מהכיוון שאליו העכבר גרר אותה
        GetComponent<Rigidbody2D>().gravityScale = 1; // החזרת כח המשיכה
        _birdWasLaunched = true;
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);

        if (transform.position.x < -15.01f)
            transform.position = new Vector3(-15.01f, transform.position.y);

        if (transform.position.x > 15.05f)
            transform.position = new Vector3(15.05f, transform.position.y);

        if (transform.position.y < -5.326718f)
            transform.position = new Vector3(transform.position.x, -5.326718f);

        if (transform.position.y > 7.39f)
            transform.position = new Vector3(transform.position.x, 7.39f);

    }

    void Update()
    {
        // חיצים מהציפור לכיוון שתשוגר
        GetComponent<LineRenderer>().SetPosition(0, transform.position); // ממיקום גרירת הציפור
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition); // למיקומה ההתחלתי

        if (
                _birdWasLaunched // אם הציפור שוגרה
                &&
                GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1 // 0.1 והמהירות המשוכללת על שני הצירים לא גדולה מ
           )
            _timeSittingAround += Time.deltaTime; // הזמן מרגע שהציפור נחתה שווה למה שהיה + הזמן מאז הפרם האחרון

        // אם הציפור מחוץ למסך
        if (transform.position.x < -20.01f || transform.position.x > 20.05f || transform.position.y < -10.326718f || transform.position.y > 15 || _timeSittingAround > 3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // הסצנה תוטען מחדש
    }
}