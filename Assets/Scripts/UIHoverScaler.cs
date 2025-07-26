/*
    Author: Kevin Heng
    Date: 25/07/2025
    Description: The UIHoverScaler class is used for the pop up effect when hovering over buttons in the main menu
*/
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    public float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;


    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;
        StartCoroutine(ScaleAnim());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        StartCoroutine(ScaleAnim());
    }

    private IEnumerator ScaleAnim()
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }

        transform.localScale = targetScale; // Snap exactly to target
    }
}
