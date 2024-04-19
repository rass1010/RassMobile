using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;

namespace RassMobile.Mods
{
    internal class AirGrab : ModFramework
    {

        bool holdR;
        bool holdL;
        GameObject holdableR = new GameObject();
        GameObject holdableL = new GameObject();

        public AirGrab(string name, string description) : base(name, description)
        {
        }
        public override void Update()
        {
            base.Update();

            if(ControllerInputPoller.instance.rightControllerGripFloat > 0.8 && !holdR)
            {
                holdR = true;
                holdableR = new GameObject();
                holdableR.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                GorillaLocomotion.Climbing.GorillaClimbable gorillaClimbable = holdableR.AddComponent<GorillaLocomotion.Climbing.GorillaClimbable>();
                GorillaLocomotion.Climbing.GorillaHandClimber gorillaHandClimber = GorillaLocomotion.Player.Instance.rightControllerTransform.Find("GorillaHandClimber").GetComponent<GorillaLocomotion.Climbing.GorillaHandClimber>();
                //holdable.AddComponent<PhotonView>();
                GorillaLocomotion.Player.Instance.BeginClimbing(gorillaClimbable, gorillaHandClimber);
            }
            else if (ControllerInputPoller.instance.rightControllerGripFloat <= 0.8 && holdR)
            {
                holdR = false;
            }

            if (ControllerInputPoller.instance.leftControllerGripFloat > 0.8 && !holdL)
            {
                holdL = true;
                holdableL = new GameObject();
                holdableL.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                GorillaLocomotion.Climbing.GorillaClimbable gorillaClimbable = holdableL.AddComponent<GorillaLocomotion.Climbing.GorillaClimbable>();
                GorillaLocomotion.Climbing.GorillaHandClimber gorillaHandClimber = GorillaLocomotion.Player.Instance.leftControllerTransform.Find("GorillaHandClimber").GetComponent<GorillaLocomotion.Climbing.GorillaHandClimber>();
                //holdable.AddComponent<PhotonView>();
                GorillaLocomotion.Player.Instance.BeginClimbing(gorillaClimbable, gorillaHandClimber);
            }
            else if (ControllerInputPoller.instance.leftControllerGripFloat <= 0.8 && holdL)
            {
                holdL = false;
            }

        }
    }
}
