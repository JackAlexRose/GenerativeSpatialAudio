using UnityEngine;

namespace LightTypes
{
    public class ScrollingLight
    {
        public GameObject Light;
        public float ScrollSpeed;
        public float CurrentOffset;
        public Material Material;
        public SpotlightEmitter Emitter;
        public int Index;
        public float delayTimer;

        public ScrollingLight(int index, GameObject light, float scrollSpeed, float currentOffset, Material material, SpotlightEmitter emitter)
        {
            Index = index;
            Light = light;
            ScrollSpeed = scrollSpeed;
            CurrentOffset = currentOffset;
            Material = material;
            Emitter = emitter;
        }
    }
}