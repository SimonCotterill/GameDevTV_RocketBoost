using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterAnimationSpeed : MonoBehaviour
{
    [SerializeField] float animationSpeed = 1f;

    Animator monsterAnimator;

    // Update is called once per frame
    void Start()
    {
        monsterAnimator = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        monsterAnimator.speed = animationSpeed;
    }
}
