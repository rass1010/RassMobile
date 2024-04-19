using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RassMobile.Mods
{
    internal class Dash : ModFramework
    {

        bool hold;

        public Dash(string name, string description) : base(name, description)
        {
        }

        public override void Update()
        {
            base.Update();

            if(ControllerInputPoller.instance.rightControllerPrimaryButton && !hold)
            {
                hold = true;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(GorillaLocomotion.Player.Instance.headCollider.transform.forward * 10, UnityEngine.ForceMode.VelocityChange);
            }
            else if(!ControllerInputPoller.instance.rightControllerPrimaryButton && hold)
            {
                hold = false;
            }


        }

    }
}
