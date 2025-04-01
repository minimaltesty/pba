using System;

namespace SIT.Components.Data {
    [AttributeUsage( AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple=true )]
    public class ConnectionTypeAttribute : System.Attribute {

        readonly string _typeName;

        public ConnectionTypeAttribute( string typeName )
            : base() {
            _typeName = typeName;
        }

        public string TypeName { get { return _typeName; } }

    }
}
