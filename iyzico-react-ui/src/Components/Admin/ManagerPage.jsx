import { useEffect, useState } from "react";
import { GetCounts } from "../../Services/AdminService";
import { SlArrowRight } from "react-icons/sl";


export default function ManagerPage() {
    const [countData, setCountData] = useState([]);

    useEffect(() => {
        try {
            const data = GetCounts().then((res) => setCountData(res.data))
            console.log("Datalar: ", data);
        } catch (error) {
            console.log("Hata: ", error);
        }

    }, [])
    return (
        <>
            <div className="component">
                <div className="page-baslik">Yönetici İşlemleri</div>
                <div className="yonetim-icerik">
                    {countData.map((item) => (
                        <div>
                            <div className="row p-0 m-0">
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3"> Bugünkü Satış Sayısı</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.orderCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Bekleyen Sipariş Sayısı</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.orderCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div className="grafic-page-baslik page-baslik mb-3 mt-3"><SlArrowRight /> Genel Durum</div>
                            <div className="row p-0 m-0">
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Toplam Sipariş</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.orderCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Kargoya Verilen Siparişler</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.orderCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">İptal Edilen Siparişler</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.orderCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Net Sipariş Sayısı</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.orderCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="grafic-page-baslik page-baslik mb-3 mt-3"><SlArrowRight /> Ürün Bilgileri</div>
                            <div className="row p-0 m-0">
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Toplam Kategori</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.categoryCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Toplam Ürün</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.productCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Stoğu Azalan Ürün Sayısı</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.productCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="grafic-page-baslik page-baslik mb-3 mt-3"><SlArrowRight /> Kullanıcı</div>
                            <div className="row p-0 m-0">
                                <div className="col-md-3 mb-3">
                                    <div className="grafic">
                                        <div className="grafic-baslik mb-3">Toplam Kullanıcı</div>
                                        <div className="grafic-icerik">
                                            <div className="count-user">
                                                <div className="cerceve">
                                                    <div>
                                                        <div className="toplam-sayi">{item.userCount}</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>


        </>
    )
}