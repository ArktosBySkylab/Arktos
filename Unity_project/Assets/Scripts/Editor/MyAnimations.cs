using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using XDiffGui;

public abstract class MyAnimations : EditorWindow
{

    protected string _name = "";
    
    protected virtual void CreateAnimation(string output, string moveType, string name)
    {
        Debug.Log($"Create Animation {output} ; {name}_{moveType}");
        Sprite[] sprites = Resources.LoadAll<Sprite>($"Animations/SourceImages/{output}/{moveType}/"); // load all sprites in "assets/Resources/sprite" folder
        AnimationClip animClip = new AnimationClip();
        AnimationClipSettings clipSettings = new AnimationClipSettings();
        clipSettings.loopTime = true;
        animClip.frameRate = sprites.Length; // FPS
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite"; 
        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];
        for(int i = 0; i < (sprites.Length); i++) {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();
            spriteKeyFrames[i].time = (float) i / (sprites.Length);
            spriteKeyFrames[i].value = sprites[i];
        }
        
        // apply settings and keyframes to the clip
        AnimationUtility.SetAnimationClipSettings(animClip, clipSettings);

        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);
        
        AssetDatabase.CreateAsset(animClip, $"Assets/Resources/Animations/{output}/{name}_{moveType}.anim");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


   public virtual void OnGUI()
    {
        GUILayout.Label("Welcome in the custom animations creator !");
        GUILayout.Label("Take a look to the readme in Resources/Animations/SourceImages to format correclty all the frames ;)");
        _name = EditorGUILayout.TextField("Name: ", _name);
    }
}