using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController<T> where T : Item
{
    private List<T> itemCollection = new List<T>();

    public void AddItem(T item)
    {
        itemCollection.Add(item);
    }

    // Searches items with the given name in list
    public T GetItem(string name)
    {
        foreach(T item in itemCollection) 
        {
            if(item.itemName == name)
            {
                return item;
            }

        }
        return null;
    }

    // searches the Item with the given name in list and uses it
    public void UseItem(string name)
    {
        foreach(T item in itemCollection) 
        {
            if(item.itemName == name) 
            {
                item.Use();
            }
        }
    }
}
