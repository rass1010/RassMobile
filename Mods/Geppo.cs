using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RassMobile.Mods
{
    internal class Geppo : ModFramework
    {

        Vector3 oldpositionR;
        Vector3 oldpositionL;

        public Geppo(string name, string description) : base(name, description)
        {
        }

        public override void Update()
        {
            base.Update();
            if(ControllerInputPoller.instance.rightControllerIndexFloat > 0.9f)
            {
                Vector3 force = (oldpositionR - GorillaLocomotion.Player.Instance.rightControllerTransform.localPosition);
                force = (GorillaLocomotion.Player.Instance.leftControllerTransform.parent.forward * force.z) + (GorillaLocomotion.Player.Instance.leftControllerTransform.parent.right * force.x) + (GorillaLocomotion.Player.Instance.bodyCollider.transform.up * force.y);

                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(force * 1500, ForceMode.Acceleration);
                
            }
            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.9f)
            {
                Vector3 force = (oldpositionL - GorillaLocomotion.Player.Instance.leftControllerTransform.localPosition);
                force = (GorillaLocomotion.Player.Instance.leftControllerTransform.parent.forward * force.z) + (GorillaLocomotion.Player.Instance.leftControllerTransform.parent.right * force.x) + (GorillaLocomotion.Player.Instance.bodyCollider.transform.up * force.y);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(force * 1500, ForceMode.Acceleration);
            }
            oldpositionR = GorillaLocomotion.Player.Instance.rightControllerTransform.localPosition;
            oldpositionL = GorillaLocomotion.Player.Instance.leftControllerTransform.localPosition;
        }

    }
}
