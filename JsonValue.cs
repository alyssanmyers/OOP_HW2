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

        public string ToString(int i)
        {
            string str = "";
            var Formatter = new JsonFormatter();
            str = Formatter.Write(this);
            return str;
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


    internal class JsonFormatter
    {
        int Indent = 1;

        public string Write(bool Value)
        {
            return Value.ToString();
        }

        public string Write(double Value)
        {
            return Value.ToString();
        }

        public string Write(string Value)
        {
            string str = "\"";
            str += Value.ToString();
            str += "\"";

            return str;
        }

        public string Write(List<JValue> Values)
        {
            string str = "[\n";
            int Count = 0;

            foreach (var Value in Values)
            {
                if (Count != 0) str += ",\n";
                for (int i = 0; i <= Indent; i++) str += "\t";
                str += Write(Value);
                Count++;
            }

            str += "\n";
            for (int i = 0; i <= Indent; i++) str += "\t";
            str += "]";
            return str;
        }

        public string Write(Dictionary<string, JValue> Values)
        {
            string Str = "{\n";
            int Count = 0;

            foreach (var Value in Values)
            {
                if (Count != 0) Str += ",\n";

                Str += "\t";
                Str +=  Write(Value.Key) + ": " + Write(Value.Value);

                if (Value.Value.Type == JType.Array) Indent++;
                else Indent = 1;

                Count++;
            }

            Str += "\n}";
            return Str;
        }

        public string Write(byte[] Values)
        {
            return System.Convert.ToBase64String(Values);
        }

        public string Write(JValue Value)
        {
            switch (Value.Type)
            {
                case JType.Bool :
                    return Write(Value.Bool);
                case JType.Number :
                    return Write(Value.Number);
                case JType.String :
                    return Write(Value.String);
                case JType.Array :
                    return Write(Value.Array);
                case JType.Object :
                    return Write(Value.Object);
                case JType.Blob :
                    return Write(Value.Blob);
                default:
                    throw new NullReferenceException();
            }
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
    }

    internal class JsonNumber : JsonValue
    {
        public double Value;

        public override string ToString()
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
}

    internal class JsonArray : JsonValue
    {
        public List<JValue> Values;

        public override string ToString()
        {
            string str = "[";

            foreach (var v in Values)
            {
                if (Values.IndexOf(v) == Values.Count - 1)
                {
                    str += "" + v.ToString();
                }
                else
                {
                    str += "" + v.ToString() + ",";
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
                str += "\"" + pair.Key + "\"";
                str += ": ";
                str += pair.Value.ToString();
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

