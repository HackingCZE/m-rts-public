using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RTS_Battle_Kit_2._3.Assets.Scenes.Example_scene_main.Scripts
{
    public class CharacterParticleController: MonoBehaviour
    {
        ParticleSystem _particleSystem;
        private void Awake()
        {
            _particleSystem = transform.CompareTag("Knight") ? Resources.Load<ParticleSystem>("KnightDeathParticle") : Resources.Load<ParticleSystem>("EnemyDeathParticle");
        }

        // It would be better if we used an event action.
        private void OnDestroy()
        {
            Instantiate(_particleSystem, transform.position, _particleSystem.transform.rotation);
        }
    }
}