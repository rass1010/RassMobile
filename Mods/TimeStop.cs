using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utilla;

namespace RassMobile.Mods
{
    internal class TimeStop : ModFramework
    {
        public TimeStop(string name, string description) : base(name, description)
        {
            
        }
        //bools
        public bool inAllowedRoom = false;
        public bool canFreeze;

        //float
        public bool activate;

        //vectors
        public Vector3 lastVel;
        public Vector3 lastAngVel;

        public override void Update()
        {
            activate = ControllerInputPoller.instance.rightControllerPrimaryButton;

            if (activate)
            {
                if (canFreeze)
                {
                    lastVel = GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity;
                    lastAngVel = GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.angularVelocity;
                    canFreeze = false;
                }

                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.angularVelocity = Vector3.zero;
                GorillaLocomotion.Player.Instance.transform.GetComponent<Rigidbody>().velocity = (GorillaTagger.Instance.offlineVRRig.transform.up * 0.073f) * GorillaLocomotion.Player.Instance.scale;
            }
            else
            {
                ResetFreeze();
            }
        }

        public override void OnDisabled()
        {
            ResetFreeze();
        }

        void ResetFreeze()
        {
            if (!canFreeze)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = lastVel;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.angularVelocity = lastAngVel;
                canFreeze = true;
            }
        }
    }
}
