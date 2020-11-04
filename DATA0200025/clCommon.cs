using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DATA0200025.Models;
using Dapper;
namespace DATA0200025
{
    public class clCommon
    {

        public static string BNN_Url = "http://mard.adp-p.com/";
        //public static int GetTrangThaiIdTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, string sMessageType, string sMessageFunction)
        //{
        //    int vR = 0;
        //    string SQL = @"SELECT iID_MaTrangThaiSau FROM CNN25_ChuyenTrangThai 
        //                    WHERE iID_MaDoiTuong=@iID_MaDoiTuong AND iID_MaHanhDong=@iID_MaHanhDong 
        //                    AND sMessageType=@sMessageType AND sMessageFunction=@sMessageFunction";
        //    SqlCommand cmd = new SqlCommand(SQL);
        //    cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
        //    cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
        //    cmd.Parameters.AddWithValue("@sMessageType", sMessageType);
        //    cmd.Parameters.AddWithValue("@sMessageFunction", sMessageFunction);
        //    vR= Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
        //    cmd.Dispose();
        //    return vR;
        //}
        //public static int GetTrangThaiIdTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong,int iID_MaTrangThaiTruoc)
        //{
        //    int vR = 0;
        //    string SQL = @"SELECT iID_MaTrangThaiSau FROM CNN25_ChuyenTrangThai 
        //                    WHERE iID_MaDoiTuong=@iID_MaDoiTuong AND iID_MaHanhDong=@iID_MaHanhDong 
        //                    AND iID_MaTrangThaiTruoc=@iID_MaTrangThaiTruoc";
        //    SqlCommand cmd = new SqlCommand(SQL);
        //    cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
        //    cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
        //    cmd.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", iID_MaTrangThaiTruoc);
        //    vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
        //    cmd.Dispose();
        //    return vR;
        //}
        //public static TrangThaiModels GetTrangThaiModelsTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, int iID_MaTrangThaiTruoc)
        //{
        //    int iID_MaTrangThai = GetTrangThaiIdTiepTheo(iID_MaDoiTuong, iID_MaHanhDong, iID_MaTrangThaiTruoc);
        //    using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
        //    {
        //        string SQL = @"SELECT *  FROM CNN25_TrangThai 
        //                    WHERE iID_MaTrangThai=@iID_MaTrangThai";
        //        TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = iID_MaTrangThai }).FirstOrDefault();
        //        return results;
        //    }
        //}
        //public static TrangThaiModels GetTrangThaiModelsTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, string sMessageType, string sMessageFunction)
        //{
        //    int iID_MaTrangThai = GetTrangThaiIdTiepTheo(iID_MaDoiTuong, iID_MaHanhDong, sMessageType, sMessageFunction);
        //    using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
        //    {
        //        string SQL = @"SELECT *  FROM CNN25_TrangThai 
        //                    WHERE iID_MaTrangThai=@iID_MaTrangThai";
        //        TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = iID_MaTrangThai }).FirstOrDefault();
        //        return results;
        //    }
        //}

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
