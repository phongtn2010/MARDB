using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DomainModel
{
    public class ThongTinBangCSDL
    {
        private static Dictionary<int, Dictionary<String, BangCSDL>> _dicDSBang = new Dictionary<int, Dictionary<String, BangCSDL>>();

        public static Dictionary<String, BangCSDL> LayDSBang(int KetNoi)
        {
            Dictionary<String, BangCSDL> _DSBang=null;
            if(_dicDSBang.ContainsKey(KetNoi))
            {
                _DSBang = _dicDSBang[KetNoi];
            }
            if(_DSBang == null)
            {
                _DSBang = new Dictionary<string, BangCSDL>();
                String SQL = "SELECT UPPER(TABLE_NAME) FROM INFORMATION_SCHEMA.COLUMNS GROUP BY TABLE_NAME;";
                DataTable dt = Connection.GetDataTable(SQL, KetNoi);
                int i;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    String TenBang = Convert.ToString(dt.Rows[i][0]);
                    _DSBang.Add(TenBang, new BangCSDL(TenBang, KetNoi));
                }
                dt.Dispose();
                _dicDSBang.Add(KetNoi,_DSBang);
            }
            return _DSBang;
        }

        public static Boolean TonTaiTruong(String TenBang, String TenTruong, int KetNoi)
        {
            TenBang = TenBang.ToUpper();
            BangCSDL bang = LayThongTinBang(TenBang, KetNoi);
            if (bang != null)
            {
                return bang.CoTruong(TenTruong);
            }
            return false;
        }

        public static Boolean TonTaiBang(String TenBang, int KetNoi)
        {
            TenBang = TenBang.ToUpper();
            Dictionary<String, BangCSDL> DSBang = LayDSBang(KetNoi);
            return DSBang.ContainsKey(TenBang);
        }

        public static BangCSDL LayThongTinBang(String TenBang, int KetNoi)
        {
            TenBang = TenBang.ToUpper();
            Dictionary<String, BangCSDL> DSBang = LayDSBang(KetNoi);
            if (DSBang.ContainsKey(TenBang))
            {
                return DSBang[TenBang];
            }
            return null;
        }

        public static String LayTruongKhoaCuaBang(String TenBang, int KetNoi = 0)
        {
            BangCSDL bang = LayThongTinBang(TenBang, KetNoi);
            if (bang != null)
            {
                return bang.TruongKhoa;
            }
            return null;
        }
    }

    public class BangCSDL
    {
        public string TruongKhoa;
        public string TenBang;
        public DataTable dtGiaTri = null;
        public DataTable dtCot = null;        

        public BangCSDL(String TenBang, int KetNoi)
        {
            this.TenBang = TenBang;
            String SQL = "SELECT UPPER(COLUMN_NAME) " +
                          "FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                          "WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 " +
                          "AND table_name = '" + TenBang + "'";
            this.TruongKhoa = Connection.GetValueString(SQL, "", KetNoi);
            SQL = String.Format("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}';", TenBang);
            this.dtCot = Connection.GetDataTable(SQL, KetNoi);

            for (int i = 0; i < dtCot.Rows.Count; i++)
            {
                dtCot.Rows[i]["COLUMN_NAME"] = Convert.ToString(dtCot.Rows[i]["COLUMN_NAME"]);
            }
        }

        public List<String> DanhSachTruong()
        {
            List<String> arrVR = new List<string>();
            for (int i = 0; i < dtCot.Rows.Count; i++)
            {
                arrVR.Add(Convert.ToString(dtCot.Rows[i]["COLUMN_NAME"]));
            }
            return arrVR;
        }

        public Boolean CoTruong(String TenTruong)
        {
            TenTruong = TenTruong.ToUpper();
            int i;
            for(i=0;i<dtCot.Rows.Count;i++)
            {
                if (Convert.ToString(dtCot.Rows[i]["COLUMN_NAME"]).ToUpper() == TenTruong)
                {
                    return true;
                }
            }
            return false;
        }

        public String Lay_sDanhSachTruong()
        {
            String vR = "";
            int i;
            for (i = 0; i < dtCot.Rows.Count; i++)
            {
                if(i>0)
                {
                    vR += ",";
                }
                vR += Convert.ToString(dtCot.Rows[i]["COLUMN_NAME"]);
            }
            return vR;
        }

        public String LayDanhSachTruong()
        {
            String vR = "";
            int i;
            for (i = 0; i < dtCot.Rows.Count; i++)
            {
                if (i > 0)
                {
                    vR += ",";
                }
                vR += Convert.ToString(dtCot.Rows[i]["COLUMN_NAME"]);
            }
            return vR;
        }

        public String KieuGiaTri(String TenTruong)
        {
            TenTruong = TenTruong.ToUpper();
            int i;
            for (i = 0; i < dtCot.Rows.Count; i++)
            {
                if (Convert.ToString(dtCot.Rows[i]["COLUMN_NAME"]).ToUpper() == TenTruong)
                {
                    return Convert.ToString(dtCot.Rows[i]["DATA_TYPE"]);
                }
            }
            return "";
        }

        public DataTable Lay_dtGiaTri(int KetNoi)
        {
            if (dtGiaTri == null)
            {
                String SQL = String.Format("SELECT * FROM {0}", TenBang);
                dtGiaTri = Connection.GetDataTable(SQL, KetNoi);
            }
            return dtGiaTri;
        }

        ~ BangCSDL()
        {
            if(dtCot!=null) dtCot.Dispose();
        }
    }
}
