using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiCollide : MonoBehaviour
{
	public GameObject weapon;

	[HideInInspector]
	public AutoMoveAttack parent;
	bool canUlti = false;
	List<Collider2D> colliders = new List<Collider2D>();


	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        handleObjectInRangeUlti(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        handleObjectInRangeUlti(collision);
    }

    void handleObjectInRangeUlti(Collider2D collision){
        if (canUlti && GameObject.ReferenceEquals( collision.gameObject, parent.objectToChase))
        {
            if (!colliders.Contains(collision))
            {
                colliders.Add(collision);
                parent.handleObjectInRangeUlti(collision.gameObject);
            }

        }
    }


    public void CanUlti(bool canUlti){
    	this.canUlti = canUlti;
    	colliders.Clear();
    }

    public void Fire(){

    	// TODO: custom ulti for each characters

    	print("firing ulti...");
    	var obj = Instantiate(weapon, transform.position, transform.rotation);
    	obj.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * 7f;
    }

}
