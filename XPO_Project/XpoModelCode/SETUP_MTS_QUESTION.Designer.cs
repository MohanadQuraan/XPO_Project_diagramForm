﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XPO_Project.Database
{

    public partial class SETUP_MTS_QUESTION : XPLiteObject
    {
        string fID;
        [Key]
        [Size(50)]
        public string ID
        {
            get { return fID; }
            set { SetPropertyValue<string>(nameof(ID), ref fID, value); }
        }
        string fQuestionText;
        [Size(1000)]
        public string QuestionText
        {
            get { return fQuestionText; }
            set { SetPropertyValue<string>(nameof(QuestionText), ref fQuestionText, value); }
        }
        string fLOVType;
        [Size(250)]
        public string LOVType
        {
            get { return fLOVType; }
            set { SetPropertyValue<string>(nameof(LOVType), ref fLOVType, value); }
        }
        long fAnswerObject;
        public long AnswerObject
        {
            get { return fAnswerObject; }
            set { SetPropertyValue<long>(nameof(AnswerObject), ref fAnswerObject, value); }
        }
        long fAnswerAttribute;
        public long AnswerAttribute
        {
            get { return fAnswerAttribute; }
            set { SetPropertyValue<long>(nameof(AnswerAttribute), ref fAnswerAttribute, value); }
        }
        bool fIsFreeText;
        public bool IsFreeText
        {
            get { return fIsFreeText; }
            set { SetPropertyValue<bool>(nameof(IsFreeText), ref fIsFreeText, value); }
        }
        string fAnswerYesValueCriteria;
        [Size(2000)]
        public string AnswerYesValueCriteria
        {
            get { return fAnswerYesValueCriteria; }
            set { SetPropertyValue<string>(nameof(AnswerYesValueCriteria), ref fAnswerYesValueCriteria, value); }
        }
        string fCREATED_BY;
        [Size(50)]
        public string CREATED_BY
        {
            get { return fCREATED_BY; }
            set { SetPropertyValue<string>(nameof(CREATED_BY), ref fCREATED_BY, value); }
        }
        DateTime fCREATION_DATETIME;
        public DateTime CREATION_DATETIME
        {
            get { return fCREATION_DATETIME; }
            set { SetPropertyValue<DateTime>(nameof(CREATION_DATETIME), ref fCREATION_DATETIME, value); }
        }
        string fDELETED_BY;
        [Size(50)]
        public string DELETED_BY
        {
            get { return fDELETED_BY; }
            set { SetPropertyValue<string>(nameof(DELETED_BY), ref fDELETED_BY, value); }
        }
        DateTime fDELETE_DATETIME;
        public DateTime DELETE_DATETIME
        {
            get { return fDELETE_DATETIME; }
            set { SetPropertyValue<DateTime>(nameof(DELETE_DATETIME), ref fDELETE_DATETIME, value); }
        }
        string fUPDATED_BY;
        [Size(50)]
        public string UPDATED_BY
        {
            get { return fUPDATED_BY; }
            set { SetPropertyValue<string>(nameof(UPDATED_BY), ref fUPDATED_BY, value); }
        }
        DateTime fUPDATE_DATETIME;
        public DateTime UPDATE_DATETIME
        {
            get { return fUPDATE_DATETIME; }
            set { SetPropertyValue<DateTime>(nameof(UPDATE_DATETIME), ref fUPDATE_DATETIME, value); }
        }
    }

}