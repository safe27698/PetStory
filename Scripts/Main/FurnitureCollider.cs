using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FurnitureCollider : Item
{
    public FurnitureType furnitureType;
    public Vector3 position;
    public Rigidbody2D rigid;

    public bool findSpace;

    public List<Collider2D> ignoreList = new List<Collider2D>();

    public void Update()
    {
        Debug.DrawRay(transform.position, Vector3.up);
        Debug.DrawRay(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.left);
        Debug.DrawRay(transform.position, Vector3.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!findSpace)
            return;

        switch (furnitureType)
        {
            case FurnitureType.Windows:
                if (collision.collider.tag == "Accessories" || collision.collider.tag ==  "Furniture")
                {
                    Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
                    ignoreList.Add(collision.collider);
                }
                else if (collision.collider.tag == "Floor")
                {
                    foreach(Collider2D col in ignoreList)
                        Physics2D.IgnoreCollision(col, GetComponent<Collider2D>(), false);

                    findSpace = false;
                }
                break;
            case FurnitureType.Floor:
                if (collision.collider.tag == "Floor" || collision.collider.tag == "Furniture")
                {
                    findSpace = false;
                }
                break;
            case FurnitureType.Decoration:
                if (collision.collider.tag == "Ceiling")
                {
                    findSpace = false;
                }
                break;
        }

    }
}
