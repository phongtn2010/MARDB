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
        public static IEnumerable<FileDinhKemModels> GetHoSoByHoSo(int iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_DinhKem 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                var results = connect.Query<FileDinhKemModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).ToList();
                return results;
            }
        }
    }
}
