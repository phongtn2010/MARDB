using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DomainModel.Controls
{
    public class SelectOptionList
    {
        public SelectOptionList(DataTable dtData, string ValueMember, string DisplayMember)
        {
            this.dtData = dtData;
            this.ValueMember = ValueMember;
            this.DisplayMember = DisplayMember;
            this.AttributeMember = null;
        }
        public SelectOptionList(DataTable dtData, string ValueMember, string DisplayMember, string AttributeMember)
        {
            this.dtData = dtData ;
            this.ValueMember = ValueMember;
            this.DisplayMember = DisplayMember;
            this.AttributeMember = AttributeMember;
        }
        public SelectOptionList(DataTable dtData, int ValueMember, int DisplayMember)
        {
            this.dtData = dtData;
            this.ValueMember = dtData.Columns[ValueMember].ColumnName;
            this.DisplayMember = dtData.Columns[DisplayMember].ColumnName;
            this.AttributeMember = null;
        }
        public SelectOptionList(DataTable dtData, int ValueMember, int DisplayMember, int AttributeMember)
        {
            this.dtData = dtData;
            this.ValueMember = dtData.Columns[ValueMember].ColumnName;
            this.DisplayMember = dtData.Columns[DisplayMember].ColumnName;
            this.AttributeMember = dtData.Columns[AttributeMember].ColumnName;
        }
        ~SelectOptionList()
        {
            if (dtData != null)
            {
                dtData.Dispose();
            }
        }
        public DataTable dtData { get; private set; }
        public object ValueMember { get; private set; }
        public object DisplayMember { get; private set; }
        public object AttributeMember { get; private set; }
    }
}
