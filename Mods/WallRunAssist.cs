using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RassMobile.Mods
{
    internal class WallRunAssist : ModFramework
    {
        RaycastHit hit;
        Vector3 ogGravity;
        public WallRunAssist(string name, string description) : base(name, description)
        {
            ogGravity = Physics.gravity;
        }

        public override void Update()
        {
            base.Update();

            if (GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching)
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                hit = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                Physics.gravity = hit.normal * -ogGravity.magnitude;
            }
            else
            {
                if (Vector3.Distance(GorillaLocomotion.Player.Instance.bodyCollider.transform.position, hit.point) > 2)
                {
                    Physics.gravity = ogGravity;
                }
            }



        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Physics.gravity = ogGravity;
        }

    }
}
