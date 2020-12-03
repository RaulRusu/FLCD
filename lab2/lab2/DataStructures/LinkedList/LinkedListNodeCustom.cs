using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.DataStructures.LinkedList
{
    public class LinkedListNodeCustom
    {
        public LinkedListNodeCustom() { }
        public LinkedListNodeCustom(string value) 
        {
            Value = value;
        }

        public LinkedListNodeCustom Next { get; set; } = null;
        public string Value { get; set; } = null;
    }
}
