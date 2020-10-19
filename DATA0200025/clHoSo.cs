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
    public class clHoSo
    {

        public static HoSoModels GetHoSoById(string iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HoSo 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                HoSoModels results = connect.Query<HoSoModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).FirstOrDefault();
                return results;
            }
        }
        public static HoSoModels GetHoSoById(int iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HoSo 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                HoSoModels results = connect.Query<HoSoModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).FirstOrDefault();
                return results;
            }
        }
        public static int GetCount(sHoSoModels models)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = DanhSachHoSoChoXyLyGDK();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            for (int i = 0; i < TrangThais.Count; i++)
            {
                trangThai = TrangThais[i];
                if (i == 0) { DK += " AND ("; }
                if (i > 0) { DK += " OR "; }
                DK += string.Format(" iID_MaTrangThai=@iID_MaTrangThai{0}", i);
                cmd.Parameters.AddWithValue("@iID_MaTrangThai" + i, trangThai.iID_MaTrangThai);
                if (i == TrangThais.Count - 1)
                {
                    DK += ")";
                }

            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo=@sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", models.sMaHoSo);
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue=@sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", models.sMaSoThue);
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                DK += " AND dNgayTaoHoSo >= @dTuNgay";
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                DK += " AND dNgayTaoHoSo <= @dDenNgay";
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));
            }
            string SQL = string.Format("SELECT count(iID_MaHoSo) as value FROM CNN25_HoSo WHERE {0} ", DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }
        public static DataTable GetDataTable(sHoSoModels models, int page, int numrecord)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK(); 
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK(); 
                    break;
                case 7:
                    TrangThais = DanhSachHoSoChoXyLyGDK();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>(); 
                    break;
            }    
            TrangThaiModels trangThai;
            String sTrangThai = "";
            for (int i = 0; i < TrangThais.Count; i++)
            {
                trangThai = TrangThais[i];
                if (i == 0) { DK += " AND ("; }
                if (i > 0) { DK += " OR "; }
                DK += string.Format(" iID_MaTrangThai=@iID_MaTrangThai{0}", i);
                cmd.Parameters.AddWithValue("@iID_MaTrangThai" + i, trangThai.iID_MaTrangThai);
                if (i == TrangThais.Count - 1)
                {
                    DK += ")";
                }

                sTrangThai += trangThai.iID_MaTrangThai + ",";

            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            { 
                DK += " AND sMaHoSo=@sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", models.sMaHoSo);
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue=@sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", models.sMaSoThue);
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                DK += " AND dNgayTaoHoSo >= @dTuNgay";
                cmd.Parameters.AddWithValue("@dTuNgay",CommonFunction.LayNgayTuXau(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                DK += " AND dNgayTaoHoSo <= @dDenNgay";
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));
            }
            string SQL = string.Format("SELECT * FROM CNN25_HoSo WHERE {0} ", DK);
            cmd.CommandText = SQL;
            string sOrder = "iID_MaHoSo DESC";
            DataTable dt = CommonFunction.dtData(cmd, sOrder, page, numrecord);
            cmd.Dispose();
            return dt;
        }
        #region list trạng thai theo màn hình
        private static SelectOptionList DDLTrangThaiSearch(List<TrangThaiModels> trangThais)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("iID_MaTrangThai", typeof(int));
            dataTable.Columns.Add("sTen", typeof(string));
            DataRow r;
            foreach (var item in trangThais)
            {
                r = dataTable.NewRow();
                r[0] = item.iID_MaTrangThai;
                r[1] = item.sTen;
                dataTable.Rows.Add(r);
            }
            SelectOptionList DDL= new SelectOptionList(dataTable, "iID_MaTrangThai", "sTen");
            dataTable.Dispose();
            return DDL;
        }
        #region danh sách màn hình BPMC Xử lý hồ sơ giấy đăng ký
        /// <summary>
        /// Loại danh sách =1
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_ChoTiepNhanHoSo()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 1
            };
            lst.Add(trangThai);
       
            return lst;
        }
        /// <summary>
        /// Loại danh sách =2
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_ChoTiepNhanHoSoGuiBoSung()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 2,
                sTen= "Chờ tiếp nhận hồ sơ gửi bổ sung theo BPMC"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 3,
                sTen= "Chờ tiếp nhận hồ sơ gửi bổ sung theo Phòng TACN",
            };
            lst.Add(trangThai);
            return lst;
        }
        /// <summary>
        /// Loại danh sách =3
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaTuChoiXacNhanGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 19,
                sTen = "Lãnh đạo cục đã phê duyệt"
            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Loại danh sách =4
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_PhongTACNYeuCauBoSungHoSo()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 13,
                sTen = "Đã gửi BPM",
                sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"

            };
            lst.Add(trangThai);

            return lst;
        }
        // <summary>
        /// Loại danh sách =5
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaPheDuyetGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 24,
                sTen = "Lãnh đạo cục đã phê duyệt"

            };
            lst.Add(trangThai);

            return lst;
        }

        /// Loại danh sách =6
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaTraGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 26,
                sTen = "Đã xác nhận GĐK"

            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 27,
                sTen = "Chờ kết quả đánh giá sự phù hợp"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 28,
                sTen = "Đã có kết quả đánh giá sự phù hợp"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 29,
                sTen = "Chờ tiếp nhận kết quả đánh giá sự phù hợp"

            };
            lst.Add(trangThai);

            return lst;
        }
        #endregion
        /// <summary>
        /// Chuyen viên loại danh sach=7
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> DanhSachHoSoChoXyLyGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 7,
                sTen = "Đã tiếp nhận"

            };
            lst.Add(trangThai);
             trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 14,
                sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                  iID_KetQuaXuLy = 3,
                 sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"
             };
            lst.Add(trangThai);
             trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 16,
                sTen = "BPMC trả thẩm định lại",
                 iID_KetQuaXuLy = 3,
                 sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"

             };
            lst.Add(trangThai);
             trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 18,
                sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                iID_KetQuaXuLy=3,
                sKetQuaXuLy= "Từ chối cấp GĐK"

            };
            lst.Add(trangThai);
             trangThai = new TrangThaiModels
            {
                 iID_MaTrangThai = 23,
                 sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                 iID_KetQuaXuLy =1,
                 sKetQuaXuLy = "Đạt cấp giấy đăng ký"

             };
            lst.Add(trangThai);

            return lst;
        }

        #region Màn hình Lãnh đạo phòng
        /// <summary>
        /// Lãnh đạo- Loại danh sách =10
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> LDP_ChoXemXetHoSo()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 10,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 10,
                sTen = "Lãnh đạo cục yêu cầu xem xét lại",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"
            };
            lst.Add(trangThai);
            return lst;
        }
        #endregion

        #region Màn hình Lãnh đạo cục
        /// <summary>
        /// Lãnh đạo- Loại danh sách =11
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> LDC_ChoXacNhanGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 17,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 2,
                sKetQuaXuLy = "Từ chối cấp GĐK"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 22,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"
            };
            lst.Add(trangThai);
            return lst;
        }

        #endregion

        #endregion
        #region Update
        public static void UpdateNguoiXem(string iID_MaHoSo,string MaND)
        {
            string SQL = "UPDATE CNN25_HoSo SET sTenNguoiXem=@sTenNguoiXem,sUserNguoiXem=@sUserNguoiXem WHERE iID_MaHoSo=@iID_MaHoSo AND (sUserNguoiXem IS NULL OR sUserNguoiXem='')";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo",iID_MaHoSo);
            cmd.Parameters.AddWithValue("@sTenNguoiXem", CPQ_NGUOIDUNG.Get_TenNguoiDung(MaND));
            cmd.Parameters.AddWithValue("@sUserNguoiXem", MaND);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }

        public static ResultModels CleanNguoiXem(string iID_MaHoSo)
        {
            int vR = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@sTenNguoiXem","");
            cmd.Parameters.AddWithValue("@sUserNguoiXem", "");
            vR=Connection.UpdateRecord("CNN25_HoSo", "iID_MaHoSo", cmd);
            cmd.Dispose();
            return new ResultModels
            {
                success=vR>0?true:false
            };

        }
        #endregion
    }
}

