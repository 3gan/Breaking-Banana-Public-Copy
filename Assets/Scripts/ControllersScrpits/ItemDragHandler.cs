using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    public float mindDropDistance = 2f;
    public float maxDropDistance = 3f;

    private int orginalSlotItemAmount = 0;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;

        Slots originalSlot = originalParent.GetComponent<Slots>();
        orginalSlotItemAmount = originalSlot.GetCurrentItem().GetAmountInStack();
        originalSlot.itemAmmountDisplay.text = "0";
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slots dropSlot = eventData.pointerEnter?.GetComponent<Slots>();

     

        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if (dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slots>();
                
            }
        }

        

        Slots originalSlot = originalParent.GetComponent<Slots>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {

                //check if item is the same item begin swaped to stack
                if (dropSlot.GetCurrentItem().GetItemID() == originalSlot.GetCurrentItem().GetItemID() && originalSlot.GetInstanceID() != dropSlot.GetInstanceID())
                {
                    //check if the amount begin staked will exceed the maximum staxk limmit
                    if (dropSlot.GetCurrentItem().GetAmountInStack() + originalSlot.GetCurrentItem().GetAmountInStack() > originalSlot.GetCurrentItem().GetMaxStackAmount())
                    {
                        transform.SetParent(originalParent);
                        originalSlot.GetCurrentItem().amountInStack -= dropSlot.GetCurrentItem().GetMaxStackAmount() - dropSlot.GetCurrentItem().GetAmountInStack();
                        dropSlot.GetCurrentItem().amountInStack = originalSlot.GetCurrentItem().GetMaxStackAmount();

                        dropSlot.itemAmmountDisplay.text = dropSlot.GetCurrentItem().GetAmountInStack().ToString();
                        originalSlot.itemAmmountDisplay.text = originalSlot.GetCurrentItem().GetAmountInStack().ToString();
                    }
                    else
                    {
                        int holdNum = dropSlot.GetCurrentItem().GetAmountInStack();

                        dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                        originalSlot.currentItem = dropSlot.currentItem;
                        dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                        //move item into drop slot
                        transform.SetParent(dropSlot.transform);
                        dropSlot.currentItem = gameObject;

                        Destroy(originalSlot.transform.GetChild(1).gameObject);

                        dropSlot.GetCurrentItem().amountInStack += holdNum;
                        dropSlot.itemAmmountDisplay.text = dropSlot.GetCurrentItem().GetAmountInStack().ToString();//change text


                    }

                }
                else
                {
                    dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                    originalSlot.currentItem = dropSlot.currentItem;
                    dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    //swap the amount of items in the slot
                    int holdNum = dropSlot.GetCurrentItem().GetAmountInStack();
                    dropSlot.GetCurrentItem().amountInStack = originalSlot.GetCurrentItem().GetAmountInStack();
                    originalSlot.GetCurrentItem().amountInStack = holdNum;
                    originalSlot.itemAmmountDisplay.text = originalSlot.GetCurrentItem().GetAmountInStack().ToString();//change text

                    //move item into drop slot
                    transform.SetParent(dropSlot.transform);
                    dropSlot.currentItem = gameObject;
                    dropSlot.itemAmmountDisplay.text = dropSlot.GetCurrentItem().GetAmountInStack().ToString();//change text

                }

            }
            else
            {
                originalSlot.currentItem = null;

                //move item into drop slot
                transform.SetParent(dropSlot.transform);
                dropSlot.currentItem = gameObject;
                dropSlot.itemAmmountDisplay.text = dropSlot.GetCurrentItem().GetAmountInStack().ToString();//change text

            }
            dropSlot.GetCurrentItem().transform.localScale = Vector2.one;
        }
        else
        {
            if (!isWithinInventory(eventData.position))
            {
                //drop item
                DropItem(originalSlot);
            }
            else
            {
                //send back to orginalslot
                transform.SetParent(originalParent);
                originalSlot.itemAmmountDisplay.text = originalSlot.GetCurrentItem().GetAmountInStack().ToString();
            }
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
       
    }

    bool isWithinInventory(Vector2 mousePos)
    {
        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePos);
    }

    void DropItem(Slots originalSlot)
    {
        originalSlot.currentItem = null;

        //Find player
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.Log("Missing 'player' tag");
            return;
        }
        //randomDrop position
        Vector2 dropOffset = new(0f, -2f);
        Vector2 dropPosition = (Vector2)playerTransform.position + dropOffset;

        //instatiate dropped item
        GameObject dropItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        dropItem.GetComponent<BounceEffect>().StartBounce();
        dropItem.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        //remove ui item
        Destroy(gameObject);
    }
}

