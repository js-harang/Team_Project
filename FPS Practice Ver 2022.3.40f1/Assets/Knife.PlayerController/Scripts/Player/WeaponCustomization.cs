﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace KnifePlayerController
{
    public class WeaponCustomization : MonoBehaviour
    {
        public ModificationSlot[] Slots;

        public UnityAction ModificationEnd;
        public UnityAction<int> ModificatorAttached;
        public UnityAction<int> ModificatorDetached;

        public List<int> AttachedModificatorsIds = new List<int>();
        public List<Modificator> AttachedModificators = new List<Modificator>();

        List<ModificationSlot> availableSlots;

        void Awake()
        {
            availableSlots = new List<ModificationSlot>();

            foreach (ModificationSlot slot in Slots)
            {
                slot.ModificatorAttached += modAttached;
                slot.ModificatorDetached += modDetached;
            }
            availableSlots.AddRange(Slots);
        }

        public void ShowModifications()
        {
            updateSlots();
        }

        public void CloseModifications()
        {
            if (WeaponCustomizationUI.Instance != null)
                WeaponCustomizationUI.Instance.Close();
        }

        void modAttached(Modificator mod)
        {
            AttachedModificatorsIds.Add(mod.ModId);
            AttachedModificators.Add(mod);

            foreach (ModificationSlot s in mod.GivingSlots)
            {
                addSlot(s);
            }
            foreach (ModificationSlot s in mod.OverrideSlots)
            {
                removeSlot(s);
            }
        }

        void modDetached(Modificator mod)
        {
            AttachedModificatorsIds.Remove(mod.ModId);
            AttachedModificators.Remove(mod);

            foreach (ModificationSlot s in mod.GivingSlots)
            {
                removeSlot(s);
            }
            foreach (ModificationSlot s in mod.OverrideSlots)
            {
                addSlot(s);
            }
        }

        void updateSlots()
        {
            if (WeaponCustomizationUI.Instance != null)
                WeaponCustomizationUI.Instance.SetCustomizations(this, availableSlots);
        }

        void addSlot(ModificationSlot slot)
        {
            slot.ModificatorAttached += modAttached;
            slot.ModificatorDetached += modDetached;
            availableSlots.Add(slot);
        }

        void removeSlot(ModificationSlot slot)
        {
            slot.ModificatorAttached -= modAttached;
            slot.ModificatorDetached -= modDetached;
            availableSlots.Remove(slot);
        }

        public void ClearMod(ModificationSlot slot)
        {
            List<int> currentModificators = new List<int>(AttachedModificatorsIds);

            slot.SetupMod(-1);

            List<int> attachedModificators = new List<int>();
            List<int> detachedModificators = new List<int>();
            foreach (int mId in currentModificators)
            {
                if (!AttachedModificatorsIds.Contains(mId))
                {
                    detachedModificators.Add(mId);
                }
            }
            foreach (int mId in AttachedModificatorsIds)
            {
                if (!currentModificators.Contains(mId))
                {
                    attachedModificators.Add(mId);
                }
            }

            foreach (int mId in currentModificators)
            {
                if (ModificatorAttached != null)
                {
                    ModificatorAttached(mId);
                }
            }
            foreach (int mId in detachedModificators)
            {
                if (ModificatorDetached != null)
                {
                    ModificatorDetached(mId);
                }
            }
            if (ModificationEnd != null)
            {
                ModificationEnd();
            }
            updateSlots();
        }

        public void ApplyMod(ModificationSlot slot, Modificator mod)
        {
            List<int> currentModificators = new List<int>(AttachedModificatorsIds);

            slot.SetupMod(mod.ModId);

            List<int> attachedModificators = new List<int>();
            List<int> detachedModificators = new List<int>();
            foreach (int mId in currentModificators)
            {
                if (!AttachedModificatorsIds.Contains(mId))
                {
                    detachedModificators.Add(mId);
                }
            }
            foreach (int mId in AttachedModificatorsIds)
            {
                if (!currentModificators.Contains(mId))
                {
                    attachedModificators.Add(mId);
                }
            }

            foreach (int mId in currentModificators)
            {
                if (ModificatorAttached != null)
                {
                    ModificatorAttached(mId);
                }
            }
            foreach (int mId in detachedModificators)
            {
                if (ModificatorDetached != null)
                {
                    ModificatorDetached(mId);
                }
            }
            if (ModificationEnd != null)
            {
                ModificationEnd();
            }
            updateSlots();
        }
    }

    [System.Serializable]
    public class Modificator
    {
        [System.Serializable]
        public class ChangeMaterialModParams
        {
            public Renderer[] Targets;
            public Material[] Materials;
        }

        public string ModificatorName = "Mod name";

        public GameObject[] Modules;
        public MeshRenderer[] ModulesRenderers;
        public ChangeMaterialModParams MaterialChanger;
        public ChangeMaterialModParams[] MaterialChangers;
        public int ModId;

        public ModificationSlot[] OverrideSlots;
        public ModificationSlot[] GivingSlots;
        public Sprite ModIcon;

        public Object CustomData;

        public void Attach()
        {
            foreach (GameObject module in Modules)
            {
                module.SetActive(true);
            }

            foreach (MeshRenderer module in ModulesRenderers)
            {
                module.enabled = true;
            }

            for (int i = 0; i < MaterialChanger.Targets.Length; i++)
            {
                Material[] currentMaterials = MaterialChanger.Targets[i].sharedMaterials;
                for (int j = 0; j < MaterialChanger.Materials.Length; j++)
                {
                    if (MaterialChanger.Materials[j] != null)
                        currentMaterials[j] = MaterialChanger.Materials[j];
                }
                MaterialChanger.Targets[i].materials = currentMaterials;
            }

            foreach (ChangeMaterialModParams MaterialChanger in MaterialChangers)
            {
                for (int i = 0; i < MaterialChanger.Targets.Length; i++)
                {
                    Material[] currentMaterials = MaterialChanger.Targets[i].sharedMaterials;
                    for (int j = 0; j < MaterialChanger.Materials.Length; j++)
                    {
                        if (MaterialChanger.Materials[j] != null)
                            currentMaterials[j] = MaterialChanger.Materials[j];
                    }
                    MaterialChanger.Targets[i].materials = currentMaterials;
                }
            }

            foreach (ModificationSlot slot in OverrideSlots)
            {
                slot.SetupMod(-1);
            }
        }

        public void Detach()
        {
            foreach (GameObject module in Modules)
            {
                module.SetActive(false);
            }
            foreach (MeshRenderer module in ModulesRenderers)
            {
                module.enabled = false;
            }

            foreach (ModificationSlot slot in GivingSlots)
            {
                slot.SetupMod(-1);
            }
        }
    }
}