using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class BattleSystemSampleMain : MonoBehaviour
    {
        private BattleSystem battleSystem;


        // Start is called before the first frame update
        void Start()
        {
            battleSystem = new BattleSystem();
        }

        // Update is called once per frame
        void Update()
        {
            battleSystem.Update();
        }
    }
}