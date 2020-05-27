<p dir="rtl">משחק זה פותח בהשראת המשחק Angry Birds Classic שיצא בשנת 2009.</p>
<p dir="rtl">במשחק המקורי, בכל שלב מוקצת כמות של ציפורים שניתן לשגר, ולפגוע באמצעותן בחזירונים שגנבו להן את הביצים. החזירונים מוגנים מאחורי חפצים שונים וכן ישנם סוגים שונים של ציפורים בעלי יכולות שונות.</p>
<p dir="rtl">בגרסה זו, השחקן מאותגר כך שכדי לעבור שלב עליו לחסל את כל האויבים בשיגור אחד בלבד. הוא אינו מוגבל בכמות הציפורים, וכן כמות האויבים הוקטנה, אך כדי לפגוע בכולם עליו לתמרן אל היעד, לדוגמה לפגוע בעץ כדי להתגלגל לאחור וליפול על האויב המסתתר מאחורי שלט.</p>
<p dir="rtl" align="center"><img src="https://tikshoret.net/wp-img/game2.gif" /></p>
<p dir="rtl" align="center"><a href="https://tikshoret.net/wp-img/game.mp4">לחצו כאן לצפיה בסרטון</a></p>
<p dir="rtl">בפיתוח הושם דגש על ניצול המנוע של Unity כדי לחסוך בכתיבת קוד מיותרת וכן הושם דגש על קוד מודולרי שיאפשר שינויים בעתיד.</p>
<p dir="rtl">כשהשחקן מושך לאחור את הציפור בעזרת סמן העכבר, מופיעה לו הזווית אליה תשוגר הציפור. הצגת הזווית נעשית באמצעות הרכיב LineRenderer והשיגור באמצעות הוספת כח ע"י הפונקציה AddForce המשויכת לרכיב Rigidbody2D.</p>
<p dir="rtl">על מנת שהשיגור ידמה את המציאות, כך שככל שהציפור תמשך לאחור כך היא תשוגר רחוק יותר וכן שמהירות השיגור תהיה גבוהה יותר קרוב לשיגור ונמוכה יותר קרוב לנחיתה נעשה שימוש בנוסחה הבאה:</p>

```cs
(_initialPosition - transform.position) * _launchPower
```
<p dir="rtl">כאשר:</p>
<ul dir="rtl">
  <li><span dir="ltr">_initialPosition</span> הוא מיקומה ההתחלתי של הציפור בטרם השיגור</li>
  <li>transform.position הוא מיקומה הנוכחי של הציפור</li>
  <li><span dir="ltr">_launchPower</span> הוא עוצמת השיגור</li>
</ul>
<p dir="rtl">בתום השיגור מוחזר כוח המשיכה לציפור ע"י הצבת הערך 1 בפרמטר gravityScale המשויך לרכיב Rigidbody2D.</p>
<p dir="rtl">אם השחקן חיסל את כל האויבים בשיגור אחד הוא יועבר לשלב הבא. אחרת, אם הציפור משוגרת מחוץ לגבולות המסך השלב הנוכחי יחל מחדש ואם היא סיימה את השיגור תהיה השהיה קצרה לפני שהשלב יחל מחדש, כדי שהשחקן יספיק לראות את הנחיתה. זיהוי עוצמת השיגור וההשהיה נעשו באופן הבא:</p>

```cs
if (
	_birdWasLaunched // אם הציפור שוגרה
	&&
	GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1 // 0.1 והמהירות אינה גדולה מ
   )
	_timeSittingAround += Time.deltaTime; // הזמן מרגע שהציפור נחתה שווה למה שהיה + הזמן מאז הפרם האחרון
```

<p dir="rtl">כללי הפגיעה באויב ואפקט הפיצוץ נקבעו כך:</p>

```cs
[SerializeField] GameObject _cloudParticlePrefab; // פרפאב של אפקט ענן מתפוצץ

private void OnCollisionEnter2D(Collision2D collision)
{
	// אם אויב פגע באויב כלום לא יתרחש
	if (collision.collider.GetComponent<Enemy>())
		return;

	// אם הציפור פגעה באויב הוא יושמד
	if (collision.collider.GetComponent<Bird>())
	{
		Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity); // הפעלת אפקט ענן מתפוצץ במקום בו ניצב האויב
		Destroy(gameObject); // האויב מושמד
		return;
	}

	// אם חפץ אחר (לדוגמה ארגז) מתנגש באויב מלמעלה בזוית הנתונה האויב יושמד
	if (collision.contacts[0].normal.y < -0.5)
		Destroy(gameObject);
}
```

<p dir="rtl">כדי לבדוק האם בשלב הנוכחי חוסלו כל האויבים בדקנו באמצעות הפונקציה FindObjectsOfType האם נותרו עצמים מסוג Enemy.</p>
<p dir="rtl">ניהול הסצנות נעשה באמצעות הפונקציה SceneManager.LoadScene כאשר השתמשנו במשתנה סטטי (שנוצר ממנו רק עותק אחד) המחזיק את השלב הנוכחי.</p>
