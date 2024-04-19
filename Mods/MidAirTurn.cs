using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace RassMobile.Mods
{
    internal class MidAirTurn : ModFramework
    {

        bool canTurn = true;
        bool rotate = false;
        bool holding;
        float timeSinceStarted;
        Vector3 oldVel;

        public MidAirTurn(string name, string description) : base(name, description)
        {
        }

        public override void Update()
        {
            if (ControllerInputPoller.instance.rightControllerPrimary2DAxis.y < -0.9 && !holding && canTurn)
            {
                //GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles += new Vector3(0, 180, 0);
                rotate = true;
                oldVel = new Vector3(GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.x * -1, GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.y, GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity.z * -1);
                timeSinceStarted = 0f;
                canTurn = false;
                holding = true;
            }
            else if (ControllerInputPoller.instance.rightControllerPrimary2DAxis.y >= -0.9 && holding)
            {
                holding = false;
            }

            if (rotate)
            {
                Transform poll = GorillaLocomotion.Player.Instance.turnParent.transform;
                Vector3 pivotPos = GorillaLocomotion.Player.Instance.headCollider.transform.position;
                float degree = 180;
                timeSinceStarted += Time.deltaTime * 1f;
                poll.RotateAround(pivotPos, new Vector3(0, 1, 0), degree * Time.deltaTime * 2);
                GorillaLocomotion.Player.Instance.transform.GetComponent<Rigidbody>().velocity = (GorillaTagger.Instance.offlineVRRig.transform.up * 0.073f) * GorillaLocomotion.Player.Instance.scale;
                // If the object has arrived, stop the coroutine
                if (timeSinceStarted >= 0.45f)
                {
                    rotate = false;
                    canTurn = true;
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = oldVel;
                }
            }
        }

    }
}
