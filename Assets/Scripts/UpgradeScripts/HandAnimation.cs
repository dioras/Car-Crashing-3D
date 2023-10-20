using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandAnimation : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 initialPosition;
    public RectTransform targetTransform; // Hedef olarak kullanılacak RectTransform
    public float moveSpeed = 100f;
    public float waitTime = 1f; // Tıklama animasyonu süresi

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.transform.position;
        StartCoroutine(AnimateHand());
    }

    private IEnumerator AnimateHand()
    {
        Vector2 targetPosition = targetTransform.transform.position; // Hedef RectTransform'un pozisyonunu al

        while (true)
        {
            // Hedefe hareket
            while (Vector2.Distance(rectTransform.transform.position, targetPosition) > 0.1f)
            {
                rectTransform.transform.position = Vector2.MoveTowards(rectTransform.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Tıklama animasyonu (burada basit bir ölçekleme kullanıyorum)
            float timer = 0f;
            Vector3 initialScale = rectTransform.localScale;
            while (timer < waitTime)
            {
                timer += Time.deltaTime;
                float scaleValue = 1f + Mathf.Sin(timer * Mathf.PI / waitTime) * 0.1f; // 0.1f tıklama sırasında ne kadar "büyüme" olacağını belirtir
                rectTransform.localScale = initialScale * scaleValue;
                yield return null;
            }
            rectTransform.localScale = initialScale;

            // Başlangıç pozisyonuna geri dön
            while (Vector2.Distance(rectTransform.transform.position, initialPosition) > 0.1f)
            {
                rectTransform.transform.position = Vector2.MoveTowards(rectTransform.transform.position, initialPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(1f); // İstediğiniz bir bekleme süresi ekleyebilirsiniz
        }
    }
}
