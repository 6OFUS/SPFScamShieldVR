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
    /// <summary>
    /// Target scale of UI after hovering over it
    /// </summary>
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    /// <summary>
    /// Scaling speed
    /// </summary>
    public float scaleSpeed = 10f;

    /// <summary>
    /// Original scale of UI
    /// </summary>
    private Vector3 originalScale;
    /// <summary>
    /// Target scale of UI
    /// </summary>
    private Vector3 targetScale;


    void Awake()
    {
        originalScale = transform.localScale;
    }
    /// <summary>
    /// When hovering over UI
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;
        StartCoroutine(ScaleAnim());
    }

    /// <summary>
    /// When stop hovering over UI
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        StartCoroutine(ScaleAnim());
    }

    /// <summary>
    /// Animation for scaling UI
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScaleAnim()
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
