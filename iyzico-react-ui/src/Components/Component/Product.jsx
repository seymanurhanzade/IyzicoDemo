import { useState, useEffect } from "react";
import { getProducts, deleteProduct, updateProduct } from "../../Services/ProductService";
import { useNavigate } from 'react-router-dom';
import { MdDelete } from "react-icons/md";
import { FaPencilAlt } from "react-icons/fa";
import ProductUpdateModel from "../Component/ProductUpdateModel";
import DeleteModal from "./DeleteModal";
import CostumSnackbar from "../Component/CostumSnackbar";



export default function Product() {
    const [productData, setProductData] = useState([]);
    const [updateProductData, setUpdateProductData] = useState({
        Name: "",
        Price: "",
        Explanation: "",
        Quantity: "",
        ImageFile: [],
        CategoryId: "",
    });
    const [showModal, setShowModal] = useState(false);
    const [deleteShowModal, setDeleteShowModal] = useState(false);
    const [selectedId, setSelectedId] = useState(null);
    const [snackBarDatas, setSnackbarDatas] = useState({
        isOpen: false,
        message: "",
        severity: ""
    });


    const navigate = useNavigate();
    useEffect(() => {
        getProducts().then((res) => setProductData(res.data));
    }, [])

    const handleDeleteProduct = async (productId) => {

        try {
            setDeleteShowModal(false);
            const res = await deleteProduct(productId);
            setProductData((prevdata) => prevdata.filter((item) => item.id !== productId));
            setSnackbarDatas({ isOpen: true, message: "İşlem başarılı.", severity: "success" })
            return res;
        } catch (error) {
            setSnackbarDatas({ isOpen: true, message: "İşlem gerçekleşirken bir hata oluştur. Lütfen daha sonra tekrar deneyin.", severity: "error" })
            console.log("hata:", error);
            setDeleteShowModal(false);
        }
    }

    const handleUpdateInputChange = (e) => {
        const { name, value } = e.target;
        setUpdateProductData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    }
    const handleOpenModel = (item) => {
        setUpdateProductData({
            Name: item.productName,
            Price: item.price,
            Explanation: item.explanation,
            Quantity: item.quantity,
            CategoryId: item.categoryId,
            ImageFile: null,
        })
        setShowModal(true);
    }
    const handleUpdateProduct = async (productId) => {

        try {
            setShowModal(false);
            const res = await updateProduct(productId, updateProductData);
            setSnackbarDatas({ isOpen: true, message: "İşlem başarılı.", severity: "success" })
            console.log("Güncellenen ürün verisi:", res.data);
            setShowModal(false);
            return res;


        } catch (error) {
            setSnackbarDatas({ isOpen: true, message: "İşlem gerçekleşirken bir hata oluştur. Lütfen daha sonra tekrar deneyin.", severity: "error" })
            console.log("hata:", error);
            setShowModal(false);
        }
    }
    const handleFileChange = (e) => {
        setUpdateProductData((prevData) => ({
            ...prevData,
            ImageFile: Array.from(e.target.files),
        }));
    }

    const handleSnackbarClose = () => {
        setSnackbarDatas({ isOpen: false })

    }
    return (
        <>
            <div className="item-list">
                <div className="row w-100 p-0 m-0">
                    <div className="col-12 col-md-9 col-lg-12 mx-auto">
                        <table className="table table-striped">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Id</th>
                                    <th>İsim</th>
                                    <th>Fiyat</th>
                                    <th>Açıklama</th>
                                    <th>Stok</th>
                                    <th>İndirim</th>
                                    <th></th>
                                </tr>
                            </thead>

                            <tbody>{productData.map((item) => (
                                <tr
                                    key={item.id}
                                    className="px-3"
                                    style={{ cursor: "pointer" }}
                                    onClick={() => navigate(`/urun-detay/${item.id}`)}>
                                    <td>
                                        <img
                                            src={`/${item.productImages?.[0]?.imageUrl}`}
                                            className="product-img-top"
                                            alt={item.productName} />
                                    </td>
                                    <td>{item.id}</td>
                                    <td>{item.productName}</td>
                                    <td>{item.price}</td>
                                    <td>{item.explanation ?? "-"}</td>
                                    <td>{item.quantity}</td>
                                    <td>{item.offerActive ? "Var" : "Yok"}</td>
                                    <td>
                                        <button type="button" className="btn btn-warning me-2" onClick={(e) => { handleOpenModel(item); e.stopPropagation(); e.preventDefault(); setSelectedId(item.id); }}><FaPencilAlt /></button>
                                        <button type="button" className="btn btn-danger" onClick={(e) => { setDeleteShowModal(true); e.stopPropagation(); e.preventDefault(); setSelectedId(item.id); }}><MdDelete /></button>
                                    </td>
                                </tr>))}

                            </tbody>

                        </table>
                        {showModal &&
                            <ProductUpdateModel
                                Name={updateProductData.Name}
                                Price={updateProductData.Price}
                                Explanation={updateProductData.Explanation}
                                Quantity={updateProductData.Quantity}
                                CategoryId={updateProductData.CategoryId}
                                handleInputChange={handleUpdateInputChange}
                                handleUpdate={() => handleUpdateProduct(selectedId)}
                                handleFileChange={handleFileChange}
                                setshowModel={() => setShowModal(false)}
                            />
                        }
                        {deleteShowModal &&
                            <DeleteModal
                                handleDeleteId={() => handleDeleteProduct(selectedId)}
                                setshowModel={() => setDeleteShowModal(false)}
                            />
                        }
                    </div>
                </div>
                <CostumSnackbar open={snackBarDatas.isOpen}
                    snackbarClose={handleSnackbarClose}
                    severity={snackBarDatas.severity}
                    message={snackBarDatas.message}>
                </CostumSnackbar>
            </div>
        </>
    )
}
