using System;
using System.Collections.Generic;
using System.Text;

namespace Pba.Permission {
    public interface IPrivilege {

        string Id { get; }

    }

    public class Privilege : IPrivilege {

        protected string _id;
        public string Id { get { return _id; } }

    }




}
