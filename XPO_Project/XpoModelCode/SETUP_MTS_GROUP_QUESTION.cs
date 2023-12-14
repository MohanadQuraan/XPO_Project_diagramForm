using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XPO_Project.Database
{

    public partial class SETUP_MTS_GROUP_QUESTION
    {
        public SETUP_MTS_GROUP_QUESTION(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
