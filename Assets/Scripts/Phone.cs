using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Phone : MonoBehaviour
{
    public XRBaseInteractable interactable;

    public GameObject firstVibration;
    public GameObject secondVibration;
    public GameObject pickUp;
    public GameObject glow;
    public Animator animator;
    public AudioSource audioSource;
    public GameObject choiceContainer;
    public GameObject socket;
    
    public void OnPickUp(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;

        if (interactor is XRSocketInteractor)
        {
            // Object was placed in socket — ignore or handle specially
            return;
        }
        else
        {
            firstVibration.SetActive(false);
            secondVibration.SetActive(false);
            pickUp.SetActive(false);
            glow.SetActive(false);
            audioSource.Stop();
            animator.enabled = false;
            choiceContainer.SetActive(true);
            socket.SetActive(true);
        }
    }
    public void OnDrop(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject;

        if (interactor is XRSocketInteractor)
        {
            // Object was placed in socket — ignore or handle specially
            return;
        }
        else
        {
            firstVibration.SetActive(true);
            secondVibration.SetActive(true);
            if (pickUp == null)
                Debug.LogWarning("pickUp GameObject is NOT assigned!");
            else
            {
                Debug.Log("Activating pickUp");
                pickUp.SetActive(true);
            }
            glow.SetActive(true);
            audioSource.Play();
            animator.enabled = true;
            choiceContainer.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        interactable.selectEntered.AddListener(OnPickUp);
        interactable.selectExited.AddListener(OnDrop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
