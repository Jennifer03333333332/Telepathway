using UnityEngine;

namespace RhythmTool.Examples
{
    public class Line : MonoBehaviour
    {
        public float timestamp { get; private set; }
        public int tag { get; private set; }
        public Note note;
        public float strength;

        public void Init(Color color, float opacity, float timestamp,int tag,Note note,float strength)
        {
            this.timestamp = timestamp;
            this.tag = tag;
            this.note = note;
            this.strength = strength;
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            color = Color.Lerp(Color.clear, color, opacity);
            
            meshRenderer.material.SetColor("_Color", color);
            
        }
    }
}