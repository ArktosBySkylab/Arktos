using System;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class MyAnimations : EditorWindow
{
    [MenuItem("Window/UI Toolkit/MyAnimations")]
    public static void Init()
    {
        MyAnimations wnd = GetWindow<MyAnimations>();
        wnd.titleContent = new GUIContent("MyAnimations");
        wnd.Show();
    }
    
    private static void CreateAnimation(string filename, string moveType)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(filename + "/" + moveType); // load all sprites in "assets/Resources/sprite" folder
        AnimationClip animClip = new AnimationClip();
        AnimationClipSettings clipSettings = new AnimationClipSettings();
        clipSettings.loopTime = true;
        animClip.frameRate = sprites.Length * 2;   // FPS
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite"; 
        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];
        for(int i = 0; i < (sprites.Length); i++) {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();
            spriteKeyFrames[i].time = i;
            spriteKeyFrames[i].value = sprites[i];
        }
        
        // apply settings and keyframes to the clip
        AnimationUtility.SetAnimationClipSettings(animClip, clipSettings);

        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);
        
        AssetDatabase.CreateAsset(animClip, $"Assets/Animation/{filename}_{moveType}.anim");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("My animation creator");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editors/MyAnimations.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editors/MyAnimations.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);
    }

    public void OnGUI()
    {
        Debug.Log("TEST");
        string folder = EditorGUILayout.TextField("Folder Name: ");
        string move = EditorGUILayout.TextField("Move Name: ");
    }
}