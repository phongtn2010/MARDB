using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class CLogNSW
    {
        public static void Add(string sChucNang, string sID_MaNguoiDung, string sMaHoSo, string sNoiDung, string sTrangThai, string sXML, String sUserName, String sIP)
        {
            try
            {
                Bang bang = new Bang("CNN26_Log_NSW");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@sChucNang", sChucNang);
                bang.CmdParams.Parameters.AddWithValue("@sID_MaNguoiDung", sID_MaNguoiDung);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sNoiDung", sNoiDung);
                bang.CmdParams.Parameters.AddWithValue("@sTrangThai", sTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sXML", sXML);

                bang.Save();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
