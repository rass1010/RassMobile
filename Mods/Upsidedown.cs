using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Viveport;

namespace RassMobile.Mods
{
    internal class Upsidedown : ModFramework
    {

        public Upsidedown(string name, string description) : base(name, description)
        {
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.x, GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.y, 180);
            Physics.gravity *= -1;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles = new Vector3(GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.x, GorillaLocomotion.Player.Instance.rightControllerTransform.parent.eulerAngles.y, 0);
            Physics.gravity *= -1;
        }

    }
}
