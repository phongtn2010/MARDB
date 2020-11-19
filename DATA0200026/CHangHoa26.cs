using Dapper;
using DATA0200026.WebServices.XmlType.Request;
using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class CHangHoa26
    {
        public static long ThemHangHoa(long iID_MaHoSo, long iID_MaHangHoa25, long iID_MaHangHoaNSW, int iID_MaNhom, string iID_MaPhanNhom, string iID_MaLoai, string iID_MaPhanLoai, int iID_MaTrangThai,
            string sTenPhanNhom, string sTenLoaiHangHoa, string sTenPhanLoai,
            string sMaHoSo, string sMaHoSo_DangKy, int iDuDieuKien, 
            string sTenHangHoa, string sMaSoCongNhan, string sHangSanXuat, string sMaQuocGia, string sTenQuocGia, 
            string sBanChat, string sSoHieu, string sThanhPhan, string sQuyChuan, string sMauSac,
            string sHashCode, String sUserName, String sIP, long iID_MaHangHoa_Sua = 0)
        {
            long vR = 0;

            try
            {
                String sThamSo = "";

                String sTenNhom = CDanhMuc.Get_Name_DanhMuc("NHOMTACN", iID_MaNhom.ToString());

                long iID_MaHangHoa = 0;
                Bang bang = new Bang("CNN26_HangHoa");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;

                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa25", iID_MaHangHoa25);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoaNSW", iID_MaHangHoaNSW);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaNhom", iID_MaNhom);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaPhanNhom", iID_MaPhanNhom);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoai", iID_MaLoai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaPhanLoai", iID_MaPhanLoai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sTenNhom", sTenNhom);
                bang.CmdParams.Parameters.AddWithValue("@sTenPhanNhom", sTenPhanNhom);
                bang.CmdParams.Parameters.AddWithValue("@sTenLoaiHangHoa", sTenLoaiHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sTenPhanLoai", sTenPhanLoai);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo_DangKy", sMaHoSo_DangKy);
                bang.CmdParams.Parameters.AddWithValue("@iDuDieuKien", iDuDieuKien);
                bang.CmdParams.Parameters.AddWithValue("@sTenHangHoa", sTenHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sMaSoCongNhan", sMaSoCongNhan);
                bang.CmdParams.Parameters.AddWithValue("@sHangSanXuat", sHangSanXuat);
                bang.CmdParams.Parameters.AddWithValue("@sMaQuocGia", sMaQuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sTenQuocGia", sTenQuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sBanChat", sBanChat);
                bang.CmdParams.Parameters.AddWithValue("@sSoHieu", sSoHieu);
                bang.CmdParams.Parameters.AddWithValue("@sThanhPhan", sThanhPhan);
                bang.CmdParams.Parameters.AddWithValue("@sQuyChuan", sQuyChuan);
                bang.CmdParams.Parameters.AddWithValue("@sMauSac", sMauSac);
                bang.CmdParams.Parameters.AddWithValue("@sHashCode", sHashCode);


                if (iID_MaHangHoa_Sua > 0)
                {
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHangHoa_Sua;
                    bang.Save();

                    iID_MaHangHoa = iID_MaHangHoa_Sua;
                }
                else
                {
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoaNSW);
                    bang.DuLieuMoi = true;
                    bang.Save();

                    iID_MaHangHoa = iID_MaHangHoaNSW;
                }

                vR = iID_MaHangHoa;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_HangHoa(long iID_MaHoSo)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM CNN26_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo ORDER BY iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static IEnumerable<HangHoa26Models> GetListHangHoaByHoSo(long iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN26_HangHoa 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                var results = connect.Query<HangHoa26Models>(SQL, new { iID_MaHoSo = iID_MaHoSo }).ToList();
                return results;
            }
        }
        public static HangHoa26Models GetListHangHoaByHoSoOne(long iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN26_HangHoa 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                HangHoa26Models results = connect.Query<HangHoa26Models>(SQL, new { iID_MaHoSo = iID_MaHoSo }).FirstOrDefault();
                return results;
            }
        }
        public static HangHoa26Models Get_Detail(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN26_HangHoa 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                HangHoa26Models results = connect.Query<HangHoa26Models>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }

        public static DataTable Get_HangHoa_Detail_Nsw(long iID_MaHoSo, long iID_MaHangHoaNSW)
        {
            DataTable vR = null;

            string SQL = "SELECT TOP 1 * FROM CNN26_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaHangHoaNSW=@iID_MaHangHoaNSW";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@iID_MaHangHoaNSW", iID_MaHangHoaNSW);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static int UpDate_TrangThai(long iID_MaHangHoaNSW, int iTrangThai)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN26_HangHoa SET iID_MaTrangThai=@iID_MaTrangThai WHERE iID_MaHangHoaNSW=@iID_MaHangHoaNSW");
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", iTrangThai);
                cmd.Parameters.AddWithValue("@iID_MaHangHoaNSW", iID_MaHangHoaNSW);
                Connection.UpdateDatabase(cmd);

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int UpDate_TrangThai_TheoHoSo(long iID_MaHoSo, int iTrangThai)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN26_HangHoa SET iID_MaTrangThai=@iID_MaTrangThai WHERE iID_MaHoSo=@iID_MaHoSo");
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", iTrangThai);
                cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                Connection.UpdateDatabase(cmd);

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static void Delete_HangHoa_ThongTin(long iID_MaHangHoa)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("DELETE FROM CNN26_HangHoa_ChatLuong WHERE iID_MaHangHoa=@iID_MaHangHoa");
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                Connection.UpdateDatabase(cmd);

                cmd = new SqlCommand("DELETE FROM CNN26_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa");
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                Connection.UpdateDatabase(cmd);

                cmd = new SqlCommand("DELETE FROM CNN26_HangHoa_SoLuong WHERE iID_MaHangHoa=@iID_MaHangHoa");
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                Connection.UpdateDatabase(cmd);
            }
            catch (Exception ex)
            {

            }
        }

        public static int ThemhangHoaChatLuong(long iID_MaHangHoa, int iID_MaHinhThuc, string sChiTieu, string sHinhThuc, string sHamLuong, string sMaDonViTinh, string sDonViTinh, string sGhiChu, bool bChon,
            String sUserName, String sIP)
        {
            int vR = 0;

            try
            {
                Bang bang = new Bang("CNN26_HangHoa_ChatLuong");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHinhThuc", iID_MaHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sChiTieu", sChiTieu);
                bang.CmdParams.Parameters.AddWithValue("@sHinhThuc", sHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sHamLuong", sHamLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinh", sMaDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinh", sDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sGhiChu", sGhiChu);
                bang.CmdParams.Parameters.AddWithValue("@bChon", bChon);

                bang.Save();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_HangHoa_ChatLuong(long iID_MaHangHoa)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM CNN26_HangHoa_ChatLuong WHERE iID_MaHangHoa=@iID_MaHangHoa ORDER BY dNgayTao ASC";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static IEnumerable<ChatLuong26Models> Get_List_HangHoa_ChatLuong(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT * FROM CNN26_HangHoa_ChatLuong 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                var results = connect.Query<ChatLuong26Models>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).ToList();
                return results;
            }
        }

        public static int ThemhangHoaAnToan(long iID_MaHangHoa, int iID_MaLoaiAnToan, int iID_MaHinhThuc, string sChiTieu, string sHinhThuc, string sHamLuong, string sMaDonViTinh, string sDonViTinh, string sGhiChu, bool bChon,
            String sUserName, String sIP)
        {
            int vR = 0;

            try
            {
                Bang bang = new Bang("CNN26_HangHoa_AnToan");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiAnToan", iID_MaLoaiAnToan);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHinhThuc", iID_MaHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sChiTieu", sChiTieu);
                bang.CmdParams.Parameters.AddWithValue("@sHinhThuc", sHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sHamLuong", sHamLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinh", sMaDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinh", sDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sGhiChu", sGhiChu);
                bang.CmdParams.Parameters.AddWithValue("@bChon", bChon);

                bang.Save();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_HangHoa_AnToan(long iID_MaHangHoa)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM CNN26_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa ORDER BY dNgayTao ASC";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static IEnumerable<AnToan26Models> Get_List_HangHoa_AnToan(long iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT * FROM CNN26_HangHoa_AnToan 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                var results = connect.Query<AnToan26Models>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).ToList();
                return results;
            }
        }

        public static int ThemhangHoaSoLuong(long iID_MaHangHoa, decimal rKhoiLuong, string sMaDonViTinhKL, string sDonViTinhKL, decimal rKhoiLuongTan, decimal rSoLuong, string sMaDonViTinhSL, string sDonViTinhSL, string sGhiChu, bool bChon,
            String sUserName, String sIP)
        {
            int vR = 0;

            try
            {
                Bang bang = new Bang("CNN26_HangHoa_SoLuong");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@rKhoiLuong", rKhoiLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinhKL", sMaDonViTinhKL);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinhKL", sDonViTinhKL);
                bang.CmdParams.Parameters.AddWithValue("@rKhoiLuongTan", rKhoiLuongTan);
                bang.CmdParams.Parameters.AddWithValue("@rSoLuong", rSoLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinhSL", sMaDonViTinhSL);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinhSL", sDonViTinhSL);
                bang.CmdParams.Parameters.AddWithValue("@sGhiChu", sGhiChu);
                bang.CmdParams.Parameters.AddWithValue("@bChon", bChon);

                bang.Save();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static List<HangHoaXND> GetHoaXND(long iID_MaHoSo)
        {
            var hangHoas = GetListHangHoaByHoSo(iID_MaHoSo);
            List<HangHoaXND> lst = new List<HangHoaXND>();
            HangHoaXND hanghoa;
            foreach (var item in hangHoas)
            {
                hanghoa = new HangHoaXND
                {
                    GoodsId = item.iID_MaHangHoa,
                    NSWRegisterFileCode = item.sMaHoSo_DangKy,
                    GoodsCode = item.iID_MaHangHoa25,
                    NameOfGoods = item.sTenHangHoa,
                    GroupFoodOfGoods = item.iID_MaNhom,
                    GoodTypeId = item.iID_MaLoai,
                    GoodTypeName = item.sTenLoaiHangHoa,
                    RegistrationNumber = item.sMaSoCongNhan,
                    Manufacture = item.sHangSanXuat,
                    ManufactureStateCode = item.sMaQuocGia,
                    ManufactureState = item.sTenQuocGia,
                    StandardBase = item.sSoHieu,
                    Material = item.sThanhPhan,
                    FormColorOfProducts = item.sMauSac
                };

                DataTable dtCL = Get_HangHoa_ChatLuong(item.iID_MaHangHoa);
                List<QualityCriteriaList> lstChatLuong = new List<QualityCriteriaList>();
                QualityCriteriaList cl;
                if (dtCL.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCL.Rows.Count; i++)
                    {
                        cl = new QualityCriteriaList
                        {
                            QualityCriteriaName = Convert.ToString(dtCL.Rows[i]["sChiTieu"]),
                            QualityFormOfPublication = Convert.ToInt32(dtCL.Rows[i]["iID_MaHinhThuc"]),
                            QualityRequire = Convert.ToString(dtCL.Rows[i]["sHamLuong"]),
                            QualityRequireUnitID = Convert.ToString(dtCL.Rows[i]["sMaDonViTinh"]),
                            QualityRequireUnitName = Convert.ToString(dtCL.Rows[i]["sDonViTinh"])
                        };
                        lstChatLuong.Add(cl);
                    }
                }
                dtCL.Dispose();

                hanghoa.ListQualityCriteria = lstChatLuong;

                DataTable dtAT = Get_HangHoa_AnToan(item.iID_MaHangHoa);
                List<SafetyCriteriaList> lstAnToan = new List<SafetyCriteriaList>();
                SafetyCriteriaList at;
                if (dtAT.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAT.Rows.Count; i++)
                    {
                        at = new SafetyCriteriaList
                        {
                            SafetyCriteriaName = Convert.ToString(dtAT.Rows[i]["sChiTieu"]),
                            SafetyFormOfPublication = Convert.ToInt32(dtAT.Rows[i]["iID_MaHinhThuc"]),
                            SafetyRequire = Convert.ToString(dtAT.Rows[i]["sHamLuong"]),
                            SafetyRequireUnitID = Convert.ToString(dtAT.Rows[i]["sMaDonViTinh"]),
                            SafetyRequireUnitName = Convert.ToString(dtAT.Rows[i]["sDonViTinh"])
                        };
                        lstAnToan.Add(at);
                    }
                }
                dtAT.Dispose();

                hanghoa.ListSafetyCriteria = lstAnToan;

                lst.Add(hanghoa);
            }
            return lst;
        }
    }
}
