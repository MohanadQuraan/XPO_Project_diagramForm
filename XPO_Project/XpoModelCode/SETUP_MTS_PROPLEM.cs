using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XPO_Project.Database
{

    public partial class SETUP_MTS_PROPLEM
    {
        public SETUP_MTS_PROPLEM(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
