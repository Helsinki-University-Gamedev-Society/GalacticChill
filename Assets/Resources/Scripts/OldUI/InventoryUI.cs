/* using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class InventoryUI : EditorWindow
{
    

    [MenuItem("Window/UI Toolkit/InventoryUI")]
    public static void ShowExample()
    {
        InventoryUI wnd = GetWindow<InventoryUI>();
        wnd.titleContent = new GUIContent("InventoryUI");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/Scripts/InventoryUI.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
    }
} */