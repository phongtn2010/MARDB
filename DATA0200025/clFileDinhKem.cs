using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DATA0200025.Models;
using System.Data;
using DATA0200025.SearchModels;
using Dapper;
using DomainModel.Controls;

namespace DATA0200025
{
    public class clFileDinhKem
    {
        public static IEnumerable<FileDinhKemModels> GetFileByHoSo(int iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_DinhKem 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                var results = connect.Query<FileDinhKemModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).ToList();
                return results;
            }
        }
        public static IEnumerable<FileDinhKemModels> GetFileByHoSoLoaiKhac(int iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_DinhKem 
                            WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaLoaiFile <= 9";
                var results = connect.Query<FileDinhKemModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).ToList();
                return results;
            }
        }
        public static IEnumerable<FileDinhKemModels> GetFileByLoai(int iID_MaHoSo,int iID_MaLoaiFile)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_DinhKem 
                            WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaLoaiFile=@iID_MaLoaiFile";
                var results = connect.Query<FileDinhKemModels>(SQL, new { iID_MaHoSo = iID_MaHoSo, iID_MaLoaiFile= iID_MaLoaiFile }).ToList();
                return results;
            }
        }

        public static IEnumerable<FileDinhKemModels> GetFileByHangHoa(int iID_MaHangHoa,int iID_MaLoaiFile)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_DinhKem 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa AND iID_MaLoaiFile=@iID_MaLoaiFile";
                var results = connect.Query<FileDinhKemModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa, iID_MaLoaiFile= iID_MaLoaiFile }).ToList();
                return results;
            }
        }
    }
}
