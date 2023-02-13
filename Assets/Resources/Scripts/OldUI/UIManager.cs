// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;
// using System.Linq;

// public class UIManager : MonoBehaviour
// {
//     public static Vector2 selectedGrid;
//     public static boolean dragging = false;
//     public static List<Vector2> freeGrid;

//     void OnEnable()
//     {
//         VisualElement root = GetComponent<UIDocument>().rootVisualElement;

//     }

//     void Update()
//     {

//     }

//     public static bool isOccupied(VisualElement root, Vector2 pos)
//     {
//         return GetGridElement(root,pos).GetComponent<ItemComponent>().occupied;
//     }

//     public static VisualElement GetGridElement(VisualElement root, Vector2 pos)
//     {
//         return root.Children().ToArray()[pos.x].Children().ToArray()[pos.y];
//     }

//     private VisualElement[,] GetGrid(VisualElement root)
//     {
//         int len = root.Children().ToArray().Length;
//         int wid = root.Children().ToArray()[0].Children().ToArray().Length;
//         VisualElement[,] grid = new VisualElement[len,wid];
//         for(int row = 0;row<len;row++)
//         {
//             for(int column=0;column<wid;column++)
//             {
//                 grid[row,column] = root.Children().ToArray()[row].Children().ToArray()[column];
//             }
//         }
//         return grid;
//     }

//     private void SetupGrid(VisualElement root)
//     {
//         int len = root.Children().ToArray().Length;
//         int wid = root.Children().ToArray()[0].Children().ToArray().Length;
//         VisualElement[,] grid = new VisualElement[len,wid];
//         for(int row = 0;row<len;row++)
//         {
//             for(int column=0;column<wid;column++)
//             {
//                 root.Children().ToArray()[row].Children().ToArray()[column].RegisterCallback<MouseMoveEvent>(()=>{
//                         root.GetComponent<GridComponent>().HandleClick(new Vector2(row,column));
//                     }
//                 );

//                 (root.Children().ToArray()[row].Children().ToArray()[column].AddComponent<ItemComponent>() as ItemComponent).Create(false, null,null,root);

//             }
//         }
//     }


// }
