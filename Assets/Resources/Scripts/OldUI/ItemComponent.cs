// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class ItemComponent : MonoBehaviour
// {
//     public bool occupied;
//     Vector2[] otherOccupied;
//     int itemID = -1;
//     List<Vector2> allOccupied;
//     ItemData itemData;
//     VisualElement itemElement;
//     Vector2 itemOrigin;
//     VisualElement root;
//     public void Create(bool occupied, int itemID, Vector2 itemOrigin, VisualElement root)
//     {
//         this.root = root;
//         this.occupied = occupied;
//         if(itemID!=-1)
//         {
//             this.itemID = itemID;
//             this.itemOrigin = itemOrigin;
//             this.itemData = ItemFactory.CreateItemData(itemID);
//             this.itemElement = ItemFactory.CreateItemElement(itemData, itemOrigin);
//             this.otherOccupied = GetOtherOccupied(itemOrigin, itemData.displacements);
//             this.allOccupied = new List<Vector2>();
//             this.allOccupied.AddRange(otherOccupied);
//             this.allOccupied.Add(itemOrigin);
//             foreach(Vector2 occ in otherOccupied)
//             {
//                 UIManager.GetGridElement(root, occ).GetComponent<ItemComponent>().Clear();
//                 UIManager.GetGridElement(root, occ).GetComponent<ItemComponent>().occupied = true;
//             }
//         }
//     }


//     public void Clear()
//     {
//         Destroy(this.itemElement);
//         this.occupied = false;
//         this.itemID = -1;
//         this.otherOccupied = null;
//         this.itemData = null;
//         this.itemElement = null;
//         this.itemOrigin = null;
//         this.allOccupied.Clear();//????\
//     }
    
//     private Vector2[] GetOtherOccupied(Vector2 initial, Vector2[] displacements)
//     {
//         Vector2[] result = new Vector2[displacements.Length];
//         for(int i = 0; i < displacements.Length;i++)
//         {
//             result[i]=initial+displacements[i];
//         }
//         return result;
//     }


//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
