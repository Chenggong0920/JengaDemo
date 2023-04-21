using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 originalPosition;
    Quaternion originalRotation;

    Vector3 startedPosition;
    Quaternion startedRotation;
    Vector3 desiredPosition;
    Quaternion desiredRotation;

    [SerializeField]
    private float transitionDuration = 1f;

    private float transitionTime = 0f;

    private bool stacksReady = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stacksReady)
            return;


        if( transitionTime < transitionDuration )
        {
            float transitionProgress = transitionTime / transitionDuration;

            transform.position = Vector3.Lerp(startedPosition, desiredPosition, transitionProgress);
            transform.rotation = Quaternion.Lerp(startedRotation, desiredRotation, transitionProgress);

            transitionTime += Time.deltaTime;
        }
    } 

    public void OnStackSelected(int stackIndex)
    {
        desiredPosition = CalculateDesiredCameraPosition(stackIndex);
        desiredRotation = originalRotation;

        startedPosition = transform.position;
        startedRotation = transform.rotation;

        transitionTime = 0f;

        stacksReady = true;
    }

    private Vector3 CalculateDesiredCameraPosition(int stackIndex)
    {
        float xPos = BlockManager.Instance.initialPosition_ZAxis.x + stackIndex * BlockManager.Instance.stack_offset;
        return new Vector3(xPos, originalPosition.y, originalPosition.z);
    }
}
