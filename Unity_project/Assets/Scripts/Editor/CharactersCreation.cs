using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using Playground.Characters;
using Playground.Characters.Heros;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Photon.Pun;
using Playground.Characters.Monsters;
using UnityEditor.VersionControl;
using UnityEngine.Events;

namespace Editor
{
    public class CharactersCreation : MyAnimations
    {
        private string[] AnimatorParameters = {"IsJumping", "IsRunning", "IsGChanging", "IsDead"};
        private string[] tags = new[] {"Heros", "Monsters"};
        private int tagNb = 0;
        private int characterNb = 0;
        private readonly string[][] characters = {Enum.GetNames(typeof(HerosNames)), Enum.GetNames(typeof(MonstersNames))};

        [MenuItem("Window/MyAnimations/CharactersCreation")]
        private static void ShowWindow()
        {
            var window = GetWindow<CharactersCreation>();
            window.titleContent = new GUIContent("Character's Creation");
            window.Show();
        }

        public override void OnGUI()
        {
            GUILayout.Label("Welcome in the custom animations creator !");
            GUILayout.Label("Take a look to the readme in \nResources/Animations/SourceImages to format correclty\n all the frames ;)");
            
            GUILayout.BeginVertical("Box");
            tagNb = GUILayout.SelectionGrid(tagNb, tags, tags.Length);
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box");
            characterNb = GUILayout.SelectionGrid(characterNb, characters[tagNb], characters[tagNb].Length);
            GUILayout.EndVertical();
            
            if (GUILayout.Button("Generer"))
            {
                _name = characters[tagNb][characterNb];
                CreateObject();
            }
        }

