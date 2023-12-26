using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockKeys
{
    public static bool inventoryOpen = false;
    public static bool dialogOpen = false;

    public static void InventoryOpened() 
    {
        inventoryOpen = true;
        Debug.Log($"inventoryOpen = {inventoryOpen}");
    }
    public static void InventoryClosed()
    {
        inventoryOpen = false;
        Debug.Log($"inventoryOpen = {inventoryOpen}");
    }
    public static void DialogOpened() 
    { 
        dialogOpen = true;
        Debug.Log($"dialogOpen = {dialogOpen}");
    }
    public static void DialogClosed() 
    { 
        dialogOpen = false;
        Debug.Log($"dialogOpen = {dialogOpen}");
    }
}
