using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025
{
    public class clLoaiDanhSach
    {
        public enum From
        {
            ChoTiepNhanHoSo=1,
            ChoTiepNhanHoSoGuiBoSung=2,
            BPMC_DaTuChoiXacNhanGDK = 3,
            BPMC_PhongTACNYeuCauBoSungHoSo=4,
            BPMC_DaPheDuyetGDK = 5,
            BPMC_DaTraGDK=6,
            ChuyenVien_ChoXuLyHoSo=7,
            ChoXuLyKetQua = 8,
            ChoXemXetKetQua = 11,
            HoSoChatLuongChoDuyet = 13,
            LDP_ChoXemXetHoSo =10,
            LDC_ChoXacNhanGDK=12,
            ChoTiepNhanKetQua =14,
            ChoTiepNhanKetQuaGuiBoSung=15,
            PhongTACNYeuCauBoSungKetQua=16,
            DaPheDuyetThongBaoKetQua=17,
            DaCapThongBaoKetQua=18,
            LDP_YeuCauThamDinhLai = 19,
            LDP_LDC_YeuCauXemXetLai = 20
        }
    }
}
