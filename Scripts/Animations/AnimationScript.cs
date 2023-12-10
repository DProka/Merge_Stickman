using Spine.Unity;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;
    private Spine.AnimationState _animationState;

    void Awake()
    {
        
        _skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
        _animationState = _skeletonAnimation.AnimationState;
        
        _animationState.Complete += OnComplete;
    }
    // Start is called before the first frame update
    void Start()
    {
        // _skeletonAnimation.Initialize(true);

        // Spine.TrackEntry trackEntry = _animationState.SetAnimation(0, "Animation", false);
        
    }

    private void OnComplete(Spine.TrackEntry trackEntry)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _skeletonAnimation.Update(Time.deltaTime);
        
    }

}
