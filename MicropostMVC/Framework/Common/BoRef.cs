using System;

namespace MicropostMVC.Framework.Common
{
    [Serializable]
    public class BoRef
    {
        private readonly string _id;

        public BoRef()
        {
            _id = string.Empty;
        }

        public BoRef(string id)
        {
            _id = id;
        }

        public string Value
        {
            get { return _id; }
       
        }

        public override string ToString()
        {
            return Value;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Value);
        }
    }
}