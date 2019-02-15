using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManager : MonoBehaviour
{
    public GameObject gravityManager;
    private GravityManager _gravityManager;

    void Start()
    {
        _gravityManager = gravityManager.GetComponent<GravityManager>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Input.GetMouseButton(0))
        {
            Transform arrow = transform.GetChild(0);
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _gravityManager.Shoot(Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + 45) * new Vector3(0.9f, 0.9f));
        }
    }
}
