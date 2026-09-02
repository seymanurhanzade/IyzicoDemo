import { useState, useEffect } from "react";
import { getCategories } from "../../Services/CategoryService";
import { motion } from "framer-motion";

export default function ProductUpdateModel({
    Name,
    Price,
    Explanation,
    Quantity,
    CategoryId,
    setshowModel,
    handleInputChange,
    handleUpdate,
    handleFileChange
}) {


    const [categoryData, setCategoryData] = useState([]);
    useEffect(() => {
        getCategories().then((res) => setCategoryData(res.data));
    }, [])

    return (
        <>
            <motion.div initial={{ opacity: 0, translateY: -30 }} animate={{ opacity: 1, translateY: 5 }} className="modal d-block" tabIndex="-1">
                <div className="modal-dialog modal-dialog-centered">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">Güncelleme</h5>
                            <button
                                type="button"
                                className="btn-close"
                                onClick={setshowModel}
                            ></button>
                        </div>

                        <div className="modal-body">
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün İsmi</span>
                                <input type="text" aria-label="Product Name" className="form-control" name="Name" value={Name} onChange={handleInputChange} />
                            </div>
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün Fiyatı</span>
                                <input type="number" aria-label="Product Price" className="form-control" name="Price" value={Price} onChange={handleInputChange} />
                            </div>
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün Açıklaması</span>
                                <input type="text" aria-label="Product Explanation" className="form-control" name="Explanation" value={Explanation} onChange={handleInputChange} />
                            </div>
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün Adedi</span>
                                <input type="text" aria-label="Product Quantity" className="form-control" name="Quantity" value={Quantity} onChange={handleInputChange} />
                            </div>
                            <div className="input-group">
                                <input
                                    type="file"
                                    className="form-control mb-2"
                                    name="ImageFile"
                                    multiple
                                    onChange={handleFileChange}
                                />
                            </div>
                            <select className="form-select mb-2" aria-label="Product Category" name="CategoryId" value={CategoryId} onChange={handleInputChange}>
                                <option selected>Kategori</option>
                                {categoryData.map((item) => (
                                    <option key={item.id} value={item.id}>{item.categoryName}</option>
                                ))}
                            </select>
                        </div>

                        <div className="modal-footer">
                            <button
                                type="button"
                                className="btn btn-secondary"
                                onClick={setshowModel}
                            >
                                İptal
                            </button>

                            <button
                                type="button"
                                className="btn btn-primary"
                                onClick={handleUpdate}
                            >
                                Güncelle
                            </button>
                        </div>
                    </div>
                </div>
            </motion.div>
            <div className="modal-backdrop fade show"></div>
        </>

    )
}