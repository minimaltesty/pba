using System;
using System.Collections.ObjectModel;
using System.Data;

namespace SIT.Components.Data {
    public class DataParameterCollection : KeyedCollection<string, IDataParameter> {

        protected override string GetKeyForItem( System.Data.IDataParameter item ) {
            return item.ParameterName;
        }
    }
}
