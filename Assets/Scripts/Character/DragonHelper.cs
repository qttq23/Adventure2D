using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHelper : MonoBehaviour
{


    public GameObject firePrefab;
    public Transform shooterTransform;


    public delegate void OnUltiAtRightPoint();
    public event OnUltiAtRightPoint EventUltiAtRightPoint;
    public delegate void OnUltiDone();
    public event OnUltiDone EventUltiDone;






    public void HandleAttackAtRightPoint()
    {

        print("DragonHelper.cs: attack right point");
        throwFire();

    }


    public void HandleUltiAtRightPoint()
    {
        print("DragonHelper.cs: attack ulti point");
        EventUltiAtRightPoint?.Invoke();
        throwFire();
    }


    public void HandleUltiAtEndPoint()
    {
        print("DragonHelper.cs: attack ulti done");
        EventUltiDone?.Invoke();

    }

    void throwFire()
    {
        // throw a fire
        GameObject fire = Instantiate(firePrefab,
            shooterTransform.position,
            transform.rotation);

        var controller = gameObject.GetComponent<MyCharacterController>();
        var scale = fire.transform.localScale;
        fire.transform.localScale = new Vector3(
            scale.x * (controller.isTurningRight ? 1 : -1),
            scale.y,
            scale.z
            );
        fire.GetComponent<Rigidbody2D>().velocity =
        3f * new Vector2(1 * (controller.isTurningRight ? 1 : -1), 0);


    }




}
