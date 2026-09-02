import { useState, useEffect } from "react";
import { GetOrders } from "../../Services/OrderService";
export default function OrdersPage() {

    const [orders, setOrders] = useState([]);

    const getOrders = async () => {
        try {
            const data = await GetOrders().then((res) => (setOrders(res)));
            return data;
        } catch (error) {
            console.error("Error fetching orders:", error);
        }
    }
    useEffect(() => {
        getOrders();
    }, []);


    return (
        <>

            <div className="component">

                <div className="page-baslik">Siparişler</div>
              
                <div className="row w-100 p-0 m-0">
                    <div className="col-12 col-md-9 col-lg-12 mx-auto">
                            <table className="table table-striped">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>İsim</th>
                                        <th>Email</th>
                                        <th>Telefon</th>
                                        <th>Toplam</th>
                                        <th>Para Birimi</th>
                                        <th>Durum</th>
                                        <th>Oluşturulma Tarihi</th>
                                        <th>Ödeme Tarihi</th>
                                        <th>Toplam Verilen Sipariş</th>
                                       
                                    </tr>
                                </thead>
                                <tbody>
                                {orders.map((item) => (
                                    <tr key={item.id}>
                                            <td>{item.id}</td>
                                            <td>{item.customerName}</td>
                                            <td>{item.customerEmail}</td>
                                            <td>{item.customerPhone ?? "-"}</td>
                                            <td>{item.total}</td>
                                            <td>{item.currency}</td>
                                            <td>{item.status}</td>
                                            <td>{item.createdAtUtc}</td>
                                            <td>{item.paidAtUtc}</td>
                                            <td>{item.orderItemsCount}</td>
                                        </tr>
                                    )) }
                                </tbody>
                            </table>

                       
                    </div>
                </div>
            </div>
        </>

    )
}