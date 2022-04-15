using System;
using Photon.Pun;
using Playground.Characters.Heros;
using Playground.Characters.Monsters;
using Playground.Weapons;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Editor.bin
{
    public class WeaponCreation : MyAnimations
    {
        private string[] AnimatorParameters = {"IsFighting"};
        private int characterNb = 0;
        private readonly string[] weapons = Enum.GetNames(typeof(WeaponsNames));
        
        [MenuItem("Window/MyAnimations/WeaponsCreation")]
        private static void ShowWindow()
        {
            var window = GetWindow<WeaponCreation>();
            window.titleContent = new GUIContent("Weapon's creation");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Welcome in the custom animations creator !");
            GUILayout.Label("Take a look to the readme in \nResources/Animations/SourceImages to format correclty\n all the frames ;)");
            
            GUILayout.BeginVertical("Box");
            characterNb = GUILayout.SelectionGrid(characterNb, weapons, weapons.Length);
            GUILayout.EndVertical();
            
            
            if (GUILayout.Button("Generer tout (regeneration des animations)"))
            {
                if (EditorUtility.DisplayDialog("Are you sure ?",
                        "This action will erase the old animations and animator (including collider animations that you will have to redo)",
                        "Yes !", "Absolutely not, you crazy !"))
                {
                    _name = weapons[characterNb];
                    CreateFolders($"Assets/Resources/Animations/Weapons", _name);
                    CreateAnimation();
                    CreateObject(CreateAnimator());
                }
            }
            
            if (GUILayout.Button("Generer uniquement les components (pas de regeneration des animations)"))
            {
                _name = weapons[characterNb];
                CreateObject(Resources.Load<AnimatorController>($"Animations/Weapons/{_name}/{_name}"));
            }
        }
        
        
        private void CreateFolders(string parentFolder, string name)
        {
            AssetDatabase.DeleteAsset($"{parentFolder}/{name}");
            AssetDatabase.CreateFolder(parentFolder, name);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private void CreateAnimation()
        {
            base.CreateAnimation($"Weapons/{_name}", "slash", _name);
            base.CreateAnimation($"Weapons/{_name}", "direct", _name);
        }

        private void AddRightWeaponScript(ref GameObject gameObject)
        {
            if (Enum.TryParse(_name, out WeaponsNames w))
            {
                switch (w)
                {
                    case WeaponsNames.SmallSword:
                        gameObject.AddComponent<SmallSword>();
                        break;
                }
            }
        }
        
        private AnimatorController CreateAnimator()
        {
            // Create the controller and add the four movments
            AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath($"Assets/Resources/Animations/Weapons/{_name}/{_name}.controller");
            
            // Adding parameters of the controller
            controller.AddParameter("IsFighting", AnimatorControllerParameterType.Int);
            
            AnimationClip animSlash = Resources.Load<AnimationClip>($"Animations/Weapons/{_name}/{_name}_slash");
            AnimationClip animDirect = Resources.Load<AnimationClip>($"Animations/Weapons/{_name}/{_name}_direct");

            AnimatorStateMachine stateMachine = controller.layers[0].stateMachine;

            
            AnimatorState slash = stateMachine.AddState($"{_name}_slash");
            AnimatorState direct = stateMachine.AddState($"{_name}_direct");
            AnimatorState @default = stateMachine.AddState("Default");
            
            stateMachine.defaultState = @default;

            slash.motion = animSlash;
            slash.speed = 1.5f;
            slash.AddStateMachineBehaviour<WeaponsAnimations>();
            direct.motion = animDirect;

            AnimatorStateTransition any2Slash = stateMachine.AddAnyStateTransition(slash);
            any2Slash.duration = 0;
            any2Slash.hasExitTime = false;
            any2Slash.canTransitionToSelf = false;
            any2Slash.AddCondition(AnimatorConditionMode.Equals, 1, "IsFighting");

            AnimatorStateTransition slash2Default = slash.AddTransition(stateMachine.defaultState);
            slash2Default.duration = 0;
            slash2Default.hasExitTime = true;
            slash2Default.exitTime = 1;
            slash2Default.canTransitionToSelf = false;
            slash2Default.AddCondition(AnimatorConditionMode.Equals, 0, "IsFighting");

            AnimatorStateTransition any2Direct = stateMachine.AddAnyStateTransition(direct);
            any2Direct.duration = 0;
            any2Direct.hasExitTime = false;
            any2Direct.canTransitionToSelf = false;
            any2Direct.AddCondition(AnimatorConditionMode.Equals, 2, "IsFighting");
            
            AnimatorStateTransition direct2Default = direct.AddTransition(stateMachine.defaultState);
            direct2Default.duration = 0;
            direct2Default.hasExitTime = true;
            direct2Default.exitTime = 1;
            direct2Default.canTransitionToSelf = false;
            direct2Default.AddCondition(AnimatorConditionMode.Equals, 0, "IsFighting");

            return controller;
        }

        private void CreateObject(AnimatorController controller)
        {
            GameObject gameObject = new GameObject();
            
            gameObject.name = _name;
            gameObject.tag = "Weapons";
            gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            
            // //Debug.Log("RIGIDBODY 2D")
            // gameObject.AddComponent<Rigidbody2D>();
            // gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            // gameObject.GetComponent<Rigidbody2D>().mass = 1.5f;
            // gameObject.GetComponent<Rigidbody2D>().sharedMaterial =
            //     Resources.Load<PhysicsMaterial2D>("Materials/Characters");
            
            gameObject.AddComponent<Animator>();
            gameObject.GetComponent<Animator>().runtimeAnimatorController = controller;
            
            gameObject.AddComponent<PhotonView>();

            //Debug.Log("PHOTON ANIMATOR VIEW");
            gameObject.AddComponent<PhotonAnimatorView>();
            gameObject.GetComponent<PhotonAnimatorView>()
                .SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Disabled);
            foreach (string parameter in AnimatorParameters)
            {
                gameObject.GetComponent<PhotonAnimatorView>().SetParameterSynchronized(parameter,
                    PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Continuous);
            }

            //Debug.Log("PHOTON TRANSFORM VIEW CLASSIC");
            gameObject.AddComponent<PhotonTransformViewClassic>();
            gameObject.GetComponent<PhotonTransformViewClassic>();
            gameObject.GetComponent<PhotonTransformViewClassic>().m_PositionModel =
                new PhotonTransformViewPositionModel
                {
                    SynchronizeEnabled = true,
                    TeleportEnabled = true,
                    TeleportIfDistanceGreaterThan = 3,
                    InterpolateOption = PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed,
                    ExtrapolateOption = PhotonTransformViewPositionModel.ExtrapolateOptions.Disabled,
                };
            
            gameObject.GetComponent<PhotonTransformViewClassic>().m_RotationModel =
                new PhotonTransformViewRotationModel
                {
                    SynchronizeEnabled = true,
                    InterpolateOption = PhotonTransformViewRotationModel.InterpolateOptions.Disabled,
                };

            gameObject.GetComponent<PhotonTransformViewClassic>().m_ScaleModel =
                new PhotonTransformViewScaleModel
                {
                    SynchronizeEnabled = true,
                    InterpolateOption = PhotonTransformViewScaleModel.InterpolateOptions.Disabled,
                };

            gameObject.AddComponent<SpriteRenderer>();
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>($"Animations/SourceImages/Weapons/{_name}/direct/")[0];

            gameObject.AddComponent<CapsuleCollider2D>().isTrigger = true;
            
            
            AddRightWeaponScript(ref gameObject);

            //Debug.Log("SAVING...");
            AssetDatabase.DeleteAsset($"Assets/Resources/Prefabs/Weapons/{_name}.prefab");
            PrefabUtility.SaveAsPrefabAsset(gameObject, $"Assets/Resources/Prefabs/Weapons/{_name}.prefab");
            Debug.Log("Done without error");
        }
    }
}