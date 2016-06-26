//This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//Habitat plugin, made from scratch by Sirkut 
//Modified for use by Endurance Interstellar mod by JPLRepo.

using System.Linq;
using UnityEngine;

namespace ENHabitat 
{
    public class ENdeployableHabRestrictor : PartModule
    {
        [KSPField]
        public string animationName = "";
        [KSPField]
        public int crewCapacityDeployed = 0;
        [KSPField]
        public int crewCapcityRetracted = 0;

        Animation anim;
        
        public void Start()
        {
            if (!HighLogic.LoadedSceneIsFlight) return;
        }


        public override void OnUpdate() 
        {
            
            anim = part.FindModelAnimators(animationName).FirstOrDefault();
            if (anim != null)
            {
                if (anim[animationName].normalizedTime == 0f)
                {
                    this.part.CrewCapacity = crewCapcityRetracted;
                }
                if (anim[animationName].normalizedTime == 1f)
                {
                    this.part.CrewCapacity = crewCapacityDeployed;
                }
            }

            ModuleAnimateGeneric animateModule = (ModuleAnimateGeneric)this.part.GetComponent("ModuleAnimateGeneric");
            if (animateModule != null)
            {
                if (this.part.protoModuleCrew.Count > 0)
                {
                    foreach (BaseEvent eventname in this.part.GetComponent<ModuleAnimateGeneric>().Events)
                    {
                        if (eventname.guiName == animateModule.endEventGUIName)
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
