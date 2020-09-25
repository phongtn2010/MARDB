using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace DomainModel.Abstract
{
    public class Bang
    {
        private BangCSDL _bangCSDL;
        private DataTable _dtDanhSachCot;

        public Bang(string TenBang, int KetNoi = 0)
        {
            CmdParams = new SqlCommand();
            this.TenBang = TenBang;
            string strtg = TenBang;
            int i = strtg.IndexOf("_");
            _bangCSDL = ThongTinBangCSDL.LayThongTinBang(TenBang, KetNoi);

            TruongKhoa = _bangCSDL.TruongKhoa;
            if (_bangCSDL.KieuGiaTri(TruongKhoa).Equals("uniqueidentifier"))
            {
                GiaTriKhoa = Globals.getNewGuid();
            }

            DuLieuMoi = true;
            TruyVanLayDanhSach = new SqlCommand(String.Format("SELECT * FROM {0} ", TenBang));

            TruongKhoaKieuSo = false;
            _dtDanhSachCot = _bangCSDL.dtCot;
            for (i = 0; i < _dtDanhSachCot.Rows.Count; i++)
            {
                if (TruongKhoa == Convert.ToString(_dtDanhSachCot.Rows[i]["COLUMN_NAME"]))
                {
                    if (Convert.ToString(_dtDanhSachCot.Rows[i]["DATA_TYPE"]) == "int")
                    {
                        TruongKhoaKieuSo = true;
                    }
                    break;
                }
            }
        }

        ~Bang()
        {
            CmdParams.Dispose();
        }

        public Boolean CoCot(String TenCot)
        {
            TenCot = TenCot.ToUpper();
            int i;
            for (i = 0; i < _dtDanhSachCot.Rows.Count; i++)
            {
                if (TenCot == Convert.ToString(_dtDanhSachCot.Rows[i]["COLUMN_NAME"]).ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        public BangCSDL bangCSDL
        {
            get { return _bangCSDL; }
        }

        public string TenBang { get; private set; }

        public SqlCommand TruyVanLayDanhSach { get; set; }

        public Boolean TruongKhoaKieuSo { get; set; }

        public Boolean DuLieuMoi { get; set; }

        public string TruongKhoa { get; set; }

        public object GiaTriKhoa { get; set; }

        public string IPSua { get; set; }

        public string MaNguoiDungSua { get; set; }

        public SqlCommand CmdParams { get; set; }

        public int TongSoBanGhi(int KetNoi = 0)
        {
            return CommonFunction.SoBanGhi(TruyVanLayDanhSach, KetNoi);
        }

        public int TongSoTrang(int KetNoi = 0)
        {
            double nums = TongSoBanGhi(KetNoi);
            return (int)Math.Ceiling(nums / Globals.PageSize);
        }

        public NameValueCollection dataTheoGiaTriKhoa(int KetNoi = 0)
        {
            NameValueCollection data = new NameValueCollection();

            if (GiaTriKhoa != null)
            {
                string SQL = String.Format("SELECT * FROM {0} WHERE {1}=@{1}", TenBang, TruongKhoa);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@" + TruongKhoa, GiaTriKhoa);
                DataTable dt = Connection.GetDataTable(cmd, KetNoi);
                if (dt.Rows.Count > 0)
                {
                    DateTime tg;
                    string TenCotMoi, GiaTriMoi;

                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        data.Add(dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());
                        if (NgonNgu.MaNgonNgu != "" && dt.Columns[i].ColumnName.StartsWith("d"))
                        {
                            try
                            {
                                tg = (DateTime)(dt.Rows[0][i]);
                                TenCotMoi = NgonNgu.MaDate + dt.Columns[i].ColumnName;
                                GiaTriMoi = tg.ToString("dd/MM/yyyy");
                                data.Add(TenCotMoi, GiaTriMoi);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                dt.Dispose();
            }
            return data;
        }

        public NameValueCollection dataTheoGiaTriKhoa(object GiaTriKhoa, int KetNoi)
        {
            this.GiaTriKhoa = GiaTriKhoa;
            return dataTheoGiaTriKhoa(KetNoi);
        }

        virtual public DataTable dtData(string sOrder, int page)
        {
            return CommonFunction.dtData(TruyVanLayDanhSach, sOrder, page, Globals.PageSize);
        }

        virtual public DataTable dtData(string sOrder, int page, int numrecord)
        {
            return CommonFunction.dtData(TruyVanLayDanhSach, sOrder, page, numrecord);
        }

        virtual public DataTable dtData(int page, int numrecord)
        {
            return dtData(TruongKhoa, page, numrecord);
        }

        virtual public DataTable dtData(int page)
        {
            return dtData(TruongKhoa, page, Globals.PageSize);
        }

        virtual public DataTable dtData(string sOrder)
        {
            return dtData(sOrder, -1, -1);
        }

        virtual public Boolean IsValid(string TenTruong, ref object GiaTri, NameValueCollection arrLoi)
        {
            return true;
        }

        public Boolean _IsCommonValid(string TenTruong, ref object GiaTri, NameValueCollection arrLoi)
        {
            if (TenTruong.StartsWith("iID"))
            {
                if (String.IsNullOrEmpty(Convert.ToString(GiaTri).Trim()))
                {
                    GiaTri = null;
                }
            }
            else if (TenTruong.StartsWith("iDM"))
            {
            }
            else if (TenTruong.StartsWith("r") || TenTruong.StartsWith("i"))
            {
                if (String.IsNullOrEmpty(Convert.ToString(GiaTri).Trim()))
                {
                    GiaTri = null;
                }
                else if (CommonFunction.IsNumeric(GiaTri) == false)
                {
                    arrLoi.Add("err_" + TenTruong, "Trường số.");
                    return false;
                }
            }
            return true;
        }

        virtual public NameValueCollection TruyenGiaTri(string ParentID, NameValueCollection Values, String PostString = "")
        {
            string name;
            object value;

            name = "DuLieuMoi";
            value = Values[ParentID + "_" + name + PostString];
            if ((string)value == "0")
            {
                DuLieuMoi = false;
            }
            else
            {
                DuLieuMoi = true;
            }
            NameValueCollection vR = new NameValueCollection();

            DataTable dt = _dtDanhSachCot;
            int i, j;

            string strDSCam = "," + BaoMat.DanhSachTruongCam(MaNguoiDungSua, TenBang) + ",";

            string t1, t2, t3;
            Boolean ok;
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                name = Convert.ToString(dt.Rows[i]["COLUMN_NAME"]);
                t1 = ParentID + "_" + name + PostString;
                t2 = ParentID + "_" + NgonNgu.MaDate + name + PostString;
                t3 = ParentID + "_" + name + PostString + "_Kem_";
                ok = false;
                for (j = 0; j <= Values.AllKeys.Length - 1; j++)
                {
                    if (t1 == Values.AllKeys[j] || t2 == Values.AllKeys[j] || t3 == Values.AllKeys[j])
                    {
                        ok = true;
                        break;
                    }
                }

                if (ok)
                {
                    value = Values[t1];

                    if (name.StartsWith("d"))
                    {
                        if (String.IsNullOrEmpty(Values[t3]))
                        {
                            value = CommonFunction.LayNgayTuXau(Values[t2]);
                        }
                        else
                        {
                            value = CDate.LayNgayTuXau(Values[t1], Values[t3]);
                        }
                    }

                    if (name.StartsWith("b"))
                    {
                        //if (value != null)
                        //{
                        if (value == null || Convert.ToString(value).ToUpper() == "OFF" || Convert.ToString(value) == "0" || Convert.ToString(value).ToUpper() == "FALSE")
                        {
                            value = false;
                        }
                        else
                        {
                            value = true;
                        }
                        //}
                    }
                    if (strDSCam.IndexOf("," + name + ",") == -1)
                    //if (strDSCam.IndexOf("," + name + ",") == -1 && value != null)
                    {
                        if (_IsCommonValid(name, ref value, vR) && IsValid(name, ref value, vR))
                        {
                            if (name.ToUpper() == TruongKhoa.ToUpper())
                            {
                                if (String.IsNullOrEmpty((string)value) == false)
                                {
                                    GiaTriKhoa = value;
                                }
                            }
                            else
                            {
                                CmdParams.Parameters.AddWithValue("@" + name, value);
                            }
                        }
                    }
                }
            }
            //CmdParams.Parameters.AddWithValue("@" + TruongKhoa, GiaTriKhoa);

            return vR;
        }

        virtual public Dictionary<string, object> LayGoiDuLieu(NameValueCollection dataIn, Boolean DuLieuSua, int KetNoi = 0)
        {
            Dictionary<string, object> dicData = null;
            NameValueCollection data = null;

            if (dataIn != null)
            {
                data = new NameValueCollection();
                data.Add(dataIn);
            }
            else
            {
                data = this.dataTheoGiaTriKhoa(KetNoi);
            }

            dicData = new Dictionary<string, object>();
            dicData["TenBang"] = this.TenBang;
            dicData["TruongKhoa"] = this.TruongKhoa;


            dicData["data"] = data;
            return dicData;
        }

        private void ChinhLaiCmdParams()
        {
            int i;

            //Xóa các trường không có trong bảng
            for (i = CmdParams.Parameters.Count - 1; i >= 0; i--)
            {
                String TenTruong = CmdParams.Parameters[i].ParameterName;
                TenTruong = TenTruong.Substring(1);
                if (_bangCSDL.CoTruong(TenTruong) == false)
                {
                    CmdParams.Parameters.RemoveAt(i);
                }
            }
            //
            for (i = 0; i < CmdParams.Parameters.Count; i++)
            {
                if (CmdParams.Parameters[i].Value == null)
                {
                    if (CmdParams.Parameters[i].DbType.ToString() != "String" && (CmdParams.Parameters[i].ParameterName.StartsWith("@r") || CmdParams.Parameters[i].ParameterName.StartsWith("@i") || CmdParams.Parameters[i].ParameterName.StartsWith("@l")))
                    {
                        CmdParams.Parameters[i].Value = 0;
                    }
                    else
                    {
                        CmdParams.Parameters[i].Value = DBNull.Value;
                    }
                }
            }
        }

        public object Save(Boolean DienThongTinThem = true, int KetNoi = 0)
        {
            //try
            //{
            if (DienThongTinThem)
            {
                if (CoCot("dNgaySua") && CmdParams.Parameters.IndexOf("@dNgaySua") < 0)
                {
                    CmdParams.Parameters.AddWithValue("@dNgaySua", DateTime.Now);
                    CmdParams.Parameters.AddWithValue("@sIPSua", IPSua);
                    CmdParams.Parameters.AddWithValue("@sID_MaNguoiDungSua", MaNguoiDungSua);
                }
            }
            if (DuLieuMoi)
            {
                String MaTam = "";
                if (!TruongKhoaKieuSo && CmdParams.Parameters.IndexOf("@" + TruongKhoa) < 0 && GiaTriKhoa != null)
                {
                    CmdParams.Parameters.AddWithValue("@" + TruongKhoa, GiaTriKhoa);
                }

                if (CoCot("sID_MaNguoiDungTao") && CmdParams.Parameters.IndexOf("@sID_MaNguoiDungTao") < 0)
                {
                    CmdParams.Parameters.AddWithValue("@sID_MaNguoiDungTao", MaNguoiDungSua);
                }
                if (CoCot("sID_MaNguoiDung_DuocGiao") && CmdParams.Parameters.IndexOf("@sID_MaNguoiDung_DuocGiao") < 0)
                {
                    CmdParams.Parameters.AddWithValue("@sID_MaNguoiDung_DuocGiao", MaNguoiDungSua);
                    CmdParams.Parameters.AddWithValue("@iID_MaNhomNguoiDung_DuocGiao", BaoMat.LayMaNhomNguoiDung(MaNguoiDungSua));
                }
                if (CoCot("bPublic") && CmdParams.Parameters.IndexOf("@bPublic") < 0)
                {
                    DataTable dt = BaoMat.Lay_dtNhomNguoiDung(MaNguoiDungSua);
                    Boolean ChoPhepMacDinh = true;
                    if (dt != null && dt.Columns.IndexOf("bChoPhepMacDinh") >= 0 && Convert.ToBoolean(dt.Rows[0]["bChoPhepMacDinh"]) == false)
                    {
                        ChoPhepMacDinh = false;
                    }
                    if (ChoPhepMacDinh && dt != null && dt.Rows.Count > 0)
                    {
                        CmdParams.Parameters.AddWithValue("@bPublic", dt.Rows[0]["bPublic_MacDinh"]);
                        CmdParams.Parameters.AddWithValue("@iID_MaNhomNguoiDung_Public", dt.Rows[0]["iID_MaNhomNguoiDung_Public_MacDinh"]);
                    }
                }
                if (TruongKhoaKieuSo)
                {
                    if (CoCot("iID_MaTam"))
                    {
                        MaTam = Globals.getNewGuid().ToString();
                        CmdParams.Parameters.AddWithValue("@iID_MaTam", MaTam);
                    }
                }
                ChinhLaiCmdParams();
                Connection.InsertRecord(TenBang, "", CmdParams, KetNoi);
                if (MaTam != "")
                {
                    SqlCommand cmd = new SqlCommand(String.Format("SELECT {0} FROM {1} WHERE iID_MaTam=@iID_MaTam", TruongKhoa, TenBang));
                    cmd.Parameters.AddWithValue("@iID_MaTam", MaTam);
                    GiaTriKhoa = Connection.GetValue(cmd, 0, KetNoi);
                    cmd.Dispose();
                }
                else
                {
                    if (String.IsNullOrEmpty(Convert.ToString(GiaTriKhoa)))
                    {
                        SqlCommand cmd = new SqlCommand(String.Format("SELECT MAX({0}) FROM {1}", TruongKhoa, TenBang));
                        GiaTriKhoa = Connection.GetValue(cmd, 0, KetNoi);
                        cmd.Dispose();
                    }
                }
            }
            else
            {
                if (CmdParams.Parameters.IndexOf("@" + TruongKhoa) < 0)
                {
                    CmdParams.Parameters.AddWithValue("@" + TruongKhoa, GiaTriKhoa);
                }
                ChinhLaiCmdParams();
                Connection.UpdateRecord(TenBang, TruongKhoa, CmdParams, KetNoi);
            }
            return GiaTriKhoa;
            //}
            //catch
            //{
            //}
            //return null;            
        }

        public int Delete(int KetNoi = 0)
        {
            return Connection.DeleteRecord(TenBang, TruongKhoa, GiaTriKhoa, KetNoi);
        }
    }
}
