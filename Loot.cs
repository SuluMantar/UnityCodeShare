using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private BoxCollider2D collider;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    InventoryManager inventory;
    [SerializeField]
    private int amountToAdd;

    private Item item;

    public void Initialize(Item item, InventoryManager inventory)
    {
        this.item = item;
        sr.sprite = item.image;
        this.inventory = inventory;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveAndCollect(other.transform));
            inventory.AddItem(item, amountToAdd);
            inventory.AddItemToDic(item, amountToAdd);
        }


    }





    private IEnumerator MoveAndCollect(Transform target)
    {
        Destroy(collider);

        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return 0;
        }

        Destroy(gameObject);

    }
}
