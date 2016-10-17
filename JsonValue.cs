using System;
using System.Collections.Generic;

namespace Myers {
    public enum JType
    {
        Null,
        Bool,
        Number,
        String,
        Array,
        Object,
        Blob
    }

    public class JValue
    {
        public JType Type { get; private set; }
        private JsonValue Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public string ToString(int i = 0)
        {
            return Value.ToString();
        }

        public JValue()
        {
            Type = JType.Null;
        }

        public JValue(bool value)
        {
            Type = JType.Bool;
            Value = new JsonBool { Value = value };
        }

        public JValue(double value)
        {
            Type = JType.Number;
            Value = new JsonNumber { Value = value };
        }

        public JValue(string value)
        {
            Type = JType.String;
            Value = new JsonString { Value = value };
        }

        public JValue(List<JValue> values)
        {
            Type = JType.Array;
            Value = new JsonArray { Values = values };
        }

        public JValue(Dictionary<string, JValue> values)
        {
            Type = JType.Object;
            Value = new JsonObject { Values = values };
        }

        public JValue(byte[] value)
        {
            Type = JType.Blob;
            Value = new JsonBlob { Values = value };
        }

        public bool Null
        {
            get { return Type == JType.Null; }
            set { Type = JType.Null; Value = null; }
        }

        public bool Bool
        {
            get
            {
                if (Type != JType.Bool) throw new InvalidCastException();
                return ((JsonBool)Value).Value;
            }
            set
            {
                Type = JType.Bool;
                Value = new JsonBool { Value = value };
            }
        }

        public double Number
        {
            get
            {
                if (Type != JType.Number) throw new InvalidCastException();
                return ((JsonNumber)Value).Value;
            }
            set
            {
                Type = JType.Number;
                Value = new JsonNumber { Value = value };
            }
        }

        public string String
        {
            get
            {
                if (Type != JType.String) throw new InvalidCastException();
                return ((JsonString)Value).Value;
            }
        }

        public List<JValue> Array
        {
            get
            {
                if (Type != JType.Array) throw new InvalidCastException();
                return ((JsonArray)Value).Values;
            }
            set
            {
                Type = JType.Array;
                Value = new JsonArray { Values = value };
            }
        }

        public Dictionary<string, JValue> Object
        {
            get
            {
                if (Type != JType.Object) throw new InvalidCastException();
                return ((JsonObject)Value).Values;
            }
            set
            {
                Type = JType.Object;
                Value = new JsonObject { Values = value };
            }
        }

        public byte[] Blob
        {
            get
            {
                if (Type != JType.Blob) throw new InvalidCastException();
                return ((JsonBlob)Value).Values;
            }
            set
            {
                Type = JType.Blob;
                Value = new JsonBlob { Values = value };
            }
        }

        public JValue this[string key]
        {
            get { return Object[key]; }
            set { Object[key] = value; }
        }

        public int Count
        {
            get { return Array.Count; }
        }

        public JValue this[int index]
        {
            get { return Array[index]; }
            set { Array[index] = value; }
        }
    }

    internal class JsonValue
    {
    }

    internal class JsonBool : JsonValue
    {
        public bool Value;

        public override string ToString()
        {
            return Value.ToString();
        }

        public string ToString(int i = 0)
        {
            return Value.ToString();
        }
    }

    internal class JsonNumber : JsonValue
    {
        public double Value;

        public override string ToString()
        {
            return Value.ToString();
        }

        public string ToString(int i = 0)
        {
            return Value.ToString();
        }
    }

    internal class JsonString : JsonValue
    {
        public string Value;

        public override string ToString()
        {
            string str = "\"";
            str += Value.ToString();
            str += "\"";

            return str;
        }

        public string ToString(int i = 0)
        {
            return Value.ToString();
        }
    }

    internal class JsonArray : JsonValue
    {
        public List<JValue> Values;

        public override string ToString()
        {

            /*  
             JsonFormatter formatter (level of indentation?, in definistion, in array, etc.)
             Formatter.Write(Values)
             return formatter.ToString()
             
            Write(bool)
            Write(double)
            Write(List<JValue>)
            Write(Dictionary<string, JValue>)
            {
                foreach (value in list)
                {
                   Write(value) + ",\n"; 
                }
            }

            Write(JValue value)
            {
                switch(value.type)
                {
                    case JType.Bool:
                        Write(value.bool);
                    etc.
                }   
            }
             
     
             */
           

            string str = "[\n";

            foreach (var v in Values)
            {
                if (Values.IndexOf(v) == Values.Count - 1)
                {
                    str += "\t" + v.ToString();
                }
                else
                {
                    //str += "\t" + v.ToString() + ",\n";
                    str += v.ToString(1) + ",\n";
                }
            }
            str += "\n]";

            return str;
        }

        public string ToString(int indent)
        {
            string str = "[";

            foreach (var v in Values)
            {
                if (Values.IndexOf(v) == Values.Count - 1)
                {
                    str += v.ToString(indent++);
                }
                else
                {
                    str += v.ToString(indent++) + ",";
                }
            }
            str += "]";

            return str;
        }
    }

    internal class JsonObject : JsonValue
    {
        public Dictionary<string, JValue> Values;

        public override string ToString()
        {
            string str = "{";
            foreach (var pair in Values)
            {
                str += "\n\"" + pair.Key + "\"";
                str += ": ";
                str += pair.Value.ToString();
            }
            str += "\n}";

            return str;
        }

        public string ToString(int indent)
        {
            string str = "{";
            foreach (var pair in Values)
            {
                str += "\"" + pair.Key + "\"";
                str += ": ";
                if (pair.Value.Type == JType.Array)
                {
                    str += pair.Value.ToString(0);
                }
                else
                {
                    str += pair.Value.ToString();
                }
            }
            str += "}";

            return str;
        }
    }

    internal class JsonBlob : JsonValue
    {
        public byte[] Values;

        public override string ToString()
        {
            return System.Convert.ToBase64String(Values);
        }
    }
}

