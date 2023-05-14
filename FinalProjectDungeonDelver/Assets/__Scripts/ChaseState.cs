using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour, IFSMState
{
    public FSMStateType StateName { get { return FSMStateType.Chase; } }

    private IFacingMover mover;

    void Awake() {
        mover = this.GetComponentInParent<IFacingMover>();
    }
    public void DoAction()
    {
        throw new System.NotImplementedException();
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public FSMStateType ShouldTransitionToState()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
