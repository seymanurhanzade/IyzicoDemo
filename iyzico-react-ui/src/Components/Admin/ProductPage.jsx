import { useState, useEffect } from "react";
import { addProduct } from "../../Services/ProductService";
import { getCategories } from "../../Services/CategoryService";
import Product from "../Component/Product";
import { motion } from "framer-motion"; 



export default function ProductPage() {
    const [categoryData, setCategoryData] = useState([]);

    useEffect(() => {
        getCategories().then((res) => setCategoryData(res.data));
    }, [])

    const [formData, setFormData] = useState({
        Name: "",
        Price: "",
        Explanation: "",
        Quantity: "",
        ImageFile: null,
        CategoryId: "",
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    }

    const handleFileChange = (e) => {
        setFormData((prevData) => ({
            ...prevData,
            ImageFile: Array.from(e.target.files),
        }));
    }

    const handleAddProduct = async () => {
        try {
            const responce = await addProduct(formData);
            alert("Ürün başarıyla eklendi.");
            /*setProductData((prevData) => [...prevData, responce.data]);*/
            return responce;
        } catch (error) {
            alert("Ürün eklenirken bir hata oluştu.", error.message);
        }
    }


    return (
        <motion.div initial={{ opacity: 0 }} animate={{ opacity: 1 }} exit={{ opacity: 0 }} className="component">
            <div className="page-baslik">Ürünler</div>
            <div className="mb-3 mt-3">
                <div className="add-item">
                    <div className="row w-100 p-0 m-0">
                        <div className="col-md-7">
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün İsmi</span>
                                <input type="text" aria-label="Product Name" className="form-control" name="Name" value={formData.Name} onChange={handleInputChange} />
                            </div>
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün Fiyatı</span>
                                <input type="number" aria-label="Product Price" className="form-control" name="Price" value={formData.Price} onChange={handleInputChange} />
                            </div>
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün Açıklaması</span>
                                <input type="text" aria-label="Product Explanation" className="form-control" name="Explanation" value={formData.Explanation} onChange={handleInputChange} />
                            </div>
                        </div>
                        <div className="col-md-5">
                            <div className="input-group mb-2">
                                <span className="input-group-text">Ürün Adedi</span>
                                <input type="text" aria-label="Product Quantity" className="form-control" name="Quantity" value={formData.Quantity} onChange={handleInputChange} />
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
                            <select className="form-select mb-2" aria-label="Product Category" name="CategoryId" value={formData.CategoryId} onChange={handleInputChange}>
                                <option selected>Kategori</option>
                                {categoryData.map((item) => (
                                    <option key={item.id} value={item.id}>{item.categoryName}</option>
                                ))}
                            </select>
                        </div>
                        <button type="button" className="btn btn-primary item-create-button" onClick={handleAddProduct}>Ürünü Ekle</button>
                    </div>
                </div>
            </div>

            <Product />
            
        </motion.div>
    )
}