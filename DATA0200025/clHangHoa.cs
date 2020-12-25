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
using DATA0200025.WebServices.XmlType.Request;

namespace DATA0200025
{
    public class clHangHoa
    {
        public static HangHoaModels GetHangHoaById(string iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HangHoa 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                HangHoaModels results = connect.Query<HangHoaModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }
        public static HangHoaModels GetHangHoaById(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HangHoa 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                HangHoaModels results = connect.Query<HangHoaModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }
        public static IEnumerable<HangHoaModels> GetListHangHoaByHoSo(long iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT * FROM CNN25_HangHoa 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                var results = connect.Query<HangHoaModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).ToList();
                return results;
            }
        }

        public static DataTable Get_HangHoaTheoHoSo(long iID_MaHoSo,string iID_MaPhanLoai)
        {
            string SQL = "SELECT *  FROM CNN25_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaPhanLoai=@iID_MaPhanLoai";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@iID_MaPhanLoai", iID_MaPhanLoai);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }
        public static SelectOptionList DDL_HangHoa(long iID_MaHoSo)
        {

            string SQL = "SELECT iID_MaHangHoa,sTenHangHoa FROM CNN25_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            SelectOptionList ddl = new SelectOptionList(dt, "iID_MaHangHoa", "sTenHangHoa");
            dt.Dispose();
            return ddl;
        }
        public static SelectOptionList DDL_PhanLoaiTheoHoSo(string iID_MaDanhMuc_Cha)
        {
            string SQL = "SELECT * FROM CNN25_DanhMuc WHERE iID_MaDanhMuc_Cha=@iID_MaDanhMuc_Cha ORDER By sMa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaDanhMuc_Cha", iID_MaDanhMuc_Cha);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            SelectOptionList ddl = new SelectOptionList(dt, "sMa", "sTen");
            dt.Dispose();
            return ddl;
        }
        public static DataTable Get_ThongTinKhoiLuong(long iID_MaHangHoa)
        {
            string SQL = "SELECT * FROM CNN25_HangHoa_SoLuong  WHERE iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }
        public static DataTable Get_ThongTinChiTieuChatLuong(long iID_MaHangHoa)
        {
            string SQL = "SELECT * FROM CNN25_HangHoa_ChatLuong  WHERE iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }
        public static IEnumerable<ChiTieuChatLuongModels> GetChiTieuChatLuongDN(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT * FROM CNN25_HangHoa_ChatLuong WHERE iID_MaHangHoa=@iID_MaHangHoa AND bChon=1";
                var results = connect.Query<ChiTieuChatLuongModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).ToList();
                return results;
            }
        }
        public static DataTable Get_ThongTinChiTieuAnToanDN(long iID_MaHangHoa)
        {
            string SQL = "SELECT * FROM CNN25_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa AND iID_MaLoaiAnToan = 0";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }
        public static IEnumerable<ChiTieuModels> GetChiTieuAnToanDN(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT * FROM CNN25_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa AND bChon=1";
                var results = connect.Query<ChiTieuModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).ToList();
                return results;
            }
        }

        public static DataTable Get_ThongTinChiTieuAnToanKyThuat(long iID_MaHangHoa)
        {
            string SQL = "SELECT * FROM CNN25_HangHoa_AnToan_KyThuat WHERE iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }

        public static DataTable Get_ThongTinChiTieuAnToan_KyThuat_DanhMuc(string iID_MaLoaiTACN)
        {
            DataTable vR;

            string SQL = "SELECT * FROM CNN25_HangHoa_AnToan_KyThuat WHERE iID_MaLoaiTACN=@iID_MaLoaiTACN";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaLoaiTACN", iID_MaLoaiTACN);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static string Get_ThongTinChiTieuAnToan_KyThuat_DanhMuc_sSTTChiTieu(string iID_MaLoaiTACN)
        {
            string vR = "";

            string SQL = "SELECT TOP 1 sSTTChiTieu FROM CNN25_HangHoa_AnToan_KyThuat WHERE iID_MaLoaiTACN=@iID_MaLoaiTACN";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaLoaiTACN", iID_MaLoaiTACN);
            vR = Convert.ToString(Connection.GetValue(cmd, "", CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_ThongTinChiTieuAnToan_KyThuat_Detail(long iID_MaHangHoaATKT)
        {
            DataTable vR;

            string SQL = "SELECT * FROM CNN25_HangHoa_AnToan_KyThuat WHERE iID_MaHangHoaATKT=@iID_MaHangHoaATKT";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoaATKT", iID_MaHangHoaATKT);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static GiayChungNhanHopQuyModels GetChungNhanHopQuy(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_ChungNhanHopQuy 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                GiayChungNhanHopQuyModels results = connect.Query<GiayChungNhanHopQuyModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }
        public static HoSoXNCLModels GetHoSoXNCL(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HoSo_XNCL 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                HoSoXNCLModels results = connect.Query<HoSoXNCLModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }
        public static HoSoXNCLModels GetHoSoXNCL(long iID_MaHoSo, long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HoSo_XNCL 
                            WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaHangHoa=@iID_MaHangHoa";
                HoSoXNCLModels results = connect.Query<HoSoXNCLModels>(SQL, new { iID_MaHoSo = iID_MaHoSo, iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }

        public static void UpdateNguoiXem(string iID_MaHangHoa, string MaND)
        {
            string SQL = "UPDATE CNN25_HangHoa SET sTenNguoiXem=@sTenNguoiXem,sUserNguoiXem=@sUserNguoiXem WHERE iID_MaHangHoa=@iID_MaHangHoa AND (sUserNguoiXem IS NULL OR sUserNguoiXem='')";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            cmd.Parameters.AddWithValue("@sTenNguoiXem", CPQ_NGUOIDUNG.Get_TenNguoiDung(MaND));
            cmd.Parameters.AddWithValue("@sUserNguoiXem", MaND);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }

        public static ResultModels CleanNguoiXem(string iID_MaHangHoa)
        {
            int vR = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            cmd.Parameters.AddWithValue("@sTenNguoiXem", "");
            cmd.Parameters.AddWithValue("@sUserNguoiXem", "");
            vR = Connection.UpdateRecord("CNN25_HangHoa", "iID_MaHangHoa", cmd);
            cmd.Dispose();
            return new ResultModels
            {
                success = vR > 0 ? true : false
            };

        }

        public static List<HangHoaXND> GetHoaXND(long iID_MaHoSo)
        {
            var hangHoas = GetListHangHoaByHoSo(iID_MaHoSo);
            List<HangHoaXND> lst = new List<HangHoaXND>();
            HangHoaXND hanghoaXND;
            foreach (var item in hangHoas)
            {
                hanghoaXND = new HangHoaXND
                {
                    GoodsId=item.iID_MaHangHoaNSW,
                    NameOfGoods=item.sTenHangHoa,
                    GroupTypeId=item.iID_MaPhanLoai,
                    GroupTypeName=item.sTenPhanLoai,
                    Nature=item.sBanChat,

                };
                var chiTieus = GetChiTieuAnToanDN(item.iID_MaHangHoa);
                List<AnanyticalRequiredList> lstChiTieu = new List<AnanyticalRequiredList>();
                AnanyticalRequiredList ananytical;
                foreach (var ct in chiTieus)
                {
                    ananytical = new AnanyticalRequiredList
                    {
                         AnanyticalName= ct.sChiTieu,
                         FormOfPublication=ct.iID_MaHinhThuc,
                         Required=ct.sHamLuong,
                         RequireUnitID=ct.sMaDonViTinh,
                         RequireUnitName=ct.sDonViTinh,
                         Note=ct.sGhiChu
                    };
                    lstChiTieu.Add(ananytical);
                }
                hanghoaXND.ListAnanyticals = lstChiTieu;
                lst.Add(hanghoaXND);
            }
            return lst;
        }

        public static List<HangHoaGXNCL> GetHoaGXNCL(HangHoaModels hh)
        {
            //var hangHoas = GetHangHoaById(Convert.ToInt64(iID_MaHangHoa)); 
            
            List<HangHoaGXNCL> lst = new List<HangHoaGXNCL>();
            HangHoaGXNCL hanghoa;
            if (hh != null)
            {
                hanghoa = new HangHoaGXNCL
                {
                    GoodsId = hh.iID_MaHangHoaNSW,
                    NameOfGoods = hh.sTenHangHoa,
                    RegistrationNumber = hh.sMaSoCongNhan,
                    Manufacture = hh.sHangSanXuat,
                    ManufactureStateCode = hh.sMaQuocGia,
                    ManufactureState = hh.sTenQuocGia
                };

                DataTable dtKL = Get_ThongTinKhoiLuong(hh.iID_MaHangHoa);
                List<QuantityVolumeList> lstSoLuong = new List<QuantityVolumeList>();
                QuantityVolumeList ananytical;
                if (dtKL.Rows.Count > 0)
                {
                    for (int i = 0; i < dtKL.Rows.Count; i++)
                    {
                        ananytical = new QuantityVolumeList
                        {
                            Volume = Convert.ToDouble(dtKL.Rows[i]["rKhoiLuong"]),
                            VolumeUnitID = Convert.ToString(dtKL.Rows[i]["sMaDonViTinhKL"]),
                            VolumeUnit = Convert.ToString(dtKL.Rows[i]["sDonViTinhKL"]),
                            Quantity = Convert.ToDouble(dtKL.Rows[i]["rSoLuong"]),
                            QuantityUnitID = Convert.ToString(dtKL.Rows[i]["sMaDonViTinhSL"]),
                            QuantityUnitName = Convert.ToString(dtKL.Rows[i]["sDonViTinhSL"])
                        };
                        lstSoLuong.Add(ananytical);
                    }
                }
                dtKL.Dispose();

                hanghoa.ListQuantityVolume = lstSoLuong;
                hanghoa.Note = "";

                lst.Add(hanghoa);
            }
            return lst;
        }
        /// <summary>
        /// Update ngay tiếp nhận để select cho nhanh
        /// </summary>
        /// <param name="iID_MaHoSo"></param>
        /// <param name="dNgayTiepNhan"></param>
        public static void UpdateNgayTiepNhanHoSo(long iID_MaHoSo,DateTime dNgayTiepNhan)
        {
            string SQL = "UPDATE CNN25_HangHoa SET dNgayTiepNhanHoSo=@dNgayTiepNhanHoSo WHERE iID_MaHoSo=@iID_MaHoSo";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@dNgayTiepNhanHoSo", dNgayTiepNhan);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }
    }
}
