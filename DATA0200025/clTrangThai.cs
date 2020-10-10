using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DATA0200025.Models;
using DomainModel.Controls;
using System.Data;
using Dapper;

namespace DATA0200025
{
    public class clTrangThai
    {
        public static TrangThaiModels GetTrangThaiById(int iID_MaTrangThai)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_TrangThai 
                            WHERE iID_MaTrangThai=@iID_MaTrangThai";
                TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = iID_MaTrangThai }).FirstOrDefault();
                return results;
            }
        }

        public static DataTable GetDataTable()
        {
            string SQL = @"SELECT *  FROM CNN25_TrangThai";
            SqlCommand cmd = new SqlCommand(SQL);
            DataTable dt = Connection.GetDataTable(cmd,CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }
        public static SelectOptionList GetDDL()
        {
            DataTable dt = GetDataTable();
            SelectOptionList DDL = new SelectOptionList(dt, "iID_MaTrangThai", "sTen");
            return DDL;
        }
    }
}
