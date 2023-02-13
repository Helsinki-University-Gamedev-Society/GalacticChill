// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class GridComponent : MonoBehaviour
// {
//     Vector2 size;
//     public void SetSize(Vector2 size)
//     {
//         this.size = size;
//     }

//     public void HandleClick(Vector2 inputPos)
//     {
        
//         UIManager.freeGrid = UIManager.selectedGrid.GetComponent<ItemComponent>().allOccupied;
//         if(!UIManager.isOccupied(root, inputPos) && UIManager.dragging 
//             && !IsOOB(inputPos, UIManager.selectedGrid.GetComponent<ItemComponent>().itemData.displacements)
//             && IsFree(inputPos, UIManager.selectedGrid.GetComponent<ItemComponent>().itemData.displacements))
//         {
//             resetGridElements(UIManager.freeGrid);
//             placeItem(ItemFactory.CreateItemElement(ItemFactory.CreateItemData(itemID)), ItemFactory.CreateItemData(itemID), inputPos);
//         }
//     }

//     private void resetGridElements(List<Vector2> elements)
//     {
//         foreach(Vector2 elem in elements)
//         {
//             UIManager.GetGridElement(this, elem).Clear();

//         }
//     }

//     private void placeItem(ItemElement element, ItemData data, Vector2 pos)
//     {
//         element.style.translate = new Vector2(this.size.x + );
//         UIManager.GetGridElement(this, pos).GetComponent<ItemComponent>().

//     }

//     private void IsFree(Vector2 initialPos, Vector2[] displacements)
//     {
//         foreach(Vector2 displacement in displacements)
//         {
//             if(UIManager.GetGridElement(this, initialPos+displacement).occupied && !UIManager.freeGrid.Contains(initialPos+displacement))
//                 {
//                     return false;
//                 }
//         }

//         return true;
//     }

//     private void IsOOB(Vector2 initialPos, Vector2[] displacements)
//     {
//         foreach(Vector2 displacement in displacements)
//         {
//             if((initialPos+displacement).x<0 || (initialPos+displacement).y<0
//                 || (initialPos+displacement).x>size.x-1 || (initialPos+displacement).y>size.y-1)
//                 {
//                     return true;
//                 }
//         }

//         return false;
//     }
    
// }
