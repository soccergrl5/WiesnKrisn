using System;
using System.Collections.Generic;

namespace WiesnKrisn.Roles
{
    public class WitnessTestimony
    {
        private string[] _testimonies = new string[3];
        private int[] _testimonyTypes = new int[3];

        public void ShuffleTestimonies()
        {
            List<string> list  = new List<string>(_testimonies);
            List<int> listType = new List<int>(_testimonyTypes);
            Random random      = new Random();

            for (int i = 0; i < _testimonies.Length; i++)
            {
                int index = random.Next(list.Count);
                
                _testimonies[i]    = list[index];
                _testimonyTypes[i] = listType[index];
                
                list.RemoveAt(index);
                listType.RemoveAt(index);
            }
        }
        
        public void AddToTestimonies(string testimony, int index) => _testimonies[index] = testimony;
        public string[] GetTestimonies() => _testimonies;
        
        public void AddToTestimonyTypes(int testimonyType, int index) => _testimonyTypes[index] = testimonyType;
        public int GetTestimonyType(int index) => _testimonyTypes[index];
    }
}