using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Base info")]
    public Vector3 basePosition;
    public Quaternion baseRotation;
    public Rigidbody rb;
    [Space]
    [Header("Lock door info")]
    public bool isLocked = false;
    public float positionThreshold = 0.1f;
    public float rotationThreshold = 5f;
    public float checkInterval = 0.1f;

    void Start()
    {
        basePosition = transform.position;
        baseRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            StartCoroutine(WaitAndLockDoor());
        }
        if (Input.GetKey(KeyCode.L))
        {
            UnlockDoor();
        }
    }

    public IEnumerator WaitAndLockDoor()
    {
        while (!IsAtBasePosition() || !IsAtBaseRotation())
        {
            yield return new WaitForSeconds(checkInterval);
        }
        LockDoor();
    }

    public void LockDoor()
    {
        rb.isKinematic = true;
        transform.position = basePosition;
        transform.rotation = baseRotation;
        isLocked = true;
    }

    public void UnlockDoor()
    {
        rb.isKinematic = false;
        isLocked = false;
    }

    private bool IsAtBasePosition()
    {
        return Vector3.Distance(transform.position, basePosition) < positionThreshold;
    }

    private bool IsAtBaseRotation()
    {
        float angle = Quaternion.Angle(transform.rotation, baseRotation);
        return angle < rotationThreshold;
    }
}
