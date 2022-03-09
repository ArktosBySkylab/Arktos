using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Playground.Characters;
using Playground.Characters.Heros;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Photon.Pun;
using Playground.Characters.Monsters;

namespace Editor
{
    public class CharactersCreation : MyAnimations
    {
        private string[] AnimatorParameters = {"IsJumping", "IsRunning", "IsCGravity", "IsDead"};
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
            GUILayout.Label("Take a look to the readme in \nAnimations/Resources/SourceImages to format correclty\n all the frames ;)");
            
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

        private void CreateFolders()
        {
            AssetDatabase.CreateFolder($"Assets/Animations/Resources/{tags[tagNb]}", _name);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void CreateObject()
        {
            Debug.Log("Create folders");
            CreateFolders();
            Debug.Log("DONE");

            Debug.Log("Create Animations");
            CreateAnimation();
            Debug.Log("DONE");

            Debug.Log("Create Animator");
            AnimatorController controller = CreateAnimator();
            Debug.Log("DONE");
            
            Debug.Log("Create gameObject");
            GameObject gameObject = new GameObject();
            GameObject footCheck = new GameObject();
            GameObject headCheck = new GameObject();

            gameObject.name = _name;
            gameObject.tag = tags[tagNb];
            footCheck.transform.parent = gameObject.transform;
            headCheck.transform.parent = gameObject.transform;

            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

            gameObject.AddComponent<CharacterController2D>();
            gameObject.GetComponent<CharacterController2D>().m_FootCheck = footCheck.transform;
            gameObject.GetComponent<CharacterController2D>().m_HeadCheck = headCheck.transform;

            gameObject.AddComponent<Animator>();
            gameObject.GetComponent<Animator>().runtimeAnimatorController = controller;

            gameObject.AddComponent<PhotonView>();

            gameObject.AddComponent<PhotonAnimatorView>();
            gameObject.GetComponent<PhotonAnimatorView>()
                .SetLayerSynchronized(0, PhotonAnimatorView.SynchronizeType.Disabled);
            foreach (string parameter in AnimatorParameters)
            {
                gameObject.GetComponent<PhotonAnimatorView>().SetParameterSynchronized(parameter,
                    PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Continuous);
            }

            gameObject.AddComponent<PhotonTransformViewClassic>();
            gameObject.GetComponent<PhotonTransformViewClassic>().m_PositionModel =
                new PhotonTransformViewPositionModel();
            gameObject.GetComponent<PhotonTransformViewClassic>().m_RotationModel.InterpolateOption =
                PhotonTransformViewRotationModel.InterpolateOptions.Disabled;
            gameObject.GetComponent<PhotonTransformViewClassic>().m_ScaleModel.InterpolateOption =
                PhotonTransformViewScaleModel.InterpolateOptions.Disabled;

            PrefabUtility.SaveAsPrefabAsset(gameObject, $"Assets/Prefabs/{tags[tagNb]}/");
            Debug.Log("DONE");
        }

        private void CreateAnimation()
        {
            base.CreateAnimation($"{tags[tagNb]}/{_name}", "run", _name);
            base.CreateAnimation($"{tags[tagNb]}/{_name}", "idle", _name);
            base.CreateAnimation($"{tags[tagNb]}/{_name}", "GChange", _name);
            //base.CreateAnimation($"{tags[tagNb]}/{name}", "death", name);
        }

        
        /// <summary>
        /// Assuming that `output` is a valid name of a character and that each anim is correctly formated
        /// </summary>
        private AnimatorController CreateAnimator()
        {
            // Load the four moves available for all characters
            AnimationClip animRun = Resources.Load<AnimationClip>($"{tags[tagNb]}/{_name}/{_name}_run");
            AnimationClip animJump = Resources.Load<AnimationClip>($"{tags[tagNb]}/{_name}/{_name}_jump");
            AnimationClip animIdle = Resources.Load<AnimationClip>($"{tags[tagNb]}/{_name}/{_name}_idle");
            AnimationClip animGChange = Resources.Load<AnimationClip>($"{tags[tagNb]}/{_name}/{_name}_GChange");
            //AnimationClip animDeath = Resources.Load<AnimationClip>($"{tags[tagNb]}/{_name}/{_name}_death");

            // Create the controller and add the four movments
            AnimatorController controller = new AnimatorController();
            //controller.AddMotion(animRun);
            //controller.AddMotion(animJump);
            //controller.AddMotion(animIdle);
            //controller.AddMotion(animGChange);
            //controller.AddMotion(animDeath);
            
            // Adding parameters of the controller
            controller.AddParameter("IsRunning", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsJumping", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsGChanging", AnimatorControllerParameterType.Bool);
            controller.AddParameter("IsDead", AnimatorControllerParameterType.Bool);

            controller.AddLayer("main");
            // Create all the states
            AnimatorStateMachine stateMachine = controller.layers[0].stateMachine; // Always use only 1 layer for characters
            // Adding stateMachines
            AnimatorState run = stateMachine.AddState($"{_name}_run");
            AnimatorState jump = stateMachine.AddState($"{_name}_idle");
            AnimatorState idle = stateMachine.AddState($"{_name}_jump");
            AnimatorState GChange = stateMachine.AddState($"{_name}_GChange");
            //AnimatorState death = stateMachine.AddState($"{_name}_death");

            stateMachine.defaultState = idle;
                
            // Add states
            run.motion = animRun;
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
            
            AnimatorStateTransition jump2Idle = run.AddExitTransition();
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
            
            AnimatorStateTransition GChange2Idle = run.AddExitTransition();
            GChange2Idle.duration = 0;
            GChange2Idle.hasExitTime = false;
            GChange2Idle.AddCondition(AnimatorConditionMode.IfNot, 0, "IsGChanging");
            
            AnimatorStateTransition idle2Run = run.AddExitTransition();
            idle2Run.duration = 0;
            idle2Run.hasExitTime = false;
            idle2Run.AddCondition(AnimatorConditionMode.If, 0, "IsRunning");

            AnimatorStateTransition run2Idle = run.AddExitTransition();
            idle2Run.duration = 0;
            idle2Run.hasExitTime = false;
            idle2Run.AddCondition(AnimatorConditionMode.IfNot, 0, "IsRunning");
            
            
            AssetDatabase.CreateAsset(controller, $"Assets/Animations/Resources/{tags[tagNb]}/{name}.controller");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return controller;
        }
    }
}