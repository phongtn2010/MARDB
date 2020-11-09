using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DATA0200025.Models;
using Dapper;
using DomainModel.Controls;

namespace DATA0200025
{
    public class clCommon
    {

        public static string BNN_Url = "http://mard.adp-p.com/";
       
        public static SelectOptionList KetQuaXyLyDDL(bool TatCa=true)
        {
            string SQL = "SELECT * FROM CNN25_KetQuaXuLy ORDER BY iID_KetQuaXuLy";
            DataTable dt = Connection.GetDataTable(SQL);
            if (TatCa)
            {
                DataRow r = dt.NewRow();
                r["iID_KetQuaXuLy"] = "0";
                r["sTen"] = "--Tất cả--";
                dt.Rows.InsertAt(r, 0);
            }
            SelectOptionList ddl = new SelectOptionList(dt, "iID_KetQuaXuLy", "sTen");
            dt.Dispose();
            return ddl;

        }
        public static DoiTuongModels GetDoiTuongById(int iID_MaDoiTuong)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_DoiTuong 
                            WHERE iID_MaDoiTuong=@iID_MaDoiTuong";
                DoiTuongModels results = connect.Query<DoiTuongModels>(SQL, new { iID_MaDoiTuong = iID_MaDoiTuong }).FirstOrDefault();
                return results;
            }
        }

        public static HanhDongModels GetHanhDongById(int iID_MaHanhDong)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HanhDong
                            WHERE iID_MaHanhDong=@iID_MaHanhDong";
                HanhDongModels results = connect.Query<HanhDongModels>(SQL, new { iID_MaHanhDong = iID_MaHanhDong }).FirstOrDefault();
                return results;
            }
        }
    }
}
