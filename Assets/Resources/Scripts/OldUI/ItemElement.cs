// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;

// public class ItemElement : VisualElement
// {
//     private Vector2 originalPos;
//     private bool isDragging = false;
//     private ItemData item;
//     public string desc;
//     public string spriteName;

//     //GENERATED FROM ITEM DATA
//     public ItemElement(ItemData item)
//     {
//         this.item = item;
//         name = item.name;
//         desc = item.desc;
//         spriteName = item.spriteName;

//         style.height = item.width;
//         style.width = item.height;
//         style.visibility = Visibility.Hidden;

//         VisualElement icon = new VisualElement
//         {
//             style = { backgroundImage = ResourceManager.Instance.ImportTexture($"Assets/Resources/Tile/Sprites/{spriteName}.png") }
//         };
//         Add(icon);

//     }

//     public void SetPosition(Vector2 pos)
//     {
//         style.left = pos.x;
//         style.top = pos.y;
//     }

//     private void OnMouseDown(MouseDownEvent e)
//     {
//         if(!isDragging) 
//         {
//             isDragging=true;
//             return;
//         }

//         //Check for conflict and replace after click

//     }

//     private void OnMouseMove(MouseDownEvent e)
//     {
//         if (!isDragging) return;
//         SetPosition(e.mousePosition);
//     }


    
// }
