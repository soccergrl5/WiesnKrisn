using System;
using System.Collections.Generic;

namespace WiesnKrisn.Roles
{
    public class WitnessTestimony
    {
        private string[] _testimonies = new string[3];

        public void ShuffleTestimonies()
        {
            List<string> list = new List<string>(_testimonies);
            Random random     = new Random();

            for (int i = 0; i < _testimonies.Length; i++)
            {
                int index = random.Next(list.Count);
                
                _testimonies[i] = list[index];
                list.RemoveAt(index);
            }
        }
        
        public void AddToTestimonies(string testimony, int index) => _testimonies[index] = testimony;
        public string[] GetTestimonies() => _testimonies;
    }
}