using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TubeTurning : MonoBehaviour

{
    public float rotationSpeed = 2.0f;

    private bool isRotating = false;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationTime = 0f;

    private RaycastHit hit;

    void Start()
    {
        if (GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRotating)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Tube")
                {
                    StartSmoothRotation(hit.collider.gameObject);
                }
            }
        }

        if (isRotating)
        {
            rotationTime += Time.deltaTime * rotationSpeed;
            hit.collider.gameObject.transform.rotation =
                Quaternion.Slerp(startRotation, targetRotation, rotationTime);

            if (rotationTime >= 1.0f)
            {
                isRotating = false;
                hit.collider.gameObject.transform.rotation = targetRotation;
            }
        }
    }

    void StartSmoothRotation(GameObject target)
    {
        startRotation = target.transform.rotation;

        Vector3 currentEuler = target.transform.eulerAngles;
        Vector3 targetEuler = new Vector3(
            currentEuler.x,
            currentEuler.y - 90f, 
            currentEuler.z
        );

        targetRotation = Quaternion.Euler(targetEuler);

        rotationTime = 0f;
        isRotating = true;

        Debug.Log("Tube object starting smooth rotation!");
    }
}