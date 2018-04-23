using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Models
{
    public class ComboBoxPairs
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ComboBoxPairs(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
