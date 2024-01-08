using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_collection.Models
{
    public class CV
    {
        public string? Name { get; set; }
        private List<string> _skills = new List<string>();
        public List<string>? Skills
        {
            get
            {
                return _skills;
            }
            set
            {
                _skills = value;
            }
        }
        public string? Address { get; set; }
        public int Age { get; set; }
        public string? MartialStatus {  get; set; }
        public CV()
        {

        }
        public CV(string? name, List<string>? skills, string? address, int age, string? martialStatus)
        {
            Name = name;
            Skills = skills;
            Address = address;
            Age = age;
            MartialStatus = martialStatus;
        }
        public override string ToString()
        {
            string result = "Name: " + Name +
                 "\nAge: " + Age +
                 "\nSkills: ";
            if (Skills?.Count >0)
            {
                for (int i = 0; i < Skills.Count; i++)
                {
                    result += Skills[i] + '\n';
                }
            }
            else
            {
                result += '\n'; 
            }
            result += "Address: " + Address + '\n' + "Martial status: " + MartialStatus + '\n';
            return result;
        }
    }
}
