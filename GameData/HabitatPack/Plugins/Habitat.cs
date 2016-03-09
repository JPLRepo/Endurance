using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Habitat 
{
    public class deployableHabRestrictor : PartModule
    {
        [KSPField]
        public string animationName = "";
        [KSPField]
        public int crewCapacityDeployed = 0;
        [KSPField]
        public int crewCapcityRetracted = 0;

        Animation anim;
        public override void OnStart(StartState state)
        {
            base.OnStart(state);
        }

        public void Start()
        {
            if (!HighLogic.LoadedSceneIsFlight) return;
        }

        
        public override void OnUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                anim = part.FindModelAnimators(animationName).FirstOrDefault();
                if (anim[animationName].normalizedTime == 0f)
                {
                    this.part.CrewCapacity = crewCapcityRetracted;
                }
                if (anim[animationName].normalizedTime == 1f)
                {
                    this.part.CrewCapacity = crewCapacityDeployed;
                }


                ModuleAnimateGeneric animateModule = (ModuleAnimateGeneric)this.part.GetComponent("ModuleAnimateGeneric");
                if (this.part.protoModuleCrew.Count() > 0)
                {
                    foreach (BaseEvent eventname in this.part.GetComponent<ModuleAnimateGeneric>().Events)
                    {
                        if(eventname.guiName == animateModule.endEventGUIName)
                            eventname.guiActive = false;
                    }
                }
                else
                {
                    foreach (BaseEvent eventname in this.part.GetComponent<ModuleAnimateGeneric>().Events)
                    {
                        if (eventname.guiName == animateModule.endEventGUIName)
                            eventname.guiActive = true;
                    }
                }
            }
            base.OnUpdate();
        }
    }

    
}