        private static void CreateFolders(string parentFolder, string name)
        {
            AssetDatabase.DeleteAsset($"{parentFolder}/{name}");
            AssetDatabase.CreateFolder(parentFolder, name);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddRightCharScript(ref GameObject gameObject)
        {
            if (Enum.TryParse(_name, out HerosNames hero))
            {
                switch (hero)
                {
                    case HerosNames.Alchemist:
                        gameObject.AddComponent<Alchemist>();
                        gameObject.GetComponent<Alchemist>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case HerosNames.Ninja:
                        gameObject.AddComponent<Ninja>();
                        gameObject.GetComponent<Ninja>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    
                    case HerosNames.Kitsune:
                        gameObject.AddComponent<Kitsune>();
                        gameObject.GetComponent<Kitsune>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case HerosNames.Mage:
                        gameObject.AddComponent<Mage>();
                        gameObject.GetComponent<Mage>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case HerosNames.Rogue:
                        gameObject.AddComponent<Rogue>();
                        gameObject.GetComponent<Rogue>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case HerosNames.Drow:
                        gameObject.AddComponent<Drow>();
                        gameObject.GetComponent<Drow>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case HerosNames.Kenku:
                        gameObject.AddComponent<Kenku>();
                        gameObject.GetComponent<Kenku>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case HerosNames.Invoker:
                        gameObject.AddComponent<Invoker>();
                        gameObject.GetComponent<Invoker>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case HerosNames.JojoTheKing:
                        gameObject.AddComponent<JojoTheKing>();
                        gameObject.GetComponent<JojoTheKing>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                }
            }

            if (Enum.TryParse(_name, out MonstersNames monster))
            {
                switch (monster)
                {
                    case MonstersNames.AMonster:
                        gameObject.AddComponent<AMonster>();
                        gameObject.GetComponent<AMonster>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                    
                    case MonstersNames.AnotherMonster:
                        gameObject.AddComponent<AnotherMonster>();
                        gameObject.GetComponent<AnotherMonster>().controller =
                            gameObject.GetComponent<CharacterController2D>();
                        break;
                }
            }
        }

        private void CreateObject()
        {
            CreateFolders($"Assets/Resources/Animations/{tags[tagNb]}", _name);

            CreateAnimation();

            AnimatorController controller = CreateAnimator();
            
            //Debug.Log("Create gameObject");
            GameObject gameObject = new GameObject();
            GameObject footCheck = new GameObject();
            GameObject headCheck = new GameObject();

            gameObject.name = _name;
            gameObject.tag = "Heros";
            gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            footCheck.name = "FootCheck";
            headCheck.name = "HeadCheck";
            footCheck.transform.parent = gameObject.transform;
            headCheck.transform.parent = gameObject.transform;


            //Debug.Log("RIGIDBODY 2D");
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

            //Debug.Log("CHARACTER CONTROLLER 2D");
            gameObject.AddComponent<CharacterController2D>();
            gameObject.GetComponent<CharacterController2D>().m_FootCheck = footCheck.transform;
            gameObject.GetComponent<CharacterController2D>().m_HeadCheck = headCheck.transform;
            
            AddRightCharScript(ref gameObject);

            //Debug.Log("ANIMATOR");
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

            Debug.Log("PHOTON TRANSFORM VIEW CLASSIC");
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
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>($"Animations/SourceImages/{tags[tagNb]}/{_name}/idle/")[0];

            gameObject.AddComponent<CapsuleCollider2D>();

            Debug.Log("SAVING...");
            AssetDatabase.DeleteAsset($"Assets/Resources/Prefabs/{tags[tagNb]}/{_name}.prefab");
            PrefabUtility.SaveAsPrefabAsset(gameObject, $"Assets/Resources/Prefabs/{tags[tagNb]}/{_name}.prefab");
            Debug.Log("DONE");
        }

        private void CreateAnimation()
        {
            base.CreateAnimation($"{tags[tagNb]}/{_name}", "run", _name);
            base.CreateAnimation($"{tags[tagNb]}/{_name}", "idle", _name);
            base.CreateAnimation($"{tags[tagNb]}/{_name}", "jump", _name);
            base.CreateAnimation($"{tags[tagNb]}/{_name}", "GChange", _name);
            //base.CreateAnimation($"{tags[tagNb]}/{name}", "death", name);
        }

        
        /// <summary>
        /// Assuming that `output` is a valid name of a character and that each anim is correctly formated
        /// </summary>
        private AnimatorController CreateAnimator()
        {
            // Load the four moves available for all characters
            AnimationClip animRun = Resources.Load<AnimationClip>($"Animations/{tags[tagNb]}/{_name}/{_name}_run");
            AnimationClip animJump = Resources.Load<AnimationClip>($"Animations/{tags[tagNb]}/{_name}/{_name}_jump");
            AnimationClip animIdle = Resources.Load<AnimationClip>($"Animations/{tags[tagNb]}/{_name}/{_name}_idle");
            AnimationClip animGChange = Resources.Load<AnimationClip>($"Animations/{tags[tagNb]}/{_name}/{_name}_GChange");
            //AnimationClip animDeath = Resources.Load<AnimationClip>($"Animations/{tags[tagNb]}/{_name}/{_name}_death");

            // Create the controller and add the four movments
            AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath($"Assets/Resources/Animations/{tags[tagNb]}/{_name}/{_name}.controller");
            
            // Adding parameters of the controller
            controller.AddParameter("IsRunning", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsJumping", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsGChanging", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsDead", AnimatorControllerParameterType.Bool);

            // Create all the states
            AnimatorStateMachine stateMachine = controller.layers[0].stateMachine; // Always use only 1 layer for characters
            // Adding stateMachines
            AnimatorState run = stateMachine.AddState($"{_name}_run");
            AnimatorState jump = stateMachine.AddState($"{_name}_jump");
            AnimatorState idle = stateMachine.AddState($"{_name}_idle");
            AnimatorState GChange = stateMachine.AddState($"{_name}_GChange");
            //AnimatorState death = stateMachine.AddState($"{_name}_death");

            stateMachine.defaultState = idle;
                
            // Add states
            run.motion = animRun;
            run.speed = 2;
            idle.motion = animIdle;
            jump.motion = animJump;
            GChange.motion = animGChange;
            //death.motion = animDeath;

            // Create transitions between states
            //AnimatorTransition entry2Idle = stateMachine.AddEntryTransition(idle); // Theorically already done by `stateMachine.defaultState` command
            
            AnimatorStateTransition any2Jump = stateMachine.AddAnyStateTransition(jump);
            any2Jump.duration = 0;
            any2Jump.hasExitTime = false;
            any2Jump.canTransitionToSelf = false;
            any2Jump.AddCondition(AnimatorConditionMode.If, 0, "IsJumping");

            AnimatorStateTransition jump2Idle = jump.AddTransition(idle); //run.AddExitTransition();
            jump2Idle.duration = 0;
            jump2Idle.hasExitTime = false;
            jump2Idle.AddCondition(AnimatorConditionMode.IfNot, 0, "IsJumping");
            
            //AnimatorStateTransition any2Death = stateMachine.AddAnyStateTransition(death);
            //any2Death.duration = 0;
            //any2Death.hasExitTime = false;
            //any2Death.canTransitionToSelf = false;
            //any2Death.AddCondition(AnimatorConditionMode.If, 0, "IsDead");
            
            AnimatorStateTransition any2GChange = stateMachine.AddAnyStateTransition(GChange);
            any2GChange.duration = 0;
            any2GChange.hasExitTime = false;
            any2Jump.canTransitionToSelf = false;
            any2GChange.AddCondition(AnimatorConditionMode.If, 0, "IsGChanging");
            
            AnimatorStateTransition GChange2Idle = GChange.AddTransition(idle);
            GChange2Idle.duration = 0;
            GChange2Idle.hasExitTime = false;
            GChange2Idle.AddCondition(AnimatorConditionMode.IfNot, 0, "IsGChanging");
            
            AnimatorStateTransition idle2Run = idle.AddTransition(run);
            idle2Run.duration = 0;
            idle2Run.hasExitTime = false;
            idle2Run.AddCondition(AnimatorConditionMode.If, 0, "IsRunning");

            AnimatorStateTransition run2Idle = run.AddTransition(idle);
            run2Idle.duration = 0;
            run2Idle.hasExitTime = false;
            run2Idle.AddCondition(AnimatorConditionMode.IfNot, 0, "IsRunning");
            
            return controller;
        }
    }
}