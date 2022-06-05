using System;
using UnityEngine;

namespace Playground.Items
{
    public class LightBall : MonoBehaviour
    {
        private Transform position;
        protected GameObject light;
        private Rigidbody2D rb = new Rigidbody2D();
        private CircleCollider2D _circleCollider2D = new CircleCollider2D();

        

        public void Start()
        {
            light = new GameObject("The Light");
            Light _light = light.AddComponent<Light>();
            _light.color = Color.blue;
            light.transform.position = GameObject.FindGameObjectWithTag("Heros").transform.position;
        }
    }
}