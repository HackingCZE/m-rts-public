using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class RainParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rainParticle;
    [SerializeField]private ParticleSystem _rainDropParticle;
    private List<ParticleCollisionEvent> _collisionEvents;
    
    private ObjectPool<ParticleSystem> _rainDropParticlePool;
    
    private Dictionary<ParticleSystem, float> _activeParticles;

    private void Awake()
    {
        _collisionEvents = new List<ParticleCollisionEvent>();
        _activeParticles = new Dictionary<ParticleSystem, float>();
        _rainDropParticlePool = new(_rainDropParticle, 30, transform);
    }
    
    void OnParticleCollision(GameObject other)
    {
        CheckParticlesToReturnToPool();

        int numCollisionEvents = _rainParticle.GetCollisionEvents(other, _collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            if(Random.Range(0,100) < 50) continue; // 50% percent chance to drop a raindrop
            
            Vector3 hitPosition = _collisionEvents[i].intersection;

            ParticleSystem currenObj = _rainDropParticlePool.GetFromPool();
            currenObj.transform.position = hitPosition;
            currenObj.Play();
            
            _activeParticles.Add(currenObj, 0f);
        }
    }

    private void CheckParticlesToReturnToPool()
    {
        var particlesToRemove = new List<ParticleSystem>();

        for(int i = 0; i < _activeParticles.Count; i++)
        {
            var pair = _activeParticles.ElementAt(i);
            
            _activeParticles[pair.Key] += Time.deltaTime;
            
            if(pair.Value >= 2f)
            {
                particlesToRemove.Add(pair.Key);
            }
        }
        
        foreach(var particle in particlesToRemove)
        {
            particle.Stop();
            particle.time = 0;
            _activeParticles.Remove(particle);
            _rainDropParticlePool.ReturnToPool(particle);
        }
    }
    
    
}
