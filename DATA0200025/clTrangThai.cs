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
        public static int GetTrangThaiIdTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, string sMessageType, string sMessageFunction)
        {
            int vR = 0;
            string SQL = @"SELECT iID_MaTrangThaiSau FROM CNN25_ChuyenTrangThai 
                            WHERE iID_MaDoiTuong=@iID_MaDoiTuong AND iID_MaHanhDong=@iID_MaHanhDong 
                            AND sMessageType=@sMessageType AND sMessageFunction=@sMessageFunction";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
            cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
            cmd.Parameters.AddWithValue("@sMessageType", sMessageType);
            cmd.Parameters.AddWithValue("@sMessageFunction", sMessageFunction);
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();         
            return vR;
        }
        public static int GetTrangThaiIdTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, int iID_MaTrangThai,int iID_MaTrangThaiTruoc)
        {
            int vR = 0;
            string SQL = @"SELECT iID_MaTrangThaiSau FROM CNN25_ChuyenTrangThai 
                            WHERE iID_MaDoiTuong=@iID_MaDoiTuong AND iID_MaHanhDong=@iID_MaHanhDong 
                            AND iID_MaTrangThaiTruoc=@iID_MaTrangThaiTruoc";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
            cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
            cmd.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", iID_MaTrangThai);
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();
            //sw dụng khi Chuyên viên	10,11,12	Thu hồi và xử lý lại Trở lại mã trạng thái hồ sơ trước đó (7,14,16,18,23)
            if (vR == -1) return iID_MaTrangThaiTruoc;

            return vR;
        }

        public static TrangThaiModels GetTrangThaiModelsTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, int iID_MaTrangThai,int iID_MaTrangThaiTruoc)
        {
            int TrangThai = GetTrangThaiIdTiepTheo(iID_MaDoiTuong, iID_MaHanhDong, iID_MaTrangThai, iID_MaTrangThaiTruoc);
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_TrangThai 
                            WHERE iID_MaTrangThai=@iID_MaTrangThai";
                TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = TrangThai }).FirstOrDefault();
                return results;
            }
        }
        public static TrangThaiModels GetTrangThaiModelsTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, string sMessageType, string sMessageFunction)
        {
            int iID_MaTrangThai = GetTrangThaiIdTiepTheo(iID_MaDoiTuong, iID_MaHanhDong, sMessageType, sMessageFunction);
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_TrangThai 
                            WHERE iID_MaTrangThai=@iID_MaTrangThai";
                TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = iID_MaTrangThai }).FirstOrDefault();
                return results;
            }
        }

        public static TrangThaiModels GetTrangThaiById(int iID_MaTrangThai)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_TrangThai 
                            WHERE iID_MaTrangThai=@iID_MaTrangThai";
                TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = iID_MaTrangThai }).FirstOrDefault();
                if(results==null)
                {
                    results = new TrangThaiModels();
                }    
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
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaTrangThai"] = 0;
            dt.Rows[0]["sTen"] = "-- Tất cả --";
            SelectOptionList DDL = new SelectOptionList(dt, "iID_MaTrangThai", "sTen");
            return DDL;
        }
        public static DataTable GetDataTable_ToChucChiDinh()
        {
            string SQL = @"SELECT *  FROM CNN25_TrangThai WHERE iID_MaTrangThai IN(28,27)";
            SqlCommand cmd = new SqlCommand(SQL);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }
        public static SelectOptionList GetTrangThai_ToChucChiDinh()
        {
            DataTable dt = GetDataTable_ToChucChiDinh();
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaTrangThai"] = 0;
            dt.Rows[0]["sTen"] = "-- Tất cả --";
            SelectOptionList DDL = new SelectOptionList(dt, "iID_MaTrangThai", "sTen");
            return DDL;
        }

        public static SelectOptionList GetTrangThai_XMCL_ChuyenVien()
        {
            string SQL = @"SELECT * FROM CNN25_TrangThai WHERE iID_MaTrangThai IN(32,37,39,34)";
            SqlCommand cmd = new SqlCommand(SQL);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaTrangThai"] = 0;
            dt.Rows[0]["sTen"] = "-- Tất cả --";
            SelectOptionList DDL = new SelectOptionList(dt, "iID_MaTrangThai", "sTen");
            return DDL;
        }
        public static SelectOptionList GetTrangThai_XNCL_LDP()
        {
            return Get_TrangThai("34,36,40,43");//"34,35,36,40,43" bỏ 35 vì tên trạng thái giống nhau, nhưng tìm kiếm chọn 34 select cả 35
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maTrangThais">"32,37,39,34"</param>
        /// <returns></returns>
        public static SelectOptionList Get_TrangThai(string maTrangThais)
        {
            string SQL = string.Format("SELECT * FROM CNN25_TrangThai WHERE iID_MaTrangThai IN({0})", maTrangThais);
            SqlCommand cmd = new SqlCommand(SQL);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaTrangThai"] = 0;
            dt.Rows[0]["sTen"] = "-- Tất cả --";
            SelectOptionList DDL = new SelectOptionList(dt, "iID_MaTrangThai", "sTen");
            return DDL;
        }
    }
}
